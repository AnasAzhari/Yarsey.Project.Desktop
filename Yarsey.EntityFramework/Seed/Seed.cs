using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarsey.Domain.Models;

namespace Yarsey.EntityFramework.Seed
{
    public class Seed
    {
        public async Task SeedReference(YarseyDbContext yarseyDbContext,List<String> listModule)
        {
            if (await yarseyDbContext.RunningNumbers.AnyAsync()) return;

            if(listModule!=null)
            {
                if (listModule.Count > 0)
                {
                    foreach (var item in listModule)
                    {
                        var runningnoModule = new RunningNumber() { ModuleName = item, RunningNo = 0 };

                        yarseyDbContext.RunningNumbers.Add(runningnoModule);

                    }
                    await yarseyDbContext.SaveChangesAsync();

                }
            }


        }
    }
}
