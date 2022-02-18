using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorkSchedule.Api.Queries.Files;
using WorkSchedule.Application.Data;
using WorkSchedule.Application.Persistency.Entities;
using WorkSchedule.Application.QueryHandlers.Files;
using WorkSchedule.Application.Services.DropboxService;
using WorkSchedule.UnitTests.MockRepositories;
using Xunit;
using FluentAssertions;

namespace WorkSchedule.UnitTests.QueryHandlerTests.Files
{
    public class GetFileByFileNameQueryHandlerTests
    {
        private readonly IUnitOfWork _uow;
        private IDropboxService _dropboxService;
        public GetFileByFileNameQueryHandlerTests()
        {
            List<WordFile> entities = GenerateEntities();
            var fileRepo = new MockGenericRepository<WordFile>(entities);
            var uowMock = MockUnitOfWork.GetUnitOfWorkMock();
            uowMock.Setup(r => r.WordFileRepository).Returns(fileRepo.GetGenericRepository().Object);
            _uow = uowMock.Object;
        }

        [Fact(DisplayName = "Valid Id Should Return a WordFile")]
        public async Task ValidId_Should_Return_A_WordFile()
        {
            string testString = "teststring";
            byte[] bytes = Encoding.ASCII.GetBytes(testString);
            var dropboxServiceMock = new Mock<IDropboxService>();
            _dropboxService = dropboxServiceMock.Object;
            dropboxServiceMock.Setup(r => r.GetFile(It.IsAny<string>())).ReturnsAsync(bytes);
            var query = new GetFileByFileNameQuery { Id = Guid.Parse("7c1f48c0-97fe-4b54-9b4c-73cad8c34634") };
            var queryHandler = new GetFileByFileNameQueryHandler(_uow, _dropboxService);
            var (fileName, file) = await queryHandler.Handle(query, CancellationToken.None);
            fileName.Should().NotBeNullOrEmpty();
            fileName.Should().Be("test.docx");
            file.Should().NotBeNull();
            file.Should().BeSameAs(bytes);
        }

        [Fact(DisplayName = "Invalid Id Should Throw Exception")]
        public async Task InValid_Id_Should_Throw_Exception()
        {
            string testString = "teststring";
            byte[] bytes = Encoding.ASCII.GetBytes(testString);
            var dropboxServiceMock = new Mock<IDropboxService>();
            _dropboxService = dropboxServiceMock.Object;
            dropboxServiceMock.Setup(r => r.GetFile(It.IsAny<string>())).ReturnsAsync(bytes);
            var query = new GetFileByFileNameQuery { Id = Guid.Parse("7c1f48c0-97fe-4b54-0000-73cad8c34634") };
            var queryHandler = new GetFileByFileNameQueryHandler(_uow, _dropboxService);
            await queryHandler.Awaiting(y => y.Handle(query, CancellationToken.None)).Should()
                .ThrowAsync<ApplicationException>()
                .WithMessage("The requested file doesn't exists!");
        }

        [Fact(DisplayName = "Valid Id Should Throw Exception If Not Found On Dropbox")]
        public async Task Valid_Id_Should_Throw_Exception_If_Not_Found_On_Dropbox()
        {
            byte[] bytes = null;
            var dropboxServiceMock = new Mock<IDropboxService>();
            _dropboxService = dropboxServiceMock.Object;
            dropboxServiceMock.Setup(r => r.GetFile(It.IsAny<string>())).ReturnsAsync(bytes);
            var query = new GetFileByFileNameQuery { Id = Guid.Parse("7c1f48c0-97fe-4b54-9b4c-73cad8c34634") };
            var queryHandler = new GetFileByFileNameQueryHandler(_uow, _dropboxService);
            await queryHandler.Awaiting(y => y.Handle(query, CancellationToken.None)).Should()
                .ThrowAsync<ApplicationException>()
                .WithMessage("The requested file doesn't exists on DropBox!");
        }

        private List<WordFile> GenerateEntities()
        {
            var entities = new List<WordFile>
            {
                new WordFile
                {
                    FileName = "test.docx",
                    FilePath = "./test.docx",
                    Id = Guid.Parse("7c1f48c0-97fe-4b54-9b4c-73cad8c34634")
                }
            };
            return entities;
        }
    }
}
