using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SnapShop.Infrastructure.Interface;
using SnapShop.Models;

namespace SnapShop.Framework.Authentication;
public class Authentication : IAuthentication
{
    private readonly IUserRespository _userRespository;
    private readonly Jwt _jwt;

    public Authentication(IUserRespository userRespository)
    {
        _userRespository = userRespository;
        _jwt = new Jwt(userRespository);
    }

    public string Login(string username, string password)
    {
        return _jwt.Login(username, password);
    }

    public async Task<string> Register(User user, string password)
    {
        return await _jwt.Register(user, password);
    }
}
