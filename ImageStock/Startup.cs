using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ImageStock.Data.Interfaces;
using ImageStock.Data.Mocks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ImageStock
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IGallery, MockGallery>();
            services.AddMvc(mvcOptions => mvcOptions.EnableEndpointRouting = false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseMvcWithDefaultRoute();

            var path = env.ContentRootPath;
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync(File.ReadAllText(Path.Combine(env.WebRootPath, "index.html")));
            //    });
            //    endpoints.MapGet("/gallery", async context =>
            //    {
            //        await context.Response.WriteAsync(File.ReadAllText(Path.Combine(env.WebRootPath, "gallery.html")));
            //    });
            //});
        }
    }
}
