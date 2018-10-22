using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebStore.DAL.Context;
using WebStore.Domain.Entities;
using WebStore.Services;
using WebStore.Services.Sql;
using WebStore.Interfaces;
using WebStore.Logger;
using WebStore.Services.Middleware;

namespace WebStore.ServicesHosting
{
    public class Startup
    {
        public Startup( IConfiguration configuration )
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices( IServiceCollection services )
        {
            services.AddMvc();

            // Добавляем EF Core
            services.AddDbContext<WebStoreContext>( options => options.UseSqlServer(
                 Configuration.GetConnectionString( "DefaultConnection" ) ) );

            // Добавляем разрешение зависимостей
            services.AddSingleton<IEmployeesData, InMemoryEmployeesData>();

            services.AddTransient<IProductData, SqlProductData>();
            services.AddTransient<IOrdersService, SqlOrdersService>();

            services.AddIdentity<User, IdentityRole>()
                    .AddEntityFrameworkStores<WebStoreContext>()
                    .AddDefaultTokenProviders();

            // Настройки для корзины
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ICartService, CookieCartService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure( IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory )
        {
            loggerFactory.AddLog4Net();

            if ( env.IsDevelopment() )
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware( typeof( ErrorHandlingMiddleware ) );

            app.UseMvc();
        }
    }
}
