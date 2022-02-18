using WorkSchedule.Api.Constants;
using WorkSchedule.Api.Dtos;
using WorkSchedule.Application.Persistency.Entities;

namespace WorkSchedule.Application.Services.RequestService
{
    public interface IRequestService
    {
        public Task<Request> CreateRequest(string userId, DateTime date, RequestType type);
        public Task<bool> DeleteRequest(object id);
        public Task<Request> GetRequestById(object id);
        public Task<IEnumerable<Request>> GetAllRequests();
        public Task<IEnumerable<Request>> GetAllRequestsForMonth(int year, int month);
        public Task<IEnumerable<Request>> GetAllRequestsForUser(string userId);
        public Task<IEnumerable<Request>> GetAllRequestsForUserByDate(string userId, int year, int month);
        public Task<IEnumerable<Request>> GetAllRequestsForYear(int year);
    }
}