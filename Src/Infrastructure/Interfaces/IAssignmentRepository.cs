using SnapShop.Framework.Repositories;
using SnapShop.Models;

namespace SnapShop.Infrastructure.Interface
{
    public interface IAssignmentRepository : IRepository<Assignment>
    {
        List<Assignment> GetAllNew();
    }
}
