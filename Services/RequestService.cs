using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WorkScheduleMaker.Data;
using WorkScheduleMaker.Dtos;
using WorkScheduleMaker.Entities;

namespace WorkScheduleMaker.Services
{
    public class RequestService : IRequestService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public RequestService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Request> CreateRequest(string userId, RequestDto requestDto)
        {
            var requestIsInvalid = await CheckRequest(userId, requestDto);
            var user = _unitOfWork.UserRepository.Get(user => user.Id == userId, null, "", true).FirstOrDefault();
            if (user is null || requestIsInvalid)
            {
                return null;
            }
            var request = _mapper.Map<Request>(requestDto);
            request.User = user;
            _unitOfWork.RequestRepository.Add(request);
            _unitOfWork.Save();
            return request;
        }

        public async Task<bool> DeleteRequest(Guid id)
        {
            var request = _unitOfWork.RequestRepository.GetByID(id);
            if (request is null) 
            {
                return false;
            }
            _unitOfWork.RequestRepository.Delete(request);
            _unitOfWork.Save();
            return true;
        }

        public async Task<IEnumerable<Request>> GetAllRequests()
        {
            var requests = _unitOfWork.RequestRepository.Get(null, null, "User");
            return requests;
        }

        public async Task<IEnumerable<Request>> GetAllRequestsForMonth(int year, int month)
        {
            var requests = _unitOfWork.RequestRepository.Get(request => request.Date.Year == year && request.Date.Month == month, null, "User");
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
            var requests = _unitOfWork.RequestRepository.Get(request => request.User.Id == userId && request.Date.Year == year && request.Date.Month == month, null, "User");
            requests = requests.OrderBy(request => request.Date);

            return requests;
        }

        public async Task<IEnumerable<Request>> GetAllRequestsForYear(int year)
        {
            var requests = _unitOfWork.RequestRepository.Get(request => request.Date.Year == year, null, "user");
            return requests;
        }

        public async Task<Request> GetRequestById(Guid id)
        {
            var request = _unitOfWork.RequestRepository.GetByID(id);
            return request;
        }

        private async Task<bool> CheckRequest(string userId, RequestDto requestDto)
        {
            var year = DateTime.Now.Year;
            var month = DateTime.Now.Month;
            var date = new DateTime(year, month, 1, 0, 0, 0);
            var requestDate = new DateTime(requestDto.Date.Year, requestDto.Date.Month, 1, 0, 0, 0);
            int result = DateTime.Compare(requestDate, date);
            var request = _unitOfWork.RequestRepository.Get(request => request.User.Id == userId && request.Date == requestDto.Date).Any();
            var isWeekend = IsWeekend(requestDto.Date);
            var isHoliday = await IsHoliday(requestDto.Date);
            var isThereAScheduleAlready = await IsThereAScheduleForThisMonth(requestDto.Date.Year, requestDto.Date.Month);
            return request || result == -1 || isWeekend || isHoliday || isThereAScheduleAlready;
        }

        private async Task<bool> IsThereAScheduleForThisMonth(int year, int month)
        {
            return _unitOfWork.ScheduleRepository.Get(schedule => schedule.Year == year && schedule.Month == month, null, "", true).Any();
        }

        private bool IsWeekend(DateTime date) => date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;

        private async Task<bool> IsHoliday(DateTime date)
        {
            var month = date.Month;
            var day = date.Day;
            var isHoliday = _unitOfWork.HolidayRepository.Get(holiday => holiday.Month == month && holiday.Day == day).Any();
            return isHoliday;
        }
    }
}