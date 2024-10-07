using EntityLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.WatcherDto
{
    public class WatcherRequestDto
    {
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public FilterTypeEnum Type { get; set; }

    }
}
