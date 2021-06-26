using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarsey.Domain.Models;
using Yarsey.Domain.Services;
using Yarsey.EntityFramework.Services.Common;

namespace Yarsey.EntityFramework.Services
{
    public class BusinessDataService : IBusinessService
    {
        private readonly YarseyDbContextFactory _yarseyDbContextFactory;

        private readonly NonQueryDataService<Business> _nonQueryDataService;

        public BusinessDataService(YarseyDbContextFactory contextFactory)
        {
            _yarseyDbContextFactory = contextFactory;
            _nonQueryDataService = new NonQueryDataService<Business>(contextFactory);
        }

        public async Task<Business> Create(Business entity)
        {
            return await _nonQueryDataService.Create(entity);
        }

        public async Task<bool> Delete(int id)
        {
            return await _nonQueryDataService.Delete(id);
        }

        public async Task DeleteProduct(Product product)
        {
            using (YarseyDbContext dbContext = _yarseyDbContextFactory.CreateDbContext())
            {
                Product entity = await dbContext.Products.FirstOrDefaultAsync(x => x.Id == product.Id);
                dbContext.Products.Remove(entity);
                await dbContext.SaveChangesAsync();

            }
        }

        public async Task<Business> GetDefault()
        {
            using (YarseyDbContext dbContext = _yarseyDbContextFactory.CreateDbContext())
            {
                Business entity = await dbContext.Businesses.Include(c => c.Customers).Include(p=>p.Products)
                                        .FirstOrDefaultAsync();
                return entity;
            }
        }

        public async Task<Business> Get(int id)
        {
            using (YarseyDbContext dbContext = _yarseyDbContextFactory.CreateDbContext())
            {
                Business entity = await dbContext.Businesses.Include(c=>c.Customers).Include(p=>p.Products)
                                        .FirstOrDefaultAsync((a) => a.Id == id);
                return entity;
            }
        }

        public async Task<IEnumerable<Business>> GetAll()
        {
            using (YarseyDbContext dbContext = _yarseyDbContextFactory.CreateDbContext())
            {
                IEnumerable<Business> businesses = await dbContext.Businesses
                                                        .ToListAsync();
                return businesses;
            }
        }

        public async Task<Business> GetBusinessByName(string name)
        {
            using (YarseyDbContext dbContext=_yarseyDbContextFactory.CreateDbContext())
            {
                Business businesses = await dbContext.Businesses.Where(s => s.BusinessName==name.ToLower()).FirstOrDefaultAsync();

                return businesses;
            }
           
        }

        public async Task<Business> Update(int id, Business entity)
        {
            return await _nonQueryDataService.Update(id, entity);
        }

        public async Task AddCustomer(int bizId,Customer customer)
        {
            using (YarseyDbContext dbContext = _yarseyDbContextFactory.CreateDbContext())
            {
                Business businesses = await dbContext.Businesses.Include(p=>p.Customers).FirstOrDefaultAsync(x=>x.Id==bizId);

                businesses.Customers.Add(customer);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task AddProduct(int bizId, Product product)
        {
            using (YarseyDbContext dbContext = _yarseyDbContextFactory.CreateDbContext())
            {
                Business businesses = await dbContext.Businesses.Include(p => p.Products).FirstOrDefaultAsync(x => x.Id == bizId);

                businesses.Products.Add(product);
                await dbContext.SaveChangesAsync();
            }
        }


    }
}
