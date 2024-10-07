using EntityLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.WatcherDto
{
    public class WatcherResponseDto
    {
        public DateTime Time { get; set; }
        public string OldPath { get; set; }
        public string? NewPath { get; set; }
    }
}
