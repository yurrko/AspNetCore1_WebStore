using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebStore.DAL.Context;
using WebStore.infrastructure.Implementations;
using WebStore.infrastructure.Interfaces;
using WebStore.Infrastructure.Implementations.Sql;

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

            services.AddSingleton<IEmployeesData , InMemoryEmployeesData>();
            services.AddScoped<IProductData , SqlProductData>();

            services.AddDbContext<WebStoreContext>( options => options.UseSqlServer(
                             Configuration.GetConnectionString( "DefaultConnection" ), b => b.MigrationsAssembly( "WebStore" ) )  );

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure( IApplicationBuilder app , IHostingEnvironment env )
        {

            if ( env.IsDevelopment() )
            {
                app.UseDeveloperExceptionPage();
            }
            //Добавляем расширение для использования статических файлов, т.к. appsettings.json - это статический файл
            app.UseStaticFiles();

            app.UseMvc( routes =>
            {
                routes.MapRoute(
                name: "default" ,
                template: "{controller=Home}/{action=Index}/{id?}" );
            } );
        }
    }
}
