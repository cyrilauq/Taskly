using AutoMapper;
using Moq;
using Taskly.Client.Application.Exceptions;
using Taskly.Client.Application.Mappers;
using Taskly.Client.Application.Model;
using Taskly.Client.Application.Services;
using Taskly.Client.Application.State.Interfaces;
using Taskly.Client.Domain.DTO;
using Taskly.Client.Domain.Repositories.Interfaces;
using UnauthorizedAccessException = Taskly.Client.Application.Exceptions.UnauthorizedAccessException;

namespace Taskly.Client.Application.Tests.Services
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
        [ExpectedException(typeof(Exceptions.UnauthorizedAccessException))]
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

        [TestMethod]
        public async Task When_SaveAsync_WhitAnEmptyGuid_ThenCallCreateMethodOfRepository()
        {
            // Arrange
            string guid = Guid.Empty.ToString();
            TodoModel model = new TodoModel() { Id = guid };

            // Act
            await service.SaveAsync(model);

            // Assert
            repositoryMock.Verify(rm => rm.Create(It.IsAny<TodoDTO>()), Times.Once());
        }

        [TestMethod]
        [DataRow("")]
        [DataRow("fdlkhbfdlksjhgdfhg")]
        public async Task When_SaveAsync_WhitNonValidGuid_ThenCallCreateMethodOfRepository(string guid)
        {
            // Arrange
            TodoModel model = new TodoModel() { Id = guid };

            // Act
            await service.SaveAsync(model);

            // Assert
            repositoryMock.Verify(rm => rm.Create(It.IsAny<TodoDTO>()), Times.Once());
        }

        [TestMethod]
        public async Task When_SaveAsync_WhitNonEmptyGuid_ThenCallUpdateteAsyncMethodOfRepository()
        {
            // Arrange
            string guid = Guid.NewGuid().ToString();
            TodoModel model = new TodoModel() { Id = guid };

            // Act
            await service.SaveAsync(model);

            // Assert
            repositoryMock.Verify(rm => rm.Update(It.IsAny<Guid>(), It.IsAny<TodoDTO>()), Times.Once());
        }
    }
}
