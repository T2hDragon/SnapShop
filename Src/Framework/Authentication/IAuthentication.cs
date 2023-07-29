using SnapShop.Models;

namespace SnapShop.Framework.Authentication
{
    public interface IAuthentication
    {
        string Login(string username, string password);
        Task<string> Register(User user, string password);
    }
}
