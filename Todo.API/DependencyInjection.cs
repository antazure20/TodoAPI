using Microsoft.EntityFrameworkCore;
using Todo.Application;
using Todo.Application.Contracts;
using Todo.Application.Implementation;
using Todo.Domain.RepositoryInterfaces;
using Todo.Infrastructure;
using Todo.Infrastructure.Persistence.Entities;
using Todo.Infrastructure.Repository;

namespace Todo.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<TodoAppDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddAutoMapper(typeof(InfraAssemblyMarker).Assembly);  // register AutoMapper profiles from the Infrastructure assembly only

            services.AddScoped<IUserRepository, UserRepository>();


            return services;
        }

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ApplicationLayerMarker).Assembly);  // register AutoMapper profiles from the Application assembly only)
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
