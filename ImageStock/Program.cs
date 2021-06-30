using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace ImageStock
{
    public class Program
    {
        //Точка входу
        public static void Main(string[] args)
        {
            //Cтворюємо HostBuilder для розгортання веб-застосунку
            IHostBuilder hostBuilder = Host.CreateDefaultBuilder(args);
                
            //Налаштовуємо HostBuilder
            hostBuilder.ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseIISIntegration();
                webBuilder.UseStartup<Startup>();
            });

            //Створюємо Host (хост) для запуску веб-застосунку
            IHost host = hostBuilder.Build();

            //Запускаємо веб-застосунок в рамках створеного хосту
            host.Run();
        }
    }
}
