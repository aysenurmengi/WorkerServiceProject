using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer;

namespace EntityLayer
{
    public class WatcherTables
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public string OldPath { get; set; }
        public string? NewPath { get; set; }
        public DateTime Time { get; set; }

        public IQueryable<WatcherTables> Where(Func<object, bool> value)
        {
            throw new NotImplementedException();
        }
    }
}
