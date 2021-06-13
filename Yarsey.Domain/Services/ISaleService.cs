using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarsey.Domain.Models;

namespace Yarsey.Domain.Services
{
    public interface ISaleService:IDataService<Sale>
    {

        Task<Sale> GetSaleById(int id);
        Task<Sale> GetSaleByRunningNo(string runningNo);

        Task<Sale> GetSaleByIdInBusiness(int businessId,int id);
        Task<Sale> GetSaleByRunninNoInBusiness(int businessId,string runningNo);
    }
}
