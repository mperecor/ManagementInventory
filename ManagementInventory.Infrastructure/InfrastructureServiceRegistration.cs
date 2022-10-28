using ManagementInventory.Application.Contracts.Persistence;
using ManagementInventory.Infrastructure.Persistence;
using ManagementInventory.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ManagementInventory.Infrastructure
{
    /// <summary>
    /// Class to register all dependecies to infrastructure layer
    /// </summary>
    public static class InfrastructureServiceRegistration
    {
        /// <summary>
        /// Method to add all depedencies to infrastructure layer
        /// </summary>
        /// <param name="services">Collection to service to add dependencies</param>
        /// <param name="configuration"></param>
        /// <returns>Returns a collection of services with all dependencies injected</returns>
        public static IServiceCollection AddIInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ManagementInventoryDbContext>(opt =>
                opt.UseInMemoryDatabase("Inventory"));

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));

            return services;
        }
    }
}