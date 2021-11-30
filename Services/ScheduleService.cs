using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Bson;
using WorkScheduleMaker.Data.Repositories;
using WorkScheduleMaker.Dtos;
using WorkScheduleMaker.Entities;
using WorkScheduleMaker.Enums;

namespace WorkScheduleMaker.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly UserManager<User> _userManager;
        private readonly IRepository<MonthlySchedule> _scheduleRepository;
        private readonly IHolidayService _holidayService;
        private readonly IMapper _mapper;
        private readonly Random _random;
        private readonly IRepository<Day> _dayRepository;


        #region Constructor
        public ScheduleService(UserManager<User> userManager, IRepository<MonthlySchedule> scheduleRepository, IMapper mapper, IHolidayService holidayService, IRepository<Day> dayRepository)
        {
            _userManager = userManager;
            _scheduleRepository = scheduleRepository;
            _mapper = mapper;
            _random = new Random();
            _holidayService = holidayService;
            _dayRepository = dayRepository;
        }
        #endregion

        #region Public methods

        /// <summary>
        /// Creates a Schedule for a given month if it doesn't already exists.
        /// </summary>
        /// <param name="scheduleDto">DTO from the request</param>
        /// <returns>A new MonthlySchedule</returns>
        public async Task<MonthlySchedule?> CreateSchedule(ScheduleDto scheduleDto)
        {
            var schedule = await _scheduleRepository.FindAsync(schedule => schedule.Year == scheduleDto.Year && schedule.Month == scheduleDto.Month);
            if (schedule is not null) 
            {
                return schedule;
            }
            schedule = _mapper.Map<MonthlySchedule>(scheduleDto);
            
            var users = await _userManager.Users.ToListAsync();
            var userSchedules = users.Select(user => new UserSchedule { User = user }).ToList();
            var days = (schedule.Days as List<Day>);

            var helperCounter = 1;
            var previousDay = days[0];
            for (var index = 0; index < days.Count; index++)
            {
                var day = days[index];
                var tempIndex = index;

                if (day.IsHoliday || day.IsWeekend)
                {
                    continue;
                }
                schedule.NumOfWorkdays++;
                var maxNumberOfUsersForMorning = index % 2 == 0 ? users.Count / 2 : (int)Math.Round(users.Count / 2.0);
                if (index % 2 == 0) 
                {
                    GenerateMorningSchedules(helperCounter, maxNumberOfUsersForMorning, day, userSchedules);
                    GenerateForenoonSchedules(day, userSchedules);
                }
                else 
                {
                    var usersNotScheduledYet = GetUsersNotScheduledYet(day, userSchedules);
                    List<MorningSchedule> morningSchedules = usersNotScheduledYet.Where(schedule => previousDay.UsersScheduledForForenoon.Where(user => user.User.Id == schedule.User.Id).Any()).Select(schedule => new MorningSchedule { User = schedule.User }).ToList();
                    if (morningSchedules.Count == 0) 
                    {
                        while (day.UsersScheduledForMorning.Count < maxNumberOfUsersForMorning)
                        {
                            var randomUserSchedule = usersNotScheduledYet[_random.Next(usersNotScheduledYet.Count)];
                            day.UsersScheduledForMorning.Add(new MorningSchedule { User = randomUserSchedule.User });
                        }
                    }
                    foreach (var morningschedule in morningSchedules)
                    {
                        day.UsersScheduledForMorning.Add(morningschedule);
                    }
                    GenerateForenoonSchedules(day, userSchedules);
                }
                UpdateUserSchedules(day, userSchedules);
                previousDay = day;
                helperCounter++;
            }
            schedule.IsSaved = true;
            _scheduleRepository.Add(schedule);
            schedule.Summaries = GenerateSummary(userSchedules);
            await _scheduleRepository.SaveAsync();
            return schedule;
        }

        /// <summary>
        /// Gets a Schedule from the database if it exists.
        /// </summary>
        /// <param name="year">The year</param>
        /// <param name="month">The month</param>
        /// <returns>A MonthlySchedule. It'll create a blank one if it doesn't exist.</returns>      
        public async Task<MonthlySchedule> GetSchedule(int year, int month)
        {
            var schedule = await _scheduleRepository.FindAsync(schedule => schedule.Year == year && schedule.Month == month);
            if (schedule is not null) 
            {
                return schedule;
            }
            return await CreateBlankScheduleWithRequests(year, month);
        }

        /// <summary>
        /// Deletes a schedule from the database.
        /// </summary>
        /// <param name="id">The ID of the schedule</param>
        /// <returns></returns>
        public async Task<bool> DeleteSchedule(Guid id)
        {
            var schedule = await _scheduleRepository.GetById(id);
            if (schedule is null) 
            {
                return false;
            }
            _scheduleRepository.Delete(schedule);
            await _scheduleRepository.SaveAsync();
            return true;
        }
        
        /// <summary>
        /// Updates an existing schedule in the database.
        /// </summary>
        /// <param name="id">ID of the schedule</param>
        /// <param name="updateScheduleDto">DTO from the request</param>
        /// <returns></returns>
        public async Task<bool> UpdateSchedule(Guid id, List<DayDto> dayDtos)
        {
            var schedule = await _scheduleRepository.GetById(id);
            if (schedule is null) 
            {
                return false;
            }
            var days = await _dayRepository.FindAllAsync(day => day.Date.Year == schedule.Year && day.Date.Month == schedule.Month);

            foreach (var dayDto in dayDtos)
            {
                var dayToDelete = days.Where(day => day.Date == dayDto.Date).FirstOrDefault();
                if (dayToDelete is not null) 
                {
                    _dayRepository.Delete(dayToDelete);
                }
                var dayFromDto = _mapper.Map<Day>(dayDto);
                var morningSchedules = dayFromDto.UsersScheduledForMorning.Select(schedule => new MorningSchedule 
                { 
                    UserId = schedule.UserId, 
                    IsRequest = schedule.IsRequest,

                }).ToList();
                var forenoonSchedules = dayFromDto.UsersScheduledForForenoon.Select(schedule => new Forenoonschedule 
                { 
                    UserId = schedule.UserId, 
                    IsRequest = schedule.IsRequest,

                }).ToList();
                var holidaySchedules = dayFromDto.UsersOnHoliday.Select(schedule => new HolidaySchedule
                {
                    UserId = schedule.UserId
                }).ToList();
                var newDay = new Day 
                {
                    Date = dayFromDto.Date,
                    UsersOnHoliday = holidaySchedules,
                    UsersScheduledForMorning = morningSchedules,
                    UsersScheduledForForenoon = forenoonSchedules
                };
                schedule.Days.Add(newDay);
            }
            await _dayRepository.SaveAsync();
            await _scheduleRepository.SaveAsync();
            return true;
        }
        #endregion

        #region Private Helper Methods
        private async Task<MonthlySchedule> CreateBlankScheduleWithRequests(int year, int month)
        {
            var schedule = new MonthlySchedule();
            schedule.Days = await GetDays(year, month);
            schedule.Year = year;
            schedule.Month = month;
            schedule.Id = Guid.NewGuid();
            var users = await _userManager.Users.Include(user => user.Requests.Where(request => request.Date.Year == year && request.Date.Month == month)).ToListAsync();
            for (var i = 0; i < schedule.Days.Count; i++)
            {
                var date = DateTime.Parse($"{year}-{month}-{i + 1}");
                var day = (schedule.Days as List<Day>)[i];
                if (day.IsWeekend)
                {
                    continue;
                }
                day.UsersScheduledForMorning = users.SelectMany(user => user.Requests).Where(request => request.Date == date && request.Type == RequestType.MORNING).Select(request => new MorningSchedule { User = request.User, IsRequest = true }).ToList();
                day.UsersScheduledForForenoon = users.SelectMany(user => user.Requests).Where(request => request.Date == date && request.Type == RequestType.FORENOON).Select(request => new Forenoonschedule { User = request.User, IsRequest = true }).ToList();
                day.UsersOnHoliday = users.SelectMany(user => user.Requests).Where(request => request.Date == date && request.Type == RequestType.HOLIDAY).Select(request => new HolidaySchedule { User = request.User }).ToList();
            }

            return schedule;
        }

        private async Task<List<Day>> GetDays(int year, int month) 
        {
            var holidays = await _holidayService.Find(holiday => (holiday.Year == year && holiday.Month == month) || holiday.IsFix);
            List<Day> days = new();
            for (int index = 1; index <= DateTime.DaysInMonth(year, month); index++)
            {
                var day = new Day { Date = DateTime.Parse($"{year}-{month}-{index}") };
                day.IsWeekend = IsWeekend(day);
                day.IsHoliday = IsHoliday(day, holidays);
                days.Add(day);
            }
            return days;
        }
        private bool IsWeekend(Day day) => day.Date.DayOfWeek == DayOfWeek.Saturday || day.Date.DayOfWeek == DayOfWeek.Sunday;

        private List<UserSchedule> GetUsersNotScheduledYet(Day day, List<UserSchedule> schedules)
        {
            var usersNotScheduledYet = schedules.Where(userSchedule => 
                    !day.UsersScheduledForMorning.Where(schedule => schedule.User.Id == userSchedule.User.Id).Any() &&
                    !day.UsersScheduledForForenoon.Where(schedule => schedule.User.Id == userSchedule.User.Id).Any() &&
                    !day.UsersOnHoliday.Where(schedule => schedule.User.Id == userSchedule.User.Id).Any()).ToList();
            return usersNotScheduledYet;
        }

        private List<Summary> GenerateSummary(List<UserSchedule> userSchedules)
        {
            List<Summary> summaries = userSchedules.Select(userSchedule => new Summary 
                                        { 
                                            Name = userSchedule.User.Name,
                                            Morning = userSchedule.NumOfMorningSchedules - userSchedule.NumOfHolidays,
                                            Forenoon = userSchedule.NumOfForenoonSchedules,
                                            Holiday = userSchedule.NumOfHolidays
                                        }).ToList();
            return summaries;
        }

        private void GenerateForenoonSchedules(Day day, List<UserSchedule> userSchedules)
        {
            var usersNotScheduledYet = GetUsersNotScheduledYet(day, userSchedules);
            List<Forenoonschedule> forenoonschedules = usersNotScheduledYet.Select(schedules => new Forenoonschedule { User = schedules.User }).ToList();
            foreach (var schedule in forenoonschedules)
            {
                day.UsersScheduledForForenoon.Add(schedule);
            }
        }

        private void GenerateMorningSchedules(int helperCounter, int maxUsersForMorning, Day day, List<UserSchedule> userSchedules)
        {
            var usersNotScheduledYet = GetUsersNotScheduledYet(day, userSchedules);
            var possibleUsers = usersNotScheduledYet.Where(userSchedule => userSchedule.NumOfMorningSchedules < helperCounter).ToList();
            while (day.UsersScheduledForMorning.Count < maxUsersForMorning)
            {
                var randomUserSchedule = possibleUsers[_random.Next(possibleUsers.Count)];
                day.UsersScheduledForMorning.Add(new MorningSchedule { User = randomUserSchedule.User });
                possibleUsers.Remove(randomUserSchedule);
            }
        }

        private void UpdateUserSchedules(Day day, List<UserSchedule> userSchedules)
        {
            foreach (var userSchedule in userSchedules)
            {
                var holidayCount = day.UsersOnHoliday.Where(user => user.User.Id == userSchedule.User.Id).ToList().Count;
                userSchedule.NumOfMorningSchedules += day.UsersScheduledForMorning.Where(user => user.User.Id == userSchedule.User.Id).ToList().Count;
                userSchedule.NumOfMorningSchedules += holidayCount;
                userSchedule.NumOfForenoonSchedules += day.UsersScheduledForForenoon.Where(user => user.User.Id == userSchedule.User.Id).ToList().Count;
                userSchedule.NumOfHolidays += holidayCount;
            }
        }

        private bool IsHoliday(Day day, IEnumerable<Holiday> holidays) 
        {
            var d = day.Date.Day;
            var month = day.Date.Month;
            return holidays.Where(holiday => holiday.Day == d && holiday.Month == month).Any();
        }
        #endregion

    }
}