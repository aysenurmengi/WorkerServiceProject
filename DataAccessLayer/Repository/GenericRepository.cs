using DataLayer.Abstract;
using EntityLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DataLayer.Repository
{
    //TEntity vermemimizin sebebi farklı entityler olduğunda tek bir yerden işlemleri gerçekleştirmek
    public class GenericRepository<TEntity> : IGeneric<TEntity> where TEntity : class
    {
        private readonly ServiceContext _serviceContext;

        //TEntity'e ait verileri doğrudan veritabanından çekmek, eklemek vs. için
        private readonly DbSet<TEntity> _dbSet; 
        public GenericRepository(ServiceContext serviceContext)
        {
            _serviceContext = serviceContext;
            // generic'in T türündeki her tabloyu yönetmesini sağlıyor
            _dbSet = _serviceContext.Set<TEntity>(); 
        }

        //TEntity türündeki nesnenin koşula uygunluğunun kontrolü için bool kullandık
        public IEnumerable<TEntity> Where(Func<TEntity, bool> predicate)
        { //predicate -> bu fonk. nesneler üzerinde koşullu işlemlerle filtreleme sağlar 
            return _dbSet.Where(predicate).ToList();
        }

        
        //sadece okuma yapacağımızı söylüyoruz
        public IQueryable<TEntity> GetAll()
        {
            return _serviceContext.Set<TEntity>().AsNoTracking(); 
        }
        //AsNoTracking ->sadece okuma yapacağımız güncelleme yapmayacağımız işlemlerde
        public async Task<TEntity> GetById(int id)
        {
            return await _serviceContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(e => EF.Property<int>(e, "ID") == id);
        }

        public async Task Create(TEntity entity)
        {
            try
            {
                await _serviceContext.Set<TEntity>().AddAsync(entity);
                await _serviceContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                Console.WriteLine("GenericRepository hatalı Create işlemi!");
            }
            
        }

        public async Task Delete(int id)
        {
            try
            {
                var entity = await GetById(id);
                if (entity != null)
                {
                    _serviceContext.Set<TEntity>().Remove(entity);
                    await _serviceContext.SaveChangesAsync(); // Silme işleminden sonra SaveChangesAsync çağrıldı.
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("GenericRepository hatalı Delete işlemi!");
               
            }
            
        }

        public async Task Update(int id, TEntity entity)
        {
            try
            {
                var existingEntity = await GetById(id);
                if (existingEntity != null)
                {
                    _serviceContext.Entry(existingEntity).CurrentValues.SetValues(entity); // Güncelleme işlemi doğru yapıldı.
                    await _serviceContext.SaveChangesAsync(); // Güncellemeden sonra SaveChangesAsync çağrıldı.
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("GenericRepository hatalı Update işlemi!");
            }
            
        }
    }
}
