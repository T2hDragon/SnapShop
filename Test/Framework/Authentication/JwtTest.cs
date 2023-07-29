using Moq;
using SnapShop.Infrastructure.Interface;
using Xunit;
using Project = SnapShop.Framework.Authentication;

namespace Test.Framework.Authentication;

public class JwtTest
{
    private readonly Project.Jwt _jwt;
    private readonly Mock<IUserRespository> _userRepositoryMock;
    public JwtTest() {
        System.Environment.SetEnvironmentVariable("SECRET_KEY", "^d&)j%p*$2c@^e_#d1f+l53yd#=k4c5#0zq()cl1018dj5%xh$");
        string? nullValue = null;
        _userRepositoryMock = new Mock<IUserRespository>();
        _userRepositoryMock.SetReturnsDefault(false);
        _userRepositoryMock.Setup(x => x.ExistsWithEmail("actual@email.com")).Returns(true);
        _userRepositoryMock.Setup(x => x.ExistsWithUsername("actualUsername")).Returns(true);
        _userRepositoryMock.Setup(x => x.GetPasswordByUsername(It.IsAny<string>())).Returns(nullValue);
        _userRepositoryMock.Setup(x => x.GetPasswordByUsername("actualUsername")).Returns(BCrypt.Net.BCrypt.HashPassword("Actu4lP$S5W0rd", BCrypt.Net.BCrypt.GenerateSalt()));


        _jwt = new Project.Jwt(_userRepositoryMock.Object);
    }

    [Theory]
    [InlineData("actualUsername", "actual@email.com", "Username already exists")]
    [InlineData("actualUsername", "false@email.com", "Username already exists")]
    [InlineData("falseUsername", "actual@email.com", "Email already exists")]
    [InlineData("falseUsername", "false@email.com", null)]

    public async void RegisterTest(string username, string email, string? expectedErrorMessage)
    {
        SnapShop.Models.User user = new () {
            Username = username,
            Email = email,
            Name = "Name",
        };

        if (expectedErrorMessage is null) {
            string token = await _jwt.Register(user, "password");
            Assert.NotEmpty(token);
            Assert.Equal(3, token.Split(".").Length);
        } else {
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _jwt.Register(user, "password"));
            Assert.Equal(expectedErrorMessage, exception.Message);
        }
    }

    [Theory]
    [InlineData("falseUsername", "Actu4lP$S5W0rd", "Incorrect username")]
    [InlineData("falseUsername", "F4lsePas$w0rd", "Incorrect username")]
    [InlineData("actualUsername", "F4lsePas$w0rd", "Incorrect password")]
    [InlineData("actualUsername", "Actu4lP$S5W0rd", null)]
    public void LoginTest(string username, string password, string? expectedErrorMessage)
    {

        if (expectedErrorMessage is null) {
            string token = _jwt.Login(username, password);
            Assert.NotEmpty(token);
            Assert.Equal(3, token.Split(".").Length);
        } else {
            var exception = Assert.Throws<UnauthorizedAccessException>(() => _jwt.Login(username, password));
            Assert.Equal(expectedErrorMessage, exception.Message);
        }
    }
}