using AutoMapper;
using FluentAssertions;
using FluentValidation;
using Moq;
using TodoList.Application.Features.Todo.Commands.Mark;
using TodoList.Application.Mappers;
using TodoList.Application.Models;
using TodoList.Application.Services.Exceptions;
using TodoList.Application.Services.Interfaces;
using TodoList.Domain.IRepository;
using UnauthorizedAccessException = TodoList.Application.Services.Exceptions.UnauthorizedAccessException;

namespace TodoList.Application.Tests;

[TestClass]
public class MarkCommandHandlerTests
{
    private IValidator<MarkCommand> _validator;
    private Mock<ITodoRepository> _mockedTodoRepository;
    private Mock<IUserContext> _mockedUserContext;
    private IMapper _mapper;
    private MarkCommandHandler _handler;
    private Guid connectedUserUid = Guid.NewGuid();

    [TestInitialize]
    public void TestCaseSetUp()
    {
        _mockedTodoRepository = new Mock<ITodoRepository>();
        _mockedUserContext = new Mock<IUserContext>();

        _mockedUserContext.Setup(muc => muc.UserId).Returns(connectedUserUid);
        _mockedUserContext.Setup(muc => muc.IsAuthenticated).Returns(true);

        _validator = new MarkCommandValidator();
        _mapper = new MapperConfiguration(cf =>
        {
            cf.AddProfile(new TodoMapper());
        }).CreateMapper();

        _handler = new(_mockedTodoRepository.Object, _mockedUserContext.Object, _mapper, _validator);
    }

    [TestMethod]
    public async Task When_CommandContainsNoTodo_ThenRepositoryIsNotCalled()
    {
        // Act
        await _handler.Handle(new MarkCommand(), CancellationToken.None);

        // Assert
        _mockedTodoRepository.Verify(mtr => mtr.GetByIdAsync(It.IsAny<Guid>()), Times.Never());
    }

    [TestMethod]
    public async Task When_UserIsNotConnected_ThenThrowExceptions()
    {
        // Arrange
        _mockedUserContext.Setup(muc => muc.IsAuthenticated)
            .Returns(false);

        // Act
        Func<Task> action = async () => await _handler.Handle(new MarkCommand() { TodoIds = [Guid.NewGuid()], IsDone = true }, CancellationToken.None);

        // Assert
        await action.Should()
            .ThrowAsync<UnauthorizedAccessException>();
    }

    [TestMethod]
    public async Task When_CommandContainsOneTodo_ThenRepositoryIsCalledOneTime()
    {
        // Act
        _mockedTodoRepository.Setup(mtr => mtr.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(new Todo
            {
                Name = "Couocou",
                UserId = connectedUserUid,
            });
        await _handler.Handle(new MarkCommand() { TodoIds = [ Guid.NewGuid() ], IsDone = true }, CancellationToken.None);

        // Assert
        _mockedTodoRepository.Verify(mtr => mtr.GetByIdAsync(It.IsAny<Guid>()), Times.Once());
    }

    [TestMethod]
    public async Task When_UserTryMarkTodoThatsNotHis_ThenThrowException()
    {
        // Arrange
        _mockedTodoRepository.Setup(mtr => mtr.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(new Todo
            {
                Name = "Couocou",
                UserId = Guid.NewGuid(),
            });
        MarkCommand command = new MarkCommand() { TodoIds = [Guid.NewGuid()], IsDone = true };

        // Act
        Func<Task> action = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await action.Should()
            .ThrowAsync<UnauthorizedAccessException>();
    }

    [TestMethod]
    public async Task When_UserTryMarkTodoThatNotExists_ThenThrowException()
    {
        // Arrange
        _mockedTodoRepository.Setup(mtr => mtr.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Todo?)null);
        MarkCommand command = new MarkCommand() { TodoIds = [Guid.NewGuid()], IsDone = true };

        // Act
        Func<Task> action = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await action.Should()
            .ThrowAsync<ResourceNotFoundException>();
    }
}
