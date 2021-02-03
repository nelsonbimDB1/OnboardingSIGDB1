using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using OnboardingSIGDB1.CrossCutting.IoC;
using OnboardingSIGDB1.Data.Context;
using Microsoft.EntityFrameworkCore;
using OnboardingSIGDB1.Domain.Notification;
using Microsoft.OpenApi.Models;

namespace OnboardingSIGDB1.API
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
            var connection = Configuration.GetConnectionString("SIGDB1");
            services.AddDbContext<SIGContext>(options => options.UseSqlServer(connection, b => b.MigrationsAssembly("OnboardingSIGDB1.API")));
            services.AddMemoryCache();
            ConfigurationIOC.LoadDomainServices(services);
            ConfigurationIOC.LoadDataServices(services);
            AutoMapperConfiguraction.Config(services);
            services.AddMvc()
                   .AddFluentValidation(fvc =>
                               fvc.RegisterValidatorsFromAssemblyContaining<Startup>())
                   .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Swagger API",
                    Version = "v1",
                    Description = "OnboardingSIGDB1"
                });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("v1/swagger.json", "MyAPI V1");
                });
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
