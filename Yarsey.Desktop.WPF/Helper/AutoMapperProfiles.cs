using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarsey.Desktop.WPF.ViewModels;
using Yarsey.Domain.Models;

namespace Yarsey.Desktop.WPF.Helper
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<NewCustomerViewModel, Customer>();
            CreateMap<ProductSelectionViewModel, ProductSelection>();
            CreateMap<NewInvoiceViewModel, Invoice>();

        }
    }
}
