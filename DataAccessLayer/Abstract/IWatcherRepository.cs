using EntityLayer;
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
        IEnumerable<WatcherTables> GetWatchersByFilter(DateTime startDate, DateTime endDate, string type); //metodu kullanmak için burda tanımladım
    }
}
