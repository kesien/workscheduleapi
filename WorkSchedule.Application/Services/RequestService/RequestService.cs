using WorkSchedule.Api.Constants;
using WorkSchedule.Application.Data;
using WorkSchedule.Application.Persistency.Entities;

namespace WorkSchedule.Application.Services.RequestService
{
    public class RequestService : IRequestService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RequestService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Request> CreateRequest(Guid userId, DateTime date, RequestType type)
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

        public async Task<IEnumerable<Request>> GetAllRequestsForUserByDate(Guid userId, int year, int month, string type)
        {
            var requests = await _unitOfWork.RequestRepository.Get(request => request.User.Id == userId);
            if (year != 0)
            {
                requests = requests.Where(request => request.Date.Year == year).ToList();
            }
            if (month != 0)
            {
                requests = requests.Where(request => request.Date.Month == month).ToList();
            }
            int requestType;
            if (type != "all")
            {
                int.TryParse(type, out requestType);
                requests = requests.Where(request => request.Type == (RequestType)requestType).ToList();
            }
            return requests;
        }

        public async Task<IEnumerable<Request>> GetAllRequestsForUser(Guid userId)
        {
            var requests = await _unitOfWork.RequestRepository.Get(request => request.User.Id == userId, request => request.OrderBy(r => r.Date), "User");
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

        private async Task<bool> CheckRequest(Guid userId, DateTime requestDate, RequestType type)
        {
            var request = (await _unitOfWork.RequestRepository.Get(request => request.User.Id == userId && request.Date == requestDate)).Any();
            var isHoliday = await IsHoliday(requestDate);
            var isThereAScheduleAlready = await IsThereAScheduleForThisMonth(requestDate.Date.Year, requestDate.Date.Month);
            return request || isHoliday || isThereAScheduleAlready;
        }

        private async Task<bool> IsThereAScheduleForThisMonth(int year, int month)
        {
            var result = await _unitOfWork.ScheduleRepository.Get(schedule => schedule.Year == year && schedule.Month == month, null, "", true);
            return result.Any();
        }

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