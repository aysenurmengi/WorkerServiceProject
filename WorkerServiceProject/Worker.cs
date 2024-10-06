using System;
using System.IO;
using log4net;

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection; //Scoped servisler için gerekli

namespace WorkerServiceProject
{
    public class Worker : BackgroundService
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(Worker));
        
        private readonly IServiceProvider _serviceProvider; //Scoped servisleri çözümlemek için

        public Worker(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //Scoped bir servis olan ICustomFileSystem'i kullanmak için scope oluþturuyoruz
            using (var scope = _serviceProvider.CreateScope())
            {
                var fileSystemWatcherService = scope.ServiceProvider.GetRequiredService<ICustomFileSystem>();

                fileSystemWatcherService.Start();

                while (!stoppingToken.IsCancellationRequested)
                {
                    _logger.Info($"Worker running at: {DateTime.Now}");
                    Console.WriteLine($"Worker running at: {DateTime.Now}");
                    await Task.Delay(20000, stoppingToken);
                }
            }
        }
    }
}


