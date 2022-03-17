using MediatR;
using WorkSchedule.Api.Queries.Files;
using WorkSchedule.Application.Data;
using WorkSchedule.Application.Exceptions;
using WorkSchedule.Application.Services.DropboxService;

namespace WorkSchedule.Application.QueryHandlers.Files
{
    public class GetFileByFileNameQueryHandler : IRequestHandler<GetFileByFileNameQuery, (string, byte[])>
    {
        private readonly IUnitOfWork _uow;
        private readonly IDropboxService _dropbox;

        public GetFileByFileNameQueryHandler(IUnitOfWork uow, IDropboxService dropbox)
        {
            _uow = uow;
            _dropbox = dropbox;
        }

        public async Task<(string, byte[])> Handle(GetFileByFileNameQuery request, CancellationToken cancellationToken)
        {
            var file = await _uow.WordFileRepository.GetByID(request.Id);
            if (file is null)
            {
                throw new BusinessException { ErrorCode = 599, ErrorMessages = new List<string> { "The requested file doesn't exists!" } };
            }
            var bytes = await _dropbox.GetFile($"{file.FileName}");
            if (bytes is null)
            {
                throw new BusinessException { ErrorCode = 599, ErrorMessages = new List<string> { "The requested file doesn't exists on DropBox!" } };
            }
            return (file.FileName, bytes);
        }
    }
}
