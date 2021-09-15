using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarsey.Desktop.WPF.ViewModels;
using Yarsey.Domain.Models;

namespace Yarsey.Desktop.WPF.Services
{
    public interface IPdfService
    {

        public void CreatePDF(Invoice invoice,Business business);

        public void CreateInvoicePDF(NewInvoiceViewModel newInvoiceViewModel,Business business);
    }
}
