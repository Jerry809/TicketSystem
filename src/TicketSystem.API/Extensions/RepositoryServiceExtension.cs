using Microsoft.Extensions.DependencyInjection;
using TicketSystem.Repository.Repositories;
using TicketSystem.Repository.Repositories.Interfaces;

namespace TicketSystem.API.Extensions
{
    public static class RepositoryServiceExtension
    {
        public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
        {
            services.AddScoped<ITicketRepository, TicketRepository>();

            return services;
        }
    }
}