using Microsoft.EntityFrameworkCore;
using WorkScheduleMaker.Entities;

namespace WorkScheduleMaker.Data.Repositories
{
    public class RequestRepository : Repository<ApplicationDbContext, Request>
    {
        public RequestRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
