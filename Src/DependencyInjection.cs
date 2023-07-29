
using SnapShop.Framework.Authentication;
using SnapShop.Framework.Repositories;
using SnapShop.Infrastructure.Interface;
using SnapShop.Infrastructure.Repositories;
using SnapShop.Models;

public class DependencyInjection
{
    public static void Add(IServiceCollection services)
    {
        AddRepositories(services);
        services.AddTransient<IAuthentication, Authentication>();
        services.AddScoped<IUserManager, UserManager>();
    }

    private static void AddRepositories(IServiceCollection services)
    {
        // Transient would be more suitable, but Scoped is more efficient
        services.AddScoped<IRepository<Entity>, EntityRepository<Entity>>();
        services.AddScoped<IAssignmentRepository, AssignmentRepository>();
        services.AddScoped<IUserRespository, UserRespository>();
    }
}

