using System;
using System.IdentityModel.Tokens.Jwt;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TicketSystem.API.Helpers;
using TicketSystem.API.Services;
using TicketSystem.API.Services.Interfaces;
using TicketSystem.API.ViewModel;
using TicketSystem.API.ViewModel.Validators;

namespace TicketSystem.API.Extensions
{
    public static class FeatureServiceExtension
    {
        public static IServiceCollection AddFeatureServices(this IServiceCollection services)
        {
            services.AddTransient<IValidator<CommentInsertViewModel>, CommentInsertViewModelValidator>();
            services.AddTransient<IValidator<LoginViewModel>, LoginViewModelValidator>();
            services.AddTransient<IValidator<TicketInsertViewModel>, TicketInsertViewModelValidator>();
            services.AddTransient<IValidator<TicketStatusUpdateViewModel>, TicketStatusUpdateViewModelValidator>();
            services.AddTransient<IValidator<UserInsertViewModel>, UserInsertViewModelValidator>();

            
            services.AddSingleton<JwtHelper>();
            
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICommentService, CommentService>();

            return services;
        }
    }
}