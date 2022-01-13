using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TicketSystem.Repository.Models;

namespace TicketSystem.API.Extensions
{
    public static class DatabaseServicesExtension
    {
        private static ILoggerFactory Logger = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public static IServiceCollection AddDatabaseServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("TicketSystem");

            services.AddDbContext<TicketSystemContext>(options =>
            {
                options.UseSqlServer(connection);
                options.UseLoggerFactory(Logger);
            });
            return services;
        }
    }
}