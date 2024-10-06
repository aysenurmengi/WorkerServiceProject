using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using DataLayer.Abstract;
using DataLayer.EntityFramework;
using DataLayer.Repository;
using DataLayer;
using ServiceLayer.Abstract;
using ServiceLayer;
using WorkerServiceProject;
using Microsoft.IdentityModel.Logging;
using log4net;
using Microsoft.AspNetCore.Mvc;

[assembly: ApiController]
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
public class Program
{
    
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }


    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                // Hosted service
                services.AddHostedService<Worker>(); // Worker'ý ekliyoruz

                // Servisleri burada ekliyoruz
                services.AddScoped<IWatcherService, WatcherService>(); // IWatcherService'i scoped olarak ekliyoruz
                services.AddScoped<IWatcherRepository, EFWatcher>(); // IWatcherRepository'i scoped olarak ekliyoruz
                services.AddScoped(typeof(IGeneric<>), typeof(GenericRepository<>)); // GenericRepository'i scoped olarak ekliyoruz

                // CustomFileSystem'i scoped olarak ekliyoruz
                services.AddScoped<ICustomFileSystem, CustomFileSystem>();

                // Sql ile kullanýlacak hale getirdik
                var configuration = hostContext.Configuration;
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                services.AddDbContext<ServiceContext>(options => options.UseSqlServer(connectionString));
            });
}
