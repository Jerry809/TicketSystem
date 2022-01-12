using Microsoft.Extensions.DependencyInjection;
using TicketSystem.API.Services;
using TicketSystem.API.Services.Interfaces;

namespace TicketSystem.API.Extensions
{
    public static class FeatureServiceExtension
    {
        public static IServiceCollection AddFeatureServices(this IServiceCollection services)
        {
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICommentService, CommentService>();

            return services;
        }
    }
}