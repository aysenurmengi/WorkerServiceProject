using EntityLayer;
using EntityLayer.WatcherDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Abstract
{
    public interface IWatcherRepository : IGeneric<WatcherTables>
    {
        IQueryable<WatcherTables> GetAll(); // getall() vardı ama başını api için değiştim
        IEnumerable<WatcherTables> GetWatchersByFilter(WatcherRequestDto request); //metodu kullanmak için burda tanımladım
    }
}
