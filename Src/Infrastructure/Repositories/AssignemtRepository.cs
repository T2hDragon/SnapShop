using SnapShop.Data;
using SnapShop.Models;
using SnapShop.Framework.Repositories;
using SnapShop.Infrastructure.Interface;

namespace SnapShop.Infrastructure.Repositories
{
    public class AssignmentRepository : EntityRepository<Assignment>, IAssignmentRepository
    {
        private readonly AppDbContext _dbContext;

        public AssignmentRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }


        public List<Assignment> GetAllNew()
        {
            return getQuery().ToList();
        }
    }
}
