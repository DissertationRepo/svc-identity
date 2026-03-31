using IdentityService.Application.Models;

namespace IdentityService.UnitTests.Application.Models
{
    public class RefreshTests
    {
        [Test]
        public void Constructor_WithInvalidUserId_ThrowsArgumentException()
        {
            var exception = Assert.Throws<ArgumentException>(() => new Refresh("refresh-token", "not-a-guid"));

            Assert.Multiple(() =>
            {
                Assert.That(exception!.ParamName, Is.EqualTo("userId"));
                Assert.That(exception.Message, Does.Contain("Invalid user id."));
            });
        }

        [Test]
        public void Constructor_WithNullRefreshToken_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new Refresh(null!, Guid.NewGuid().ToString()));

            Assert.That(exception!.ParamName, Is.EqualTo("RefreshToken"));
        }

        [Test]
        public void Constructor_WithValidValues_SetsProperties()
        {
            var userId = Guid.NewGuid();

            var refresh = new Refresh("refresh-token", userId.ToString());

            Assert.Multiple(() =>
            {
                Assert.That(refresh.RefreshToken, Is.EqualTo("refresh-token"));
                Assert.That(refresh.UserId, Is.EqualTo(userId));
            });
        }
    }
}
