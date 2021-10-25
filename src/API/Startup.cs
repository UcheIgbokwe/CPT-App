using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Application.Behaviours;
using Application.Contracts.Domain.Config;
using Application.Contracts.Repository;
using Application.Features.Accounts;
using Application.Features.Booking;
using FluentValidation;
using Infrastructure;
using Infrastructure.Persistence;
using Infrastructure.Repository;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Serilog;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var configs = Configuration.GetSection("ConnectionStrings");
            services.Configure<ConnectionStrings>(configs);
            services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration.GetSection("ConnectionStrings:DefaultConnectionString").Value));

            services.AddScoped<DbContext, DataContext>();
            services.AddControllers();
            services.AddAutoMapper(typeof(Startup));
            services.AddInfrastructureServices();

            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "Covid Test API", Version = "v1" }));

            services.AddMediatR(typeof(RegisterUserCommandHandler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(CreateBookingCommandHandler).GetTypeInfo().Assembly);

            services.AddValidatorsFromAssemblyContaining(typeof(RegisterUserCommandHandler));
            services.AddValidatorsFromAssemblyContaining(typeof(CreateBookingCommandHandler));
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));

            //app.UseHttpsRedirection();

            app.UseSerilogRequestLogging();

            app.UseRouting();
            app.UseStaticFiles();
            app.UseCors("AllowAll");

            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
