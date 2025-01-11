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
        // TODO : Add Tests
        TodoApiRepository repository;
        FakeHttpClient fakeHttpClient;

        [TestInitialize]
        public void Initialize()
        {
            // Arrange
            fakeHttpClient = new FakeHttpClient();
            repository = new TodoApiRepository(fakeHttpClient);
        }

        [TestMethod]
        public async Task When_Create_HasDtoAndReceiveOkStatus_ThenReturnsTodoDTO()
        {
            // Arrange
            TodoDTO addedTodoDto = new TodoDTO("", "", "", Guid.NewGuid(), false, DateTime.Now, null);
            fakeHttpClient.SetResponse(new FakeHttpResponse<TodoDTO>(200, addedTodoDto));

            // Act
            TodoDTO addedTodoResultDto = await repository.Create(new TodoDTO("", "", "", Guid.NewGuid(), false, DateTime.Now, null));

            // Assert
            Assert.AreEqual(addedTodoResultDto.Id, addedTodoDto.Id);
        }
    }

    public class FakeHttpClient : HttpClient
    {
        private HttpResponseMessage ResponseMessage { get; set; }

        public void SetResponse<T>(FakeHttpResponse<T> fakeHttpResponse)
        {
            var json = JsonConvert.SerializeObject(fakeHttpResponse.Content);

            ResponseMessage = new HttpResponseMessage
            {
                Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json"),
                StatusCode = (HttpStatusCode)fakeHttpResponse.StatusCode
            };
        }

        public override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(ResponseMessage);
        }
    }

    public record FakeHttpResponse<T>(int StatusCode, T Content);
}
