﻿using Taskly.Web.Application.Http.Handler;
using Taskly.Web.Exceptions;

namespace Taskly.Web.Application.Tests.Http.Client
{
    [TestClass]
    public class HttpStatusHandlerTests
    {
        [TestMethod]
        public async Task WhenHttpResponseAsCode404ThenThrowException()
        {
            var handler = new HttpStatusHandler() { InnerHandler = TestHandlerFactory.CreateTestHandler(404) };

            await Assert.ThrowsExceptionAsync<NotFoundException>(async () => await new HttpClient(handler).GetAsync("https://www.google.com"));
        }

        [TestMethod]
        public async Task WhenHttpResponseAsCode500ThenThrowException()
        {
            var handler = new HttpStatusHandler() { InnerHandler = TestHandlerFactory.CreateTestHandler(500) };

            await Assert.ThrowsExceptionAsync<InternalServerErrorException>(async () => await new HttpClient(handler).GetAsync("https://www.google.com"));
        }

        [TestMethod]
        public async Task WhenUnsupportedHttpErrorCodeIsReceivedThenThrowException()
        {
            var handler = new HttpStatusHandler() { InnerHandler = TestHandlerFactory.CreateTestHandler(503) };

            await Assert.ThrowsExceptionAsync<UnExpectedHttpException>(async () => await new HttpClient(handler).GetAsync("https://www.google.com"));
        }
    }

    static class TestHandlerFactory
    {
        public static TestHandler CreateTestHandler(int statusCode)
        {
            return new TestHandler { HttpCode = statusCode };
        }
    }

    class TestHandler : DelegatingHandler
    {
        public int HttpCode { get; set; }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage();
            response.StatusCode = (System.Net.HttpStatusCode)HttpCode;
            return Task.FromResult(response);
        }
    }
}
