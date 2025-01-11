using AutoMapper;
using Moq;
using Taskly.Web.Application.Exceptions;
using Taskly.Web.Application.Mappers;
using Taskly.Web.Application.Model;
using Taskly.Web.Application.Services;
using Taskly.Web.Exceptions;
using Taskly.Web.Infrastructure.DTO;
using Taskly.Web.Infrastructure.Repositories.Interfaces;

namespace Taskly.Web.Application.Tests.Services
{
    [TestClass]
    public class TodoServiceTests
    {
        TodoService service;
        Mock<ITodoRepository> repositoryMock;

        [TestInitialize]
        public void Initialize()
        {
            // Arrange
            var mapper = new MapperConfiguration(mc => 
            {
                mc.AddProfile<TodoMapperProfile>();
            });

            repositoryMock = new Mock<ITodoRepository>();
            service = new TodoService(repositoryMock.Object, mapper.CreateMapper());
        }

        [TestMethod]
        public async Task When_CreateAsync_IsCalledAndNoExceptionThrowsByRepository_ThenReturnATodoModel()
        {
            // Arrange
            Guid resultId = Guid.NewGuid();
            repositoryMock.Setup(rm => rm.Create(It.IsAny<TodoDTO>()))
                .ReturnsAsync(new TodoDTO(resultId.ToString(), "", "", Guid.NewGuid(), false, null, null));

            // Act
            TodoModel result = await service.CreateAsync(new TodoModel("", "", "", Guid.NewGuid(), false, null, null));

            // Assert
            Assert.AreEqual(resultId.ToString(), result.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ServiceException))]
        public async Task When_RepositoryThrowsValidationException_ThenServiceThrowsServiceException()
        {
            // Arrange
            repositoryMock.Setup(rm => rm.Create(It.IsAny<TodoDTO>()))
                .ThrowsAsync(new ValidationException(""));

            // Act
            await service.CreateAsync(It.IsAny<TodoModel>());
        }
    }
}
