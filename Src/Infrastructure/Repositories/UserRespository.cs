using SnapShop.Data;
using SnapShop.Models;
using SnapShop.Framework.Repositories;
using SnapShop.Infrastructure.Interface;

namespace SnapShop.Infrastructure.Repositories
{
    public class UserRespository : EntityRepository<User>, IUserRespository
    {
        private readonly AppDbContext _dbContext;

        public UserRespository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public bool ExistsWithEmail(string email)
        {
            return getQuery().Any(e => e.Email == email);
        }

        public bool ExistsWithUsername(string username)
        {
            return getQuery().Any(e => e.Username == username);
        }

        public string? GetPasswordByUsername(string username)
        {
            return getQuery().Where(e => e.Username == username).Select(e => e.Password).SingleOrDefault();
        }

        public User? GetByUsername(string username)
        {
            return getQuery().Where(e => e.Username == username).SingleOrDefault();
        }
    }
}
