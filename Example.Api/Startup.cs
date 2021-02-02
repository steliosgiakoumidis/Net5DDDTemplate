using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Example.Api.Helpers;
using Example.Api.Middleware;
using Example.Api.Transformers;
using Example.Api.Validators;
using Example.Business.Handlers;
using Example.Business.Interfaces;
using Example.Infrastructure;
using Example.Infrastructure.Transformers;
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
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Example.Api
{
    public class Startup
    {
        public Startup(IHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();
            
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(opt =>
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                    opt => opt.EnableRetryOnFailure(3))
                    .EnableSensitiveDataLogging());
            services.AddSingleton<BasicTypesValidator>();
            services.AddSingleton<UserTransformersApi>();
            services.AddSingleton<UserTransformersInfrastructure>();
            services.AddScoped<IUserHandlers, UserHandler>();
            services.AddScoped<IDatabaseAccess, DatabaseAccess>();
            services.AddControllers();
            services.AddHeaderPropagation(opt =>
            {
                opt.Headers.Add("x-test-feature");
                opt.Headers.Add("newnew", context => "This is a hardcoded value");
            });
            services.AddHttpClient("Test", opt =>
            {
                opt.BaseAddress = new Uri("");
                opt.Timeout = TimeSpan.FromSeconds(30);
            }).AddPolicyHandler(RetryPolicies.GetHttpPolicy());
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Example.Api", Version = "v1"});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Example.Api v1"));
            }

            app.UseHttpsRedirection();
            app.UseHeaderPropagation();
            app.UseRouting();
            loggerFactory.AddSerilog();
            app.UseCustomExceptionHandling();
            app.UseEnrichedLogs();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}