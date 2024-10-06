﻿using EntityLayer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Abstract
{
    public interface IWatcherService
    {
        Task AddWatcherTableEntry(WatcherTables watcherTable);
        //Task<IEnumerable<WatcherTables>> GetWatcherTables(); //api verileri çeksin diye

        // zaman araığı ve işlem türüne göre veri çekme metodum
        Task<IEnumerable<WatcherTables>> GetWatchersByFilter(DateTime startDate, DateTime endDate, string type);
    }
}
