using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Abstract
{
    public interface IGeneric<TEntity> where TEntity : class
    {
        Task<TEntity> GetById(int id);
        Task Create(TEntity entity);
        Task Update(int id, TEntity entity);
        Task Delete(int id);

        //Task FilteredGetAll(DateTime startTime, DateTime endTime, string type);

    }
}

