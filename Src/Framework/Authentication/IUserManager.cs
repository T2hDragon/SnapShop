using SnapShop.Models;

namespace SnapShop.Framework.Authentication
{
    public interface IUserManager
    {
        public SnapShop.Models.User GetUser();
        public bool IsLoggedIn();

    }
}
