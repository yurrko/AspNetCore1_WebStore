﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using WebStore.Clients.Services.Values;
using WebStore.DAL.Context;
using WebStore.Domain.Entities;
using WebStore.Implementations;
using WebStore.Implementations.Sql;
using WebStore.Interfaces;
using WebStore.Interfaces.Api;

namespace WebStore
{
    public class Startup
    {
        /// <summary>
        /// Добавляем свойство для доступа к конфигурации
        /// </summary>
        public IConfiguration Configuration { get; }
        /// <summary>
        /// Добавляем новый конструктор, принимающий интерфейс IConfiguration
        /// </summary>
        /// <param name="configuration"></param>
        public Startup( IConfiguration configuration )
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices( IServiceCollection services )
        {
            // Добавляем сервисы, необходимые для mvc
            services.AddMvc();

            //Добавляем разрешение зависимостей
            services.AddTransient<IProductData, SqlProductData>();
            services.AddTransient<IOrdersService, SqlOrdersService>();
            services.AddSingleton<IEmployeesData, InMemoryEmployeesData>();

            //Добавляем EF Core
            services.AddDbContext<WebStoreContext>( options => options.UseSqlServer(
                Configuration.GetConnectionString( "DefaultConnection" ) ) );


            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<WebStoreContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>( options =>
            {
                // Password settings
                options.Password.RequiredLength = 6;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes( 30 );
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                //options.User.RequireUniqueEmail = true;
            } );
            services.ConfigureApplicationCookie( options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.Cookie.Expiration = TimeSpan.FromDays( 150 );
                // If the LoginPath is not set here, ASP.NET Core will default to / Account / Login
                options.LoginPath = "/Account/Login";
                // If the LogoutPath is not set here, ASP.NET Core will default to / Account / Logout
                options.LogoutPath = "/Account/Logout";
                // If the AccessDeniedPath is not set here, ASP.NET Core will default to / Account / AccessDenied
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
            } );

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<ICartService, CookieCartService>();

            services.AddTransient<IValuesService, ValuesClient>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure( IApplicationBuilder app, IHostingEnvironment env )
        {

            if ( env.IsDevelopment() )
            {
                app.UseDeveloperExceptionPage();
            }
            //Добавляем расширение для использования статических файлов, т.к. appsettings.json - это статический файл
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc( routes =>
            {
                routes.MapRoute(
                name: "areas",
                template: "{area:exists}/{controller=Home}/{action=Index}/{id?}" );

                routes.MapRoute(
                name: "default",
                template: "{controller=Home}/{action=Index}/{id?}" );
            } );
        }
    }
}
