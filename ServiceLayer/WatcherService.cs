using DataLayer.Abstract;
using EntityLayer;
using Microsoft.Extensions.FileSystemGlobbing;
using ServiceLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ServiceLayer
{

    public class WatcherService : IWatcherService
    {
        private readonly IWatcherRepository _repository; //veri erişim işlemleri için
        public WatcherService(IWatcherRepository watcherRepository)
        {
            _repository = watcherRepository;
        }

        public async Task AddWatcherTableEntry(WatcherTables entry)
        {
            await _repository.Create(entry); // veriyi repository üzerinden veritabanına ekliyoruz
        }

        public async Task<IEnumerable<WatcherTables>> GetWatchersByFilter(DateTime startDate, DateTime endDate, string type)
        {
            // EFWatcher üzerinden filtreleme işlemini çağırıyoruz
            return await Task.FromResult(_repository.GetWatchersByFilter(startDate, endDate, type));
        }

    }
}

