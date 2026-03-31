using System.Text.Json;
using IdentityService.Api.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging.Abstractions;

namespace IdentityService.UnitTests.Api.Infrastructure
{
    public class GlobalExceptionHandlerTests
    {
        [Test]
        public async Task TryHandleAsync_WithArgumentException_ReturnsBadRequestProblemDetails()
        {
            var handler = new GlobalExceptionHandler(NullLogger<GlobalExceptionHandler>.Instance);
            var httpContext = CreateHttpContext();

            var handled = await handler.TryHandleAsync(
                httpContext,
                new ArgumentException("Invalid payload.", "request"),
                CancellationToken.None);

            var responseJson = await ReadResponseBodyAsync(httpContext);
            using var document = JsonDocument.Parse(responseJson);
            var root = document.RootElement;

            Assert.Multiple(() =>
            {
                Assert.That(handled, Is.True);
                Assert.That(httpContext.Response.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
                Assert.That(root.GetProperty("status").GetInt32(), Is.EqualTo(StatusCodes.Status400BadRequest));
                Assert.That(root.GetProperty("title").GetString(), Is.EqualTo("Bad Request"));
                Assert.That(root.GetProperty("detail").GetString(), Is.EqualTo("Invalid payload. (Parameter 'request')"));
                Assert.That(root.GetProperty("traceId").GetString(), Is.EqualTo(httpContext.TraceIdentifier));
            });
        }

        [Test]
        public async Task TryHandleAsync_WithUnhandledException_ReturnsServerErrorProblemDetails()
        {
            var handler = new GlobalExceptionHandler(NullLogger<GlobalExceptionHandler>.Instance);
            var httpContext = CreateHttpContext();

            var handled = await handler.TryHandleAsync(
                httpContext,
                new InvalidOperationException("Unexpected failure."),
                CancellationToken.None);

            var responseJson = await ReadResponseBodyAsync(httpContext);
            using var document = JsonDocument.Parse(responseJson);
            var root = document.RootElement;

            Assert.Multiple(() =>
            {
                Assert.That(handled, Is.True);
                Assert.That(httpContext.Response.StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
                Assert.That(root.GetProperty("status").GetInt32(), Is.EqualTo(StatusCodes.Status500InternalServerError));
                Assert.That(root.GetProperty("title").GetString(), Is.EqualTo("Server Error"));
                Assert.That(root.GetProperty("detail").GetString(), Is.EqualTo("Unexpected failure."));
                Assert.That(root.GetProperty("traceId").GetString(), Is.EqualTo(httpContext.TraceIdentifier));
            });
        }

        private static DefaultHttpContext CreateHttpContext()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Response.Body = new MemoryStream();
            return httpContext;
        }

        private static async Task<string> ReadResponseBodyAsync(DefaultHttpContext httpContext)
        {
            httpContext.Response.Body.Position = 0;
            using var reader = new StreamReader(httpContext.Response.Body);
            return await reader.ReadToEndAsync();
        }
    }
}
