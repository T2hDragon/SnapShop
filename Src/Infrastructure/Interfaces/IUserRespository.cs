using SnapShop.Framework.Repositories;
using SnapShop.Models;

namespace SnapShop.Infrastructure.Interface
{
    public interface IUserRespository : IRepository<User>
    {
        bool ExistsWithEmail(string email);
        bool ExistsWithUsername(string username);
        string? GetPasswordByUsername(string username);

        public User? GetByUsername(string username);
    }
}
