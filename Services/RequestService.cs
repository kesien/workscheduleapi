using System.Runtime.CompilerServices;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WorkScheduleMaker.Data.Repositories;
using WorkScheduleMaker.Dtos;
using WorkScheduleMaker.Entities;

namespace WorkScheduleMaker.Services
{
    public class RequestService : IRequestService
    {
        private readonly IRepository<Request> _repository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IHolidayService _holidayService;

        public RequestService(IRepository<Request> repository, IMapper mapper, UserManager<User> userManager, IHolidayService holidayService)
        {
            _repository = repository;
            _mapper = mapper;
            _userManager = userManager;
            _holidayService = holidayService;
        }

        public async Task<Request> CreateRequest(string userId, RequestDto requestDto)
        {
            var user = await _userManager.Users.Include(user => user.Requests).FirstOrDefaultAsync(user => user.Id == userId);
            var requestIsInvalid = await CheckRequest(userId, requestDto);
            if (user is null || requestIsInvalid)
            {
                return null;
            }
            var request = _mapper.Map<Request>(requestDto);
            user.Requests.Add(request);
            await _userManager.UpdateAsync(user);
            return request;
        }

        public async Task<bool> DeleteRequest(Guid id)
        {
            var request = await GetRequestById(id);
            if (request is null) 
            {
                return false;
            }
            _repository.Delete(request);
            await _repository.SaveAsync();
            return true;
        }

        public async Task<IEnumerable<Request>> GetAllRequests()
        {
            var requests = await _repository.GetAllAsync();
            return requests;
        }

        public async Task<IEnumerable<Request>> GetAllRequestsForMonth(int year, int month)
        {
            var requests = await _repository.FindAllAsync(request => request.Date.Year == year && request.Date.Month == month);
            return requests;
        }

        public async Task<IEnumerable<Request>> GetAllRequestsForUser(string userId, int year, int month)
        {
            if (year == 0) {
                year = DateTime.Now.Year;
            }
            if (month == 0) {
                month = DateTime.Now.Month;
            }
            var requests = await _repository.FindAllAsync(request => request.UserId == userId && request.Date.Year == year && request.Date.Month == month);
            requests = requests.OrderBy(request => request.Date);

            return requests;
        }

        public async Task<IEnumerable<Request>> GetAllRequestsForYear(int year)
        {
            var requests = await _repository.FindAllAsync(request => request.Date.Year == year);
            return requests;
        }

        public async Task<Request> GetRequestById(Guid id)
        {
            var request = await _repository.GetById(id);
            return request;
        }

        private async Task<bool> CheckRequest(string userId, RequestDto requestDto)
        {
            var year = DateTime.Now.Year;
            var month = DateTime.Now.Month;
            var date = new DateTime(year, month, 1, 0, 0, 0);
            var requestDate = new DateTime(requestDto.Date.Year, requestDto.Date.Month, 1, 0, 0, 0);
            int result = DateTime.Compare(requestDate, date);
            var requests = await _repository.FindAllAsync(request => request.UserId == userId);
            var request = requests.Where(request => 
                request.Date == requestDto.Date).Any();
            var isWeekend = IsWeekend(requestDto.Date);
            var isHoliday = await IsHoliday(requestDto.Date);
            return request || result == -1 || isWeekend || isHoliday;
        }

        private bool IsWeekend(DateTime date) => date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;

        private async Task<bool> IsHoliday(DateTime date)
        {
            var holidays = await _holidayService.GetAll();
            var month = date.Month;
            var day = date.Day;
            return holidays.Where(holiday => holiday.Month == month && holiday.Day == day).Any();
        }
    }
}