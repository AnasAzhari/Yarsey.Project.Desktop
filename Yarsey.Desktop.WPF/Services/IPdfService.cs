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

        public void CreatePDF(object obj);

        public void CreateInvoicePDF(NewInvoiceViewModel newInvoiceViewModel);
    }
}
