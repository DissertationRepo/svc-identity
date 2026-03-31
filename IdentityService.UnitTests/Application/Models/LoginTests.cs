using IdentityService.Application.Models;

namespace IdentityService.UnitTests.Application.Models
{
    public class LoginTests
    {
        [Test]
        public void Constructor_WithNullEmail_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new Login(null!, "password", "refresh-token"));

            Assert.That(exception!.ParamName, Is.EqualTo("Email"));
        }

        [Test]
        public void Constructor_WithNullPassword_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new Login("user@example.com", null!, "refresh-token"));

            Assert.That(exception!.ParamName, Is.EqualTo("Password"));
        }

        [Test]
        public void Constructor_WithValues_SetsProperties()
        {
            var login = new Login("user@example.com", "password", "refresh-token");

            Assert.Multiple(() =>
            {
                Assert.That(login.Email, Is.EqualTo("user@example.com"));
                Assert.That(login.Password, Is.EqualTo("password"));
                Assert.That(login.RefreshToken, Is.EqualTo("refresh-token"));
            });
        }

        [Test]
        public void Constructor_WithNullRefreshToken_LeavesRefreshTokenNull()
        {
            var login = new Login("user@example.com", "password", null);

            Assert.That(login.RefreshToken, Is.Null);
        }
    }
}
