using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ssl.Certificate.Data;
using Ssl.Certificate.Monitor.Interfaces;
using Ssl.Certificate.Monitor.Repository;
using Ssl.Certificate.Monitor.Services;
using System.Reflection;

namespace Ssl.Certificate.Monitor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "")
            .AddJsonFile("appsettings.json", false, true)
            .Build();

            var services = new ServiceCollection();
            services.AddDbContext<MonitorDbContext>(x => x.UseSqlServer(configuration.GetConnectionString("DbContext")));
            services.AddSingleton<ICertificateService, CertificateService>();
            services.AddSingleton<IControlRepository, ControlRepository>();
            services.AddSingleton<ISslActivityLogRepository, SslActivityLogRepository>();
            services.AddTransient<ITcpClientWrapper, TcpClientWrapper>();
            services.AddSingleton<IMonitor, Monitor>();
            var serviceProvider = services.BuildServiceProvider();            
            var monitor = serviceProvider.GetRequiredService<IMonitor>();
            monitor.Run();

        }
    }
}