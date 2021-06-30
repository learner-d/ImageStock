using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ImageStock.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ImageStock
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _confRoot;
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        public Startup(IWebHostEnvironment env)
        {
            _env = env;
            _confRoot = new ConfigurationBuilder().SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json").Build();
            AppDomain.CurrentDomain.SetData("DataDirectory", env.ContentRootPath);
            Console.WriteLine(AppDomain.CurrentDomain.GetData("DataDirectory"));
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            string dbConnetionString = _confRoot.GetConnectionString("DefaultConnection");
            Console.WriteLine(dbConnetionString);

            services.AddDbContext<AppDbContext>(options => 
                options.UseSqlServer(dbConnetionString));

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => //CookieAuthenticationOptions
                {
                    options.LoginPath = new PathString("/Profile/Login");
                });

            IMvcBuilder mvcBuilder = services.AddMvc(mvcOptions => mvcOptions.EnableEndpointRouting = false);
            if(_env.IsDevelopment())
                mvcBuilder.AddRazorRuntimeCompilation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Перевірка чи перебуває програма на стадії розробки
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePagesWithReExecute("/home/error", "?code={0}");

            app.UseAuthentication();     //підтримка автентифікації
            app.UseAuthorization();      //підтримка авторизації

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();//маршритизацію виду MVC - <сайт>/<контролер>/<дія>
        }
    }
}
