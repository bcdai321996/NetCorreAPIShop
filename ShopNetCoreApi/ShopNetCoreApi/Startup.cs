using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ShopNetCoreApi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ShopNetCoreApi.Data;
using ShopNetCoreApi.Data.Infrastructure;
using ShopNetCoreApi.Data.Repositories;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.WebEncoders;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Microsoft.AspNetCore.Identity;
using ShopNetCoreApi.Extentions;

namespace ShopNetCoreApi
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
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
            services.AddControllers();
            services.Configure<AppConfig>(Configuration.GetSection("AppSetting"));
            services.AddMemoryCache();
            services.AddHttpContextAccessor();

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.Configure<WebEncoderOptions>(options =>
                options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.BasicLatin, UnicodeRanges.CjkUnifiedIdeographs)
            );
            services.AddDbContext<ShopDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b=>b.MigrationsAssembly("ShopNetCoreApi"))
            );
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Version = "1.0", Title = "ShopNetCoreApi" });

            });
            services.AddAuthentication(option =>
                {
                    option.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
                    option.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
                    option.DefaultSignInScheme = IdentityConstants.ExternalScheme;
                }
            );
            
            services.AddScoped(typeof(IRepository<>),typeof(RepositoryBase<>));
            services.AddScoped<IUserRepositories, UserRepositories>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
           
           
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShopNetCoreApi v1"));
            }
            app.UseCors("CorsPolicy");
             app.UseHttpsRedirection();
             var serviceProvider = app.ApplicationServices;
             var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
             AuthContextService.Configure(httpContextAccessor);
             app.UseMiddleware<JwtMiddlewares>();
             app.UseAuthorization();
             

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
