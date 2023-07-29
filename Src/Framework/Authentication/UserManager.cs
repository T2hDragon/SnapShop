using SnapShop.Infrastructure.Interface;
using SnapShop.Models;

namespace SnapShop.Framework.Authentication;
public class UserManager : IUserManager
{
    private readonly IUserRespository _userRespository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserManager(IUserRespository userRespository, IHttpContextAccessor httpContextAccessor)
    {
        _userRespository = userRespository;
        _httpContextAccessor = httpContextAccessor;
    }


    public SnapShop.Models.User GetUser()
    {
        var username = _httpContextAccessor.HttpContext?.User.Identity?.Name;
        if (username == null) {
            throw new AccessViolationException("User not logged in");
        }
        User? user = _userRespository.GetByUsername(username);
        if (user == null) {
            throw new AccessViolationException("User not logged in");
        }
        return user;
    }

    public bool IsLoggedIn()
    {
        return _httpContextAccessor.HttpContext?.User.Identity?.Name != null;;
    }
}
