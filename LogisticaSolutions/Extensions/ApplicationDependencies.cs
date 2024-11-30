using LogisticaSolutions.Context;
using LogisticaSolutions.Contracts.Repository;
using LogisticaSolutions.Contracts.Service;
using LogisticaSolutions.Implement.Repositories;
using LogisticaSolutions.Implement.Services;
using Microsoft.EntityFrameworkCore;



namespace LogisticaSolutions.Extensions
{
    public static class ApplicationDependencies
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Database");

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}
