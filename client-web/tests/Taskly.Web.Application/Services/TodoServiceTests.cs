using AutoMapper;
using Moq;
using Taskly.Web.Application.Exceptions;
using Taskly.Web.Application.Mappers;
using Taskly.Web.Application.Model;
using Taskly.Web.Application.Services;
using Taskly.Web.Application.State.Interfaces;
using Taskly.Web.Exceptions;
using Taskly.Web.Infrastructure.DTO;
using Taskly.Web.Infrastructure.Repositories.Interfaces;
using UnauthorizedAccessException = Taskly.Web.Application.Exceptions.UnauthorizedAccessException;

namespace Taskly.Web.Application.Tests.Services
{
    [TestClass]
    public class TodoServiceTests
    {
        TodoService service;
        Mock<ITodoRepository> repositoryMock;
        Mock<IAuthState> mockAuthState;

        [TestInitialize]
        public void Initialize()
        {
            // Arrange
            var mapper = new MapperConfiguration(mc => 
            {
                mc.AddProfile<TodoMapperProfile>();
            });

            repositoryMock = new Mock<ITodoRepository>();
            mockAuthState = new Mock<IAuthState>();
            service = new TodoService(repositoryMock.Object, mapper.CreateMapper(), mockAuthState.Object);
        }

        [TestMethod]
        public async Task When_CreateAsync_IsCalledAndNoExceptionThrowsByRepository_ThenReturnATodoModel()
        {
            // Arrange
            Guid resultId = Guid.NewGuid();
            repositoryMock.Setup(rm => rm.Create(It.IsAny<TodoDTO>()))
                .ReturnsAsync(new TodoDTO(resultId.ToString(), "", "", Guid.NewGuid(), false, null, null));

            // Act
            TodoModel result = await service.CreateAsync(new TodoModel
            {
                Content = "",
                CreatedOn = null,
                DeletedOn = null,
                Id = resultId.ToString(),
                IsDone = false,
                Name = "",
                UserId = Guid.NewGuid()
            });

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

        [TestMethod]
        public async Task When_GetConnectedUserTodos_IsCalled_ThenRepositoryIsCalledWithConnectedUserID()
        {
            // Arrange
            Guid connectedUserId = Guid.NewGuid();
            mockAuthState.SetupGet(mas => mas.UserId)
                .Returns(connectedUserId);

            // Act
            await service.GetConnectedUserTodos();

            // Assert
            repositoryMock.Verify(rm => rm.GetAllForUser(connectedUserId), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedAccessException))]
        public async Task When_GetConnectedUserTodos_AndRepositoryThrowUnauthorizedAccessException_ThenLetItPass()
        {
            // Arrange
            mockAuthState.SetupGet(mas => mas.UserId)
                .Returns(Guid.NewGuid());
            repositoryMock.Setup(rm => rm.GetAllForUser(It.IsAny<Guid>()))
                .ThrowsAsync(new UnauthorizedAccessException(""));

            // Act
            await service.GetConnectedUserTodos();
        }

        [TestMethod]
        [ExpectedException(typeof(ServiceException))]
        public async Task When_DeleteAsync_IsCalledwithANonGuidString_ThenThrowsArgumentException()
        {
            // Act
            await service.DeleteAsync("");
        }

        [TestMethod]
        public async Task When_DeleteAsync_WithCorrectGuid_ThenCallRepostiroyDeleteMethod()
        {
            // Arrange
            Guid toDeleteGuid = Guid.NewGuid();
            // Act
            await service.DeleteAsync(toDeleteGuid.ToString());

            // Assert
            repositoryMock.Verify(rm => rm.Delete(toDeleteGuid), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(ServiceException))]
        public async Task When_DeleteAsync_RepositoryThrowsNotFoundException_ThenServiceThrowsServiceException()
        {
            // Arrange
            Guid toDeleteGuid = Guid.NewGuid();
            repositoryMock.Setup(rm => rm.Delete(It.IsAny<Guid>()))
                .ThrowsAsync(new NotFoundException(""));

            // Act
            await service.DeleteAsync(toDeleteGuid.ToString());
        }

        [TestMethod]
        public async Task When_UpdateAsync_RepositoryThrowsExpectedException_ThenServiceThrowsServiceException()
        {
            // Arrange
            Guid unauthorizedId = Guid.NewGuid();
            Guid notFoundId = Guid.NewGuid();
            Guid notValidatId = Guid.NewGuid();
            repositoryMock.Setup(rm => rm.Update(unauthorizedId, It.IsAny<TodoDTO>()))
                .ThrowsAsync(new UnauthorizedAccessException(""));
            repositoryMock.Setup(rm => rm.Update(notFoundId, It.IsAny<TodoDTO>()))
                .ThrowsAsync(new NotFoundException(""));
            repositoryMock.Setup(rm => rm.Update(notValidatId, It.IsAny<TodoDTO>()))
                .ThrowsAsync(new ValidationException(""));

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ServiceException>(async () => await service.UpdateAsync(unauthorizedId.ToString(), (TodoModel)null));
            await Assert.ThrowsExceptionAsync<ServiceException>(async () => await service.UpdateAsync(notFoundId.ToString(), (TodoModel)null));
            await Assert.ThrowsExceptionAsync<ServiceException>(async () => await service.UpdateAsync(notValidatId.ToString(), (TodoModel)null));
        }
    }
}
