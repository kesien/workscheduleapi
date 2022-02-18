using System.Linq.Expressions;
using AutoMapper;
using WorkSchedule.Application.Data;
using WorkSchedule.Application.Persistency.Entities;
using WorkSchedule.Api.Constants;

namespace WorkSchedule.Application.Services.RequestService
{
    public class RequestService : IRequestService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RequestService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Request> CreateRequest(string userId, DateTime date, RequestType type)
        {
            var requestIsInvalid = await CheckRequest(userId, date, type);
            var user = (await _unitOfWork.UserRepository.Get(user => user.Id == userId, null, "", true)).FirstOrDefault();
            if (user is null || requestIsInvalid)
            {
                return null;
            }
            var request = new Request() { Date = date, Type = type };
            request.User = user;
            await _unitOfWork.RequestRepository.Add(request);
            _unitOfWork.Save();
            return request;
        }

        public async Task<bool> DeleteRequest(object id)
        {
            var request = await _unitOfWork.RequestRepository.GetByID(id);
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
            var requests = await _unitOfWork.RequestRepository.Get(null, null, "User");
            return requests;
        }

        public async Task<IEnumerable<Request>> GetAllRequestsForMonth(int year, int month)
        {
            var requests = await _unitOfWork.RequestRepository.Get(request => request.Date.Year == year && request.Date.Month == month, null, "User");
            return requests;
        }

        public async Task<IEnumerable<Request>> GetAllRequestsForUserByDate(string userId, int year, int month)
        {
            if (year == 0) {
                year = DateTime.Now.Year;
            
            }
            Expression<Func<Request, bool>> filter = request => request.User.Id == userId && request.Date.Year == year && request.Date.Month == month;

            if (month == 0) {
                filter = request => request.User.Id == userId && request.Date.Year == year;
            }
            var requests = await _unitOfWork.RequestRepository.Get(filter, request => request.OrderBy(r =>r.Date), "User");
            return requests;
        }

        public async Task<IEnumerable<Request>> GetAllRequestsForUser(string userId) {
            var requests = await _unitOfWork.RequestRepository.Get(request => request.User.Id == userId, request => request.OrderBy(r => r.Date));
            return requests;
        }

        public async Task<IEnumerable<Request>> GetAllRequestsForYear(int year)
        {
            var requests = await _unitOfWork.RequestRepository.Get(request => request.Date.Year == year, null, "User");
            return requests;
        }

        public async Task<Request> GetRequestById(object id)
        {
            var request = await _unitOfWork.RequestRepository.GetByID(id);
            return request;
        }

        private async Task<bool> CheckRequest(string userId, DateTime requestDate, RequestType type)
        {
            var date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
            var reqDate = new DateTime(requestDate.Date.Year, requestDate.Date.Month, 1, 0, 0, 0);
            int result = DateTime.Compare(requestDate, date);
            var request = (await _unitOfWork.RequestRepository.Get(request => request.User.Id == userId && request.Date == requestDate)).Any();
            var isWeekend = IsWeekend(requestDate);
            var isHoliday = await IsHoliday(requestDate);
            var isThereAScheduleAlready = await IsThereAScheduleForThisMonth(requestDate.Date.Year, requestDate.Date.Month);
            return request || result == -1 || isWeekend || isHoliday || isThereAScheduleAlready;
        }

        private async Task<bool> IsThereAScheduleForThisMonth(int year, int month)
        {
            var result = await _unitOfWork.ScheduleRepository.Get(schedule => schedule.Year == year && schedule.Month == month, null, "", true);
            return result.Any();
        }

        private bool IsWeekend(DateTime date) => date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;

        private async Task<bool> IsHoliday(DateTime date)
        {
            var month = date.Month;
            var day = date.Day;
            var year = date.Year;
            var isHoliday = (await _unitOfWork.HolidayRepository.Get(holiday => (holiday.Year == year || holiday.IsFix) && 
                holiday.Month == month && holiday.Day == day)).Any();
            return isHoliday;
        }
    }
}