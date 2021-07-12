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
        public static async Task SeedReference(YarseyDbContext yarseyDbContext,List<String> listModule)
        {

            if(listModule!=null)
            {
         
                if (listModule.Count > 0)
                {
                    foreach (var module in listModule)
                    {
                        if (await yarseyDbContext.RunningNumbers.Where(x => x.ModuleName == module).AnyAsync()) continue;

                        var runningnoModule = new RunningNumber() { ModuleName = module, RunningNo = 0 };

                        yarseyDbContext.RunningNumbers.Add(runningnoModule);
                    }
                    await yarseyDbContext.SaveChangesAsync();
                }

            }


        }
    }
}
