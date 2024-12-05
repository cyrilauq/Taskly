using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TodoList.Infrastructure.Data;
using TodoList.Infrastructure.Entities;
using TodoList.Infrastructure.Repository;
using TodoList.Infrastructure.Tests.Utils;

namespace TodoList.Infrastructure.Tests.Repository
{
    [TestClass]
    public class UserRepositoryTests
    {
        UserRepository _userRepository;
        TodoListContext context;

        [TestInitialize]
        public void SetUp()
        {
            var _contextOptions = new DbContextOptionsBuilder<TodoListContext>()
                .UseInMemoryDatabase("TodoListTest")
                .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;
            context = new TodoListContext(_contextOptions);

            var userManager = MockedUserManager.GetUserManagerMock<User>(new TodoListContext(_contextOptions));

            _userRepository = new(userManager.Object, context);
        }

        [TestMethod]
        public async Task Tests()
        {
            Assert.AreEqual(0, context.Users.Count());

            await _userRepository.Add(new User
            {
                Email = "text@test.com",
                Firstname = "Test",
                Lastname = "Test",
            }, "Pa$$w0rd");

            Assert.AreEqual(1, context.Users.Count());
        }
    }
}
