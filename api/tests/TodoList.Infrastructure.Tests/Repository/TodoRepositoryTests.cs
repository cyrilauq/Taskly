using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TodoList.Infrastructure.Data;
using TodoList.Infrastructure.Entities;
using TodoList.Infrastructure.Repository;

namespace TodoList.Infrastructure.Tests.Repository
{
    [TestClass]
    public class TodoRepositoryTests
    {
        private Guid connectedUserId;
        private TodoListContext context = null!;

        [TestInitialize]
        public async Task Setup()
        {
            var _contextOptions = new DbContextOptionsBuilder<TodoListContext>()
                .UseInMemoryDatabase("TodoListTest")
                .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            context = new TodoListContext(_contextOptions);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            connectedUserId = (await context.Users.AddAsync(new User
            {
                Email = "text@test.com",
                Firstname = "Test",
                Lastname = "Test",
            })).Entity.Id;

            try
            {
                var affectedRow = await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        [TestMethod]
        public async Task WhenUserAddTodoThenTheTodoIsAddedAndReturned()
        {
            // Arrange
            var repository = new TodoRepository(context);
            var todo = new Todo
            {
                Name = "Test",
                UpdatedOn = DateTime.UtcNow,
                Content = "Tzst",
                CreatedOn = DateTime.UtcNow,
                IsDone = false,
                UserId = connectedUserId,
            };

            // Act
            var result = await repository.AddAsync(todo);

            // Assert
            Assert.IsNotNull(result.Id);
        }

        [TestMethod]
        public async Task WhenTodoNotRealtedToUserThenThrowException()
        {
            // Arrange
            var repository = new TodoRepository(context);
            var todo = new Todo
            {
                Name = "Test",
                UpdatedOn = DateTime.UtcNow,
                Content = "Tzst",
                CreatedOn = DateTime.UtcNow,
                IsDone = false,
                UserId = connectedUserId
            };

            // Act
            var result = await repository.AddAsync(todo);
            todo.UserId = Guid.Empty;

            // Act & Assert
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(async () => await repository.UpdateAsync("1534", todo));
        }

        [TestMethod]
        public async Task WhenNoTodoExistWithTheGivenIdThenThrowException()
        {
            // Arrange
            var repository = new TodoRepository(context);
            var todo = new Todo
            {
                Name = "Test",
                UpdatedOn = DateTime.UtcNow,
                Content = "Tzst",
                CreatedOn = DateTime.UtcNow,
                IsDone = false,
                UserId = connectedUserId
            };
            await repository.AddAsync(todo);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(async () => await repository.UpdateAsync(Guid.NewGuid().ToString(), todo));
        }

        [TestMethod]
        public async Task WhenNoTodoExistWithTheGivenIdInsideTheTodoThenThrowException()
        {
            // Arrange
            var repository = new TodoRepository(context);
            var todo = new Todo
            {
                Name = "Test",
                UpdatedOn = DateTime.UtcNow,
                Content = "Tzst",
                CreatedOn = DateTime.UtcNow,
                IsDone = false,
                UserId = connectedUserId,
            };
            var addedTodo = (await repository.AddAsync(todo));
            var id = addedTodo.Id;
            addedTodo.Id = Guid.NewGuid();

            // Act & Assert
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(async () => await repository.UpdateAsync(id.ToString(), addedTodo));
        }

        [TestMethod]
        public async Task WhenIdOfTodoIsValidThenReturnTheUpdatedTodo()
        {
            // Arrange
            var repository = new TodoRepository(context);
            var todo = await repository.AddAsync(new Todo
            {
                Name = "Test",
                UpdatedOn = DateTime.UtcNow,
                Content = "Tzst",
                CreatedOn = DateTime.UtcNow,
                IsDone = false,
                UserId = connectedUserId
            });

            // Act
            todo.Content = "New content";
            var updatedTodo = await repository.UpdateAsync(todo.Id.ToString(), todo);

            // Assert
            Assert.AreEqual("New content", updatedTodo.Content);
            Assert.AreEqual(context.Todos.Count(), 1);
            Assert.AreEqual("New content", (await context.Todos.FindAsync(todo.Id))!.Content);
        }
    }
}