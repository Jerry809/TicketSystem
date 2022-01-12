using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicketSystem.Repository.Models;

namespace TicketSystem.API.Extensions
{
    public static class DatabaseServicesExtension
    {
        // This method to add Database services to service container.
        public static IServiceCollection AddDatabaseServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("TicketSystem");

            services.AddDbContext<TicketSystemContext>(options =>
            {
                options.UseSqlServer(connection);
            });
            return services;
        }
    }
}