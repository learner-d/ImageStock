using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace ImageStock
{
    public class Program
    {
        //����� �����
        public static void Main(string[] args)
        {
            //C�������� HostBuilder ��� ����������� ���-����������
            IHostBuilder hostBuilder = Host.CreateDefaultBuilder(args);
                
            //����������� HostBuilder
            hostBuilder.ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseIISIntegration();
                webBuilder.UseStartup<Startup>();
            });

            //��������� Host (����) ��� ������� ���-����������
            IHost host = hostBuilder.Build();

            //��������� ���-���������� � ������ ���������� �����
            host.Run();
        }
    }
}
