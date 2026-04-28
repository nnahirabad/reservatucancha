using canchasfutbol.Application.Contracts.Persistence;
using canchasfutbol.Domain.Models;
using canchasfutbol.Infrastructuree.Repositories;
using canchasfutbol.Infrastructuree.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection; 
using Microsoft.EntityFrameworkCore;
using canchasfutbol.Application.Features.Canchas;

namespace canchasfutbol.Infrastructuree
{
    public static class InfrastructureServiceRegistration
    {

        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
             services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            

            services.AddScoped<IUnitOfWorkRepository, UnitOfWorkRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IReservaRepository, ReservaRepository>();
            services.AddScoped<ICanchasRepository, CanchasRepository>();
            services.AddScoped<CanchaService>(); 
            

            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));

            return services;




        }
    }
}
