using System;
using System.IO;
using EntityLayer;
using log4net;
using Microsoft.Extensions.DependencyInjection; // Scoped servisler için gerekli
using ServiceLayer.Abstract;

namespace WorkerServiceProject
{
    public class CustomFileSystem : ICustomFileSystem
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(CustomFileSystem));
        private string _directoryToWatch = @"C:\Users\Monster\Desktop\projectFile";
        private FileSystemWatcher _fileSystemWatcher;
        private readonly IServiceProvider _serviceProvider; // Scoped servisleri çözümlemek için

        public CustomFileSystem(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            _fileSystemWatcher = new FileSystemWatcher()
            {
                Path = _directoryToWatch,
                Filter = "",
                EnableRaisingEvents = true
            };
        }

        public void Start()
        {
            _fileSystemWatcher.NotifyFilter = NotifyFilters.Attributes | NotifyFilters.CreationTime | NotifyFilters.DirectoryName | NotifyFilters.LastAccess;

            _fileSystemWatcher.Changed += _fileSystemWatcher_Changed;
            _fileSystemWatcher.Created += _fileSystemWatcher_Created;
            _fileSystemWatcher.Deleted += _fileSystemWatcher_Deleted;
            _fileSystemWatcher.Renamed += _fileSystemWatcher_Renamed;
            _fileSystemWatcher.Error += _fileSystemWatcher_Error;
        }

        private void _fileSystemWatcher_Error(object sender, ErrorEventArgs e)
        {
            try
            {
                log.Error("FileSystemWatcher Error: " + e.GetException().Message);
            }
            catch (Exception)
            {

                Console.WriteLine("CustomFileSystem hatalı Error işlemi!");
            }
            
        }

        private async void _fileSystemWatcher_Renamed(object sender, RenamedEventArgs e)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var watcherService = scope.ServiceProvider.GetRequiredService<IWatcherService>();

                    var entry = new WatcherTables
                    {
                        Type = "Renamed",
                        OldPath = e.OldFullPath,
                        NewPath = e.FullPath,
                        Time = DateTime.Now,
                    };

                    await watcherService.AddWatcherTableEntry(entry);
                    log.Info($"Renamed: Old: {e.OldFullPath} and New: {e.FullPath}");
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("CustomFileSystem hatalı Renamed işlemi!");
                           
            }
            
        }

        private async void _fileSystemWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var watcherService = scope.ServiceProvider.GetRequiredService<IWatcherService>();

                    var entry = new WatcherTables
                    {
                        Type = "Deleted",
                        OldPath = e.FullPath,
                        NewPath = null,
                        Time = DateTime.Now
                    };

                    await watcherService.AddWatcherTableEntry(entry);
                    string logMessage = $"Deleted: {e.FullPath} at {DateTime.Now}";
                    log.Info(logMessage);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("CustomFileSystem hatalı Deleted işlemi!");
            }
            
        }

        private async void _fileSystemWatcher_Created(object sender, FileSystemEventArgs e)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var watcherService = scope.ServiceProvider.GetRequiredService<IWatcherService>();

                    var entry = new WatcherTables
                    {
                        Type = "Created",
                        OldPath = e.FullPath,
                        NewPath = null,
                        Time = DateTime.Now
                    };

                    await watcherService.AddWatcherTableEntry(entry);
                    log.Info($"Created: {e.Name} in {e.FullPath} at {DateTime.Now}");
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("CustomFileSystem hatalı Created işlemi!");
            }
            
        }

        private async void _fileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var watcherService = scope.ServiceProvider.GetRequiredService<IWatcherService>();

                    var entry = new WatcherTables
                    {
                        Type = "Changed",
                        OldPath = e.FullPath,
                        NewPath = null,
                        Time = DateTime.Now
                    };

                    if (e.ChangeType != WatcherChangeTypes.Changed)
                    {
                        return;
                    }

                    await watcherService.AddWatcherTableEntry(entry);
                    log.Info($"Changed: {e.FullPath} at {DateTime.Now}");
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("CustomFileSystem hatalı Changed işlemi!");
            }
            
        }
    }
}








