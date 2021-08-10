using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarsey.Domain.Models;

namespace Yarsey.Domain.Services
{
    public interface IAccountService
    {

        Task<IEnumerable<Account>> GetBusinessAccounts(int bizId);

        Task GenerateDefaultAccounts(int bizId);
    }
}
