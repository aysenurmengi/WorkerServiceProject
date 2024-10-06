using DataLayer.Abstract;
using DataLayer.Repository;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.EntityFramework
{
    public class EFWatcher : GenericRepository<WatcherTables>, IWatcherRepository
    {
        public EFWatcher(ServiceContext serviceContext) : base(serviceContext)
        {

        }
        
        public IEnumerable<WatcherTables> GetWatchersByFilter(DateTime startDate, DateTime endDate, string type)
        {
            return Where(w => w.Time >= startDate && w.Time <= endDate && w.Type == type);
        }

    }
}
