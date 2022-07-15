using WorkSchedule.Api.Constants;
using WorkSchedule.Api.Dtos;
using WorkSchedule.Application.Data;
using WorkSchedule.Application.Exceptions;
using WorkSchedule.Application.Persistency.Entities;
using WorkSchedule.Application.Services.FileService;

namespace WorkSchedule.Application.Services.ScheduleService
{
    public class ScheduleService : IScheduleService
    {
        private readonly Random _random;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;

        #region Constructor
        public ScheduleService(IUnitOfWork unitOfWork, IFileService fileService)
        {
            _random = new Random();
            _unitOfWork = unitOfWork;
            _fileService = fileService;
        }
        #endregion

        #region Public methods

        /// <summary>
        /// Creates a Schedule for a given month if it doesn't already exists.
        /// </summary>
        /// <param name="scheduleDto">DTO from the request</param>
        /// <returns>A new MonthlySchedule</returns>
        public async Task<MonthlySchedule?> CreateSchedule(int year, int month)
        {
            var schedule = await CreateBlankScheduleWithRequests(year, month);
            var users = await _unitOfWork.UserRepository.Get(user => user.Role != Constants.UserRole.SUPERADMIN);
            var userSchedules = users.Select(user => new UserSchedule { User = user }).ToList();
            var days = (schedule.Days as List<Day>);

            var helperCounter = 1;
            var previousDay = days[0];
            for (var dayIndex = 0; dayIndex < days.Count; dayIndex++)
            {
                var day = days[dayIndex];
                var tempIndex = dayIndex;

                if (day.IsHoliday || day.IsWeekend)
                {
                    continue;
                }
                schedule.NumOfWorkdays++;
                var maxNumberOfUsersForMorning = dayIndex % 2 == 0 ? (users.Count() - day.UsersOnHoliday.Count) / 2 : (int)Math.Ceiling((users.Count() - day.UsersOnHoliday.Count) / 2.0);
                if (dayIndex % 2 == 0)
                {
                    day = GenerateMorningSchedules(helperCounter, maxNumberOfUsersForMorning, day, userSchedules);
                    day = GenerateForenoonSchedules(day, userSchedules);
                }
                else
                {
                    var usersNotScheduledYet = GetUsersNotScheduledYet(day, userSchedules);
                    List<MorningSchedule> morningSchedules = new();
                    if (!previousDay.IsHoliday && !previousDay.IsWeekend)
                    {
                        var notScheduledForMorning = usersNotScheduledYet.Where(schedule =>
                            previousDay.UsersScheduledForForenoon.Where(user =>
                                user.User.Id == schedule.User.Id).Any() ||
                                previousDay.UsersOnHoliday.Where(user => user.User.Id == schedule.User.Id).Any()).ToList();
                        foreach (var user in notScheduledForMorning)
                        {
                            if (day.UsersScheduledForMorning.Count + morningSchedules.Count < maxNumberOfUsersForMorning)
                            {
                                morningSchedules.Add(new MorningSchedule { User = user.User });
                                usersNotScheduledYet.Remove(user);
                            }
                        }
                    }
                    if ((morningSchedules.Count == 0 || morningSchedules.Count < maxNumberOfUsersForMorning) && usersNotScheduledYet.Count != 0)
                    {
                        while (day.UsersScheduledForMorning.Count + morningSchedules.Count < maxNumberOfUsersForMorning)
                        {
                            var randomIndex = usersNotScheduledYet.Count == 0 ? 0 : _random.Next(usersNotScheduledYet.Count);
                            var randomUserSchedule = usersNotScheduledYet[randomIndex];
                            if (!morningSchedules.Select(s => s.User.Id).Any(id => id == randomUserSchedule.User.Id))
                            {
                                morningSchedules.Add(new MorningSchedule { User = randomUserSchedule.User });
                                usersNotScheduledYet.Remove(randomUserSchedule);
                            }
                        }
                    }
                    if (day.UsersScheduledForMorning.Count != 0)
                    {
                        while (day.UsersScheduledForMorning.Count < maxNumberOfUsersForMorning)
                        {
                            var randomUserSchedule = morningSchedules[_random.Next(morningSchedules.Count)];
                            day.UsersScheduledForMorning.Add(randomUserSchedule);
                            morningSchedules.Remove(randomUserSchedule);
                        }
                    }
                    else
                    {
                        foreach (var morningschedule in morningSchedules)
                        {
                            day.UsersScheduledForMorning.Add(morningschedule);
                        }
                    }
                    day = GenerateForenoonSchedules(day, userSchedules);
                }
                userSchedules = UpdateUserSchedules(day, userSchedules);
                previousDay = day;
                helperCounter++;
            }
            schedule.IsSaved = true;
            schedule.Summaries = GenerateSummary(userSchedules);
            try
            {
                schedule.WordFile = await _fileService.GenerateWordDoc(schedule, (int)Math.Ceiling(users.Count() / 2.0));
            }
            catch (Exception ex)
            {
                schedule.WordFile = null;
                throw new BusinessException { ErrorCode = 599, ErrorMessages = new List<string> { "Couldn't create Word file!" } };
            }
            await _unitOfWork.ScheduleRepository.Add(schedule);
            _unitOfWork.Save();
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
            var schedule = await _unitOfWork.ScheduleRepository.GetByDate(year, month);
            if (schedule is not null)
            {
                return schedule;
            }
            return await CreateBlankScheduleWithRequests(year, month);
        }

