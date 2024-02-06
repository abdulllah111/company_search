using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Application.Common.Behaviours;
using Application.Common.Interfaces;
using Application.Common.Services;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssemblies(new[] {Assembly.GetExecutingAssembly()});
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            // services.AddScoped<ICategoryService>(provider => provider.GetService<CategoryService>()!);
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IEventSubscriptionService>(provider => provider.GetService<EventSubscriptionService>()!);
            return services;
        }
    }
}