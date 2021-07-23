using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarsey.Desktop.WPF.ViewModels;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using System.Drawing;
using System.Reflection;
using Yarsey.Domain.Models;
using System.IO;
using System.Windows;
using Yarsey.Desktop.WPF.Stores;

namespace Yarsey.Desktop.WPF.Services
{
    public class PdfService : IPdfService
    {
        private readonly BusinessStore businessStore;

        public PdfService(BusinessStore businessStore)
        {
            this.businessStore = businessStore;
        }


        public void CreateInvoicePDF(NewInvoiceViewModel newInvoiceViewModel)
        {
            PdfDocument document = new PdfDocument();
            document.PageSettings.Orientation = PdfPageOrientation.Portrait;
            document.PageSettings.Margins.All = 50;
            PdfPage page = document.Pages.Add();
            PdfGraphics g = page.Graphics;
            PdfTextElement element = new PdfTextElement($@"Syncfusiondd{newInvoiceViewModel.SelectedCustomer.Name}asfasf
            {newInvoiceViewModel.SelectedCustomer.Adress}
            ");
            element.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 12);
            element.Brush = new PdfSolidBrush(new PdfColor(0, 0, 0));
            PdfLayoutResult result = element.Draw(page, new RectangleF(0, 0, page.Graphics.ClientSize.Width / 2, 200));
            Assembly assembly = typeof(PdfService).Assembly;


            if (businessStore.CurrentBusiness.Image != null)
            {
                PdfImage img = PdfImage.FromStream(new MemoryStream(businessStore.CurrentBusiness.Image));
                page.Graphics.DrawImage(img, new RectangleF(g.ClientSize.Width - 200, result.Bounds.Y, 190, 45));

            }
            PdfFont subHeadingFont = new PdfStandardFont(PdfFontFamily.TimesRoman, 14);
            g.DrawRectangle(new PdfSolidBrush(new PdfColor(34, 83, 142)), new RectangleF(0, result.Bounds.Bottom + 40, g.ClientSize.Width, 30));

            element = new PdfTextElement("INVOICE " + newInvoiceViewModel.CurrentRunningNo,subHeadingFont);
            element.Brush = PdfBrushes.White;
            result = element.Draw(page, new PointF(10, result.Bounds.Bottom + 48));
            string currentDate = "DATE " + newInvoiceViewModel.InvoiceDate.ToLongDateString();
            SizeF textSize = subHeadingFont.MeasureString(currentDate);
            g.DrawString(currentDate, subHeadingFont, element.Brush, new PointF(g.ClientSize.Width - textSize.Width - 10, result.Bounds.Y));

            document.Save("Invoice.pdf");
            //Message box confirmation to view the created PDF document.
            if (MessageBox.Show("Do you want to view the PDF file?", "PDF File Created",
                 MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                //Launching the PDF file using the default Application.[Acrobat Reader]
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo = new System.Diagnostics.ProcessStartInfo("Invoice.pdf") { UseShellExecute = true };
                process.Start();
            }

            document.Close(true);


        }

        public void CreatePDF(object obj)
        {
            throw new NotImplementedException();
        }


    }
}