        public async Task<bool> CheckSchedule(int year, int month)
        {
            var schedule = await _unitOfWork.ScheduleRepository.FindAsync(schedule => schedule.Year == year && schedule.Month == month);
            if (schedule.Any())
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Deletes a schedule from the database.
        /// </summary>
        /// <param name="id">The ID of the schedule</param>
        /// <returns></returns>
        public async Task<MonthlySchedule> DeleteSchedule(object id)
        {
            var schedule = (await _unitOfWork.ScheduleRepository.Get(schedule => schedule.Id == (Guid)id, null, "WordFile")).FirstOrDefault();
            if (schedule is null)
            {
                return null;
            }
            _unitOfWork.ScheduleRepository.Delete(schedule);
            _unitOfWork.Save();
            return schedule;
        }

        /// <summary>
        /// Updates an existing schedule in the database.
        /// </summary>
        /// <param name="id">ID of the schedule</param>
        /// <param name="updateScheduleDto">DTO from the request</param>
        /// <returns></returns>
        public async Task<MonthlySchedule> UpdateSchedule(object id, List<DayDto> dayDtos)
        {
            var schedule = (await _unitOfWork.ScheduleRepository.Get(schedule => schedule.Id == (Guid)id, null, "Summaries,WordFile")).FirstOrDefault();
            if (schedule is null)
            {
                return null;
            }
            var days = await _unitOfWork.DayRepository.Get(day => day.Date.Year == schedule.Year && day.Date.Month == schedule.Month,
                null, "UsersScheduledForMorning,UsersScheduledForForenoon,UsersOnHoliday");
            var users = await _unitOfWork.UserRepository.Get(user => user.Role != Constants.UserRole.SUPERADMIN);
            var userSchedules = users.Select(user => new UserSchedule { User = user }).ToList();
            foreach (var dayDto in dayDtos)
            {
                var dayToDelete = days.Where(day => day.Date == dayDto.Date).FirstOrDefault();
                if (dayToDelete is not null)
                {
                    _unitOfWork.DayRepository.Delete(dayToDelete);
                    schedule.Days.Remove(dayToDelete);
                }
                var morningSchedules = dayDto.UsersScheduledForMorning.Select(schedule => new MorningSchedule
                {
                    User = users.Where(user => user.Id == schedule.Id).FirstOrDefault(),
                    IsRequest = schedule.IsRequest,

                }).ToList();
                var forenoonSchedules = dayDto.UsersScheduledForForenoon.Select(schedule => new Forenoonschedule
                {
                    User = users.Where(user => user.Id == schedule.Id).FirstOrDefault(),
                    IsRequest = schedule.IsRequest,

                }).ToList();
                var holidaySchedules = dayDto.UsersOnHoliday.Select(schedule => new HolidaySchedule
                {
                    User = users.Where(user => user.Id == schedule.Id).FirstOrDefault()
                }).ToList();
                var newDay = new Day
                {
                    Date = dayDto.Date,
                    UsersOnHoliday = holidaySchedules,
                    UsersScheduledForMorning = morningSchedules,
                    UsersScheduledForForenoon = forenoonSchedules
                };
                schedule.Days.Add(newDay);
            }
            foreach (var day in schedule.Days)
            {
                if (day.IsHoliday || day.IsWeekend) continue;
                UpdateUserSchedules(day, userSchedules);
            }
            foreach (var summary in schedule.Summaries)
            {
                _unitOfWork.SummaryRepository.Delete(summary);
            }
            schedule.Summaries = GenerateSummary(userSchedules);
            _unitOfWork.Save();
            await UpdateWordFile(schedule.Year, schedule.Month, (int)Math.Ceiling(users.Count() / 2.0));
            return schedule;
        }

        private async Task UpdateWordFile(int year, int month, int max)
        {
            var schedule = await _unitOfWork.ScheduleRepository.GetByDate(year, month);
            WordFile newWordFile = new();

            try
            {
                newWordFile = await _fileService.GenerateWordDoc(schedule, max);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                throw new BusinessException { ErrorCode = 599, ErrorMessages = new List<string> { "Couldn't create Word file! Changes are not saved!" } };
            }
            if (schedule.WordFile is not null)
            {
                schedule.WordFile.FilePath = newWordFile.FilePath;
                schedule.WordFile.FileName = newWordFile.FileName;
            }
            else
            {
                schedule.WordFile = newWordFile;
            }
            _unitOfWork.Save();
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
            var requests = await _unitOfWork.RequestRepository.Get(request => request.Date.Year == year && request.Date.Month == month && request.User.Role != Constants.UserRole.SUPERADMIN, null, "User");
            for (var i = 0; i < schedule.Days.Count; i++)
            {
                var date = DateTime.Parse($"{year}-{month}-{i + 1}");
                var day = (schedule.Days as List<Day>)[i];
                if (day.IsWeekend)
                {
                    continue;
                }
                day.UsersScheduledForMorning = requests.Where(request => request.Date == date && request.Type == RequestType.MORNING)
                    .Select(request => new MorningSchedule { User = request.User, IsRequest = true })
                    .ToList();
                day.UsersScheduledForForenoon = requests.Where(request => request.Date == date && request.Type == RequestType.FORENOON)
                    .Select(request => new Forenoonschedule { User = request.User, IsRequest = true })
                    .ToList();
                day.UsersOnHoliday = requests.Where(request => request.Date == date && request.Type == RequestType.HOLIDAY)
                    .Select(request => new HolidaySchedule { User = request.User })
                    .ToList();
            }

            return schedule;
        }

        private async Task<List<Day>> GetDays(int year, int month)
        {
            var holidays = await _unitOfWork.HolidayRepository.Get(holiday => (holiday.Year == year || holiday.IsFix) && holiday.Month == month);
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
            List<Summary> summaries = userSchedules.Select(userSchedule =>
                new Summary
                {
                    UserId = userSchedule.User.Id,
                    Name = userSchedule.User.Name,
                    Morning = userSchedule.NumOfMorningSchedules - userSchedule.NumOfHolidays,
                    Forenoon = userSchedule.NumOfForenoonSchedules,
                    Holiday = userSchedule.NumOfHolidays
                }).ToList();
            return summaries;
        }

        private Day GenerateForenoonSchedules(Day day, List<UserSchedule> userSchedules)
        {
            var usersNotScheduledYet = GetUsersNotScheduledYet(day, userSchedules);
            List<Forenoonschedule> forenoonschedules = usersNotScheduledYet
                .Select(schedules => new Forenoonschedule { User = schedules.User })
                .ToList();
            foreach (var schedule in forenoonschedules)
            {
                day.UsersScheduledForForenoon.Add(schedule);
            }
            return day;
        }

        private Day GenerateMorningSchedules(int helperCounter, int maxUsersForMorning, Day day, List<UserSchedule> userSchedules)
        {
            var usersNotScheduledYet = GetUsersNotScheduledYet(day, userSchedules);
            var possibleUsers = usersNotScheduledYet.Where(userSchedule => userSchedule.NumOfMorningSchedules < helperCounter).ToList();
            while (day.UsersScheduledForMorning.Count < maxUsersForMorning)
            {
                var randomUserSchedule = possibleUsers[_random.Next(possibleUsers.Count)];
                day.UsersScheduledForMorning.Add(new MorningSchedule { User = randomUserSchedule.User });
                possibleUsers.Remove(randomUserSchedule);
            }
            return day;
        }

        private List<UserSchedule> UpdateUserSchedules(Day day, List<UserSchedule> userSchedules)
        {
            foreach (var userSchedule in userSchedules)
            {
                var holidayCount = day.UsersOnHoliday.Where(user => user.User.Id == userSchedule.User.Id).ToList().Count;
                userSchedule.NumOfMorningSchedules += day.UsersScheduledForMorning.Where(user => user.User.Id == userSchedule.User.Id).ToList().Count;
                userSchedule.NumOfMorningSchedules += holidayCount;
                userSchedule.NumOfForenoonSchedules += day.UsersScheduledForForenoon.Where(user => user.User.Id == userSchedule.User.Id).ToList().Count;
                userSchedule.NumOfHolidays += holidayCount;
            }
            return userSchedules;
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