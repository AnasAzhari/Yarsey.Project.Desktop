using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarsey.Domain.Models;
using Yarsey.Domain.Services;
using Yarsey.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Yarsey.EntityFramework.Services.Common
{
    public class NonQueryDataService<T> where T:DomainObject
    {
        private readonly YarseyDbContextFactory _contextFactory;

        public NonQueryDataService(YarseyDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<T> Create(T entity)
        {
            using (YarseyDbContext yarseyDbContext = _contextFactory.CreateDbContext())
            {
                EntityEntry<T> createdResult = await yarseyDbContext.Set<T>().AddAsync(entity);
                await yarseyDbContext.SaveChangesAsync();
                return createdResult.Entity;
            }
        }
        public async Task<T> Update(int id,T entity)
        {
            using (YarseyDbContext context = _contextFactory.CreateDbContext())
            {
                entity.Id = id;
                context.Set<T>().Update(entity);
                await context.SaveChangesAsync();
                return entity;
            }
        }
        public async Task<bool> Delete(int id)
        {
            using (YarseyDbContext dbContext  =_contextFactory.CreateDbContext())
            {
                T entity = await dbContext.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);
                dbContext.Set<T>().Remove(entity);
                await dbContext.SaveChangesAsync();
                return true;
            }
        }
    }
}
