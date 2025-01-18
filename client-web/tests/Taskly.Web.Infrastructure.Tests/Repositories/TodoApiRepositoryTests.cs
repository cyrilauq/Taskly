using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using Taskly.Web.Infrastructure.DTO;
using Taskly.Web.Infrastructure.Repositories;

namespace Taskly.Web.Infrastructure.Tests.Repositories
{
    [TestClass]
    public class TodoApiRepositoryTests
    {
        TodoApiRepository repository;
        HttpClient fakeHttpClient;
        Mock<HttpMessageHandler> mockMessageHandler;

        [TestInitialize]
        public void Initialize()
        {
            // Arrange
            mockMessageHandler = new Mock<HttpMessageHandler>();
            fakeHttpClient = new HttpClient(mockMessageHandler.Object) { BaseAddress = new Uri("https://www.google.com") };
            repository = new TodoApiRepository(fakeHttpClient);
        }

        [TestMethod]
        public async Task When_Create_HasDtoAndReceiveOkStatus_ThenReturnsTodoDTO()
        {
            // Arrange
            TodoDTO addedTodoDto = new TodoDTO("", "", "", Guid.NewGuid(), false, DateTime.Now, null);
            SetMessageHandlerResponse(new FakeHttpResponse<TodoDTO>(200, addedTodoDto));

            // Act
            TodoDTO addedTodoResultDto = await repository.Create(new TodoDTO("", "", "", Guid.NewGuid(), false, DateTime.Now, null));

            // Assert
            Assert.AreEqual(addedTodoResultDto.Id, addedTodoDto.Id);
        }

        [TestMethod]
        public async Task When_GetAllForUser_RetrieveAnArrayOfTodo_ThenReturnTheTodos()
        {
            // Arrange
            IEnumerable<TodoDTO> todos = [new TodoDTO("", "", "", Guid.NewGuid(), false, DateTime.Now, null)];
            SetMessageHandlerResponse(new FakeHttpResponse<IEnumerable<TodoDTO>>(200, todos));

            // Act
            IEnumerable<TodoDTO> getTodoResultDto = await repository.GetAllForUser(Guid.NewGuid());

            // Assert
            Assert.AreEqual(todos.Count(), getTodoResultDto.Count());
        }

        [TestMethod]
        public async Task When_GetAllForUser_RetrieveAnEmptyArray_ThenReturnsAnEmptyIEnumerable()
        {
            // Arrange
            IEnumerable<TodoDTO> todos = [];
            SetMessageHandlerResponse(new FakeHttpResponse<IEnumerable<TodoDTO>>(200, todos));

            // Act
            IEnumerable<TodoDTO> getTodoResultDto = await repository.GetAllForUser(Guid.NewGuid());

            // Assert
            Assert.AreEqual(todos.Count(), getTodoResultDto.Count());
        }

        [TestMethod]
        public async Task When_GetAllForUser_RetrieveNull_ThenReturnsAnEmptyIEnumerable()
        {
            // Arrange
            IEnumerable<TodoDTO>? todos = null;
            SetMessageHandlerResponse(new FakeHttpResponse<IEnumerable<TodoDTO>>(200, todos));

            // Act
            IEnumerable<TodoDTO> getTodoResultDto = await repository.GetAllForUser(Guid.NewGuid());

            // Assert
            Assert.AreEqual(0, getTodoResultDto.Count());
        }

        [TestMethod]
        public async Task When_Delete_RequestReturnSuccessStatus_ThenReturnTrue()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            SetMessageHandlerResponse(new FakeHttpResponse<IEnumerable<TodoDTO>>(200, null));

            // Act
            bool deleteResult = await repository.Delete(id);

            // Assert
            Assert.IsTrue(deleteResult);
        }

        [TestMethod]
        public async Task When_Update_RequestReturnSuccessStatus_ThenReturnNonNullResult()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            SetMessageHandlerResponse(new FakeHttpResponse<TodoDTO>(200, new TodoDTO("", "", "", Guid.NewGuid(), false, null, null)));

            // Act
            TodoDTO updateResult = await repository.Update(id, new TodoDTO("", "", "", Guid.NewGuid(), false, null, null)); 

            // Assert
            Assert.IsNotNull(updateResult);
        }

        private void SetMessageHandlerResponse<T>(FakeHttpResponse<T> response)
        {
            HttpResponseMessage responseMsg = new()
            {
                StatusCode = (HttpStatusCode)response.StatusCode
            };
            if(response.Content != null)
            {
                responseMsg.Content = JsonContent.Create(response.Content);
            }
            mockMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(responseMsg);
        }
    }

    public record FakeHttpResponse<T>(int StatusCode, T? Content);
}
