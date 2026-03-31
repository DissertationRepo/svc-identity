using IdentityService.Application.Common;

namespace IdentityService.UnitTests.Application.Common
{
    public class ResultTests
    {
        [Test]
        public void Success_CreatesSuccessfulResult()
        {
            var result = Result.Success();

            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.IsFailure, Is.False);
                Assert.That(result.Error, Is.EqualTo(Error.None));
            });
        }

        [Test]
        public void Failure_CreatesFailedResult()
        {
            var error = new Error("auth.invalid", "Invalid credentials.", ErrorType.Unauthorized);

            var result = Result.Failure(error);

            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.False);
                Assert.That(result.IsFailure, Is.True);
                Assert.That(result.Error, Is.EqualTo(error));
            });
        }

        [Test]
        public void Success_WithValue_ReturnsStoredValue()
        {
            var result = Result.Success("token");

            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.Value, Is.EqualTo("token"));
            });
        }

        [Test]
        public void Failure_WithValue_ThrowsWhenValueIsAccessed()
        {
            var result = Result.Failure<string>(AuthErrors.InvalidCredentials);

            var exception = Assert.Throws<InvalidOperationException>(() => _ = result.Value);

            Assert.That(exception!.Message, Is.EqualTo("Failed results do not contain a value."));
        }

        [Test]
        public void Constructor_WithSuccessfulStateAndError_ThrowsArgumentException()
        {
            var error = new Error("auth.invalid", "Invalid credentials.", ErrorType.Unauthorized);

            var exception = Assert.Throws<ArgumentException>(() => new TestResult(true, error));

            Assert.That(exception!.ParamName, Is.EqualTo("error"));
        }

        [Test]
        public void Constructor_WithFailedStateAndNoError_ThrowsArgumentException()
        {
            var exception = Assert.Throws<ArgumentException>(() => new TestResult(false, Error.None));

            Assert.That(exception!.ParamName, Is.EqualTo("error"));
        }

        private sealed class TestResult : Result
        {
            public TestResult(bool isSuccess, Error error)
                : base(isSuccess, error)
            {
            }
        }
    }
}
