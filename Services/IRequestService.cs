using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkScheduleMaker.Dtos;
using WorkScheduleMaker.Entities;

namespace WorkScheduleMaker.Services
{
    public interface IRequestService
    {
        public Task<Request> CreateRequest(string userId, RequestDto requestDto);
        public Task<bool> DeleteRequest(Guid id);
        public Task<Request> GetRequestById(Guid id);
        public Task<IEnumerable<Request>> GetAllRequests();
        public Task<IEnumerable<Request>> GetAllRequestsForMonth(int year, int month);
        public Task<IEnumerable<Request>> GetAllRequestsForUser(string userId, int year, int month);
        public Task<IEnumerable<Request>> GetAllRequestsForYear(int year);
    }
}