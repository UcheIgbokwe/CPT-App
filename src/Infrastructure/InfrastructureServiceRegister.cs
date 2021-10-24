using System;
using Application.Contracts.Repository;
using Application.Contracts.Services;
using Infrastructure.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfrastructureServiceRegister
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            //add scoped interfaces
            services.AddScoped<IUserRepository, UserRepository>();
            
            


            return services;
        }
    }
}