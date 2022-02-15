using MediatR;

namespace WorkSchedule.Api.Queries.Files
{
    public class GetFileByFileNameQuery : IRequest<(string, byte[])>
    {
        public Guid Id { get; set; }
    }
}
