using DataLayer.Abstract;
using DataLayer.Repository;
using EntityLayer;
using EntityLayer.Enums;
using EntityLayer.WatcherDto;
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
        
        public IEnumerable<WatcherTables> GetWatchersByFilter(WatcherRequestDto request)
        {
            //saat muhabbetinden burdaki .Date'ler sayesinde kurtuldum
            return Where(w => w.Time.Date >= request.startDate.Date && w.Time.Date <= request.endDate.Date && w.Type == request.Type.ToString());
        }

    }
}
