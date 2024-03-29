﻿using System;
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
using System.Data;
using System.ComponentModel;
using System.Globalization;

namespace Yarsey.Desktop.WPF.Services
{
    public class PdfService : IPdfService
    {
        private readonly BusinessStore businessStore;

        public PdfService(BusinessStore businessStore)
        {
            this.businessStore = businessStore;
        }


        public void CreateInvoicePDF(NewInvoiceViewModel newInvoiceViewModel,Business business)
        {
            var globalization = System.Globalization.CultureInfo.CurrentCulture;

            string invoiceFolder = SettingsConfiguration.GetInvoiceFolder(this.businessStore.CurrentBusiness);
            string invoicefileName = newInvoiceViewModel.CurrentRunningNo+".pdf";

            string completeName = Path.Combine(invoiceFolder, invoicefileName);

            PdfDocument document = new PdfDocument();
            document.PageSettings.Orientation = PdfPageOrientation.Portrait;
            document.PageSettings.Margins.All = 30;
            PdfPage page = document.Pages.Add();
            PdfGraphics g = page.Graphics;
            
            PdfTextElement element = new PdfTextElement(@$"{business.BusinessName}
            {business.Adresss}
            {business.PhoneNo}");
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

            element = new PdfTextElement("INVOICExx " + newInvoiceViewModel.CurrentRunningNo,subHeadingFont);
            element.Brush = PdfBrushes.White;
            result = element.Draw(page, new PointF(10, result.Bounds.Bottom + 48));
            string currentDate = "DATE : " + newInvoiceViewModel.InvoiceDate.ToShortDateString();
            SizeF textSize = subHeadingFont.MeasureString(currentDate);
            g.DrawString(currentDate, subHeadingFont, element.Brush, new PointF(g.ClientSize.Width - textSize.Width - 10, result.Bounds.Y));

            element = new PdfTextElement("BILL TO ", new PdfStandardFont(PdfFontFamily.TimesRoman, 12));
            element.Brush = new PdfSolidBrush(new PdfColor(34, 83, 142));
            result = element.Draw(page, new PointF(10, result.Bounds.Bottom + 25));

            g.DrawLine(new PdfPen(new PdfColor(34, 83, 142), 0.70f), new PointF(0, result.Bounds.Bottom + 3), new PointF(g.ClientSize.Width, result.Bounds.Bottom + 3));

            element = new PdfTextElement(newInvoiceViewModel.SelectedCustomer.Name, new PdfStandardFont(PdfFontFamily.TimesRoman, 11));
            element.Brush = new PdfSolidBrush(new PdfColor(0, 0, 0));
            result = element.Draw(page, new RectangleF(10, result.Bounds.Bottom + 5, g.ClientSize.Width / 2, 100));

            element = new PdfTextElement(newInvoiceViewModel.Adress, new PdfStandardFont(PdfFontFamily.TimesRoman, 11));
            element.Brush = new PdfSolidBrush(new PdfColor(0, 0, 0));
            result = element.Draw(page, new RectangleF(10, result.Bounds.Bottom + 3, g.ClientSize.Width / 2, 100));



            PdfGrid grid = new PdfGrid();
            grid.DataSource = ConvertToDataTable(newInvoiceViewModel.ProductSelections);
            PdfGridCellStyle cellStyle = new PdfGridCellStyle();
            cellStyle.Borders.All = PdfPens.White;
            PdfGridRow header = grid.Headers[0];

            PdfGridCellStyle headerStyle = new PdfGridCellStyle();
            headerStyle.Borders.All = new PdfPen(new PdfColor(34, 83, 142));
            headerStyle.BackgroundBrush = new PdfSolidBrush(new PdfColor(34, 83, 142));
            headerStyle.TextBrush = PdfBrushes.White;
            headerStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 12f, PdfFontStyle.Regular);

            for (int i = 0; i < header.Cells.Count; i++)
            {
                if (i == 0)
                    header.Cells[i].StringFormat = new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle);
                else
                    header.Cells[i].StringFormat = new PdfStringFormat(PdfTextAlignment.Right, PdfVerticalAlignment.Middle);
            }
            header.Cells[0].Value = "ITEM";
            header.Cells[1].Value = "QUANTITY";
            header.Cells[2].Value = "RATE";
            header.Cells[3].Value = "TAXES";
            //header.Cells[4].Value = "AMOUNT";
            header.ApplyStyle(headerStyle);
            grid.Columns[0].Width = 180;
            cellStyle.Borders.Bottom = new PdfPen(new PdfColor(34, 83, 142), 0.70f);
            cellStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 11f);
            cellStyle.TextBrush = new PdfSolidBrush(new PdfColor(0, 0, 0));
            foreach (PdfGridRow row in grid.Rows)
            {
                row.ApplyStyle(cellStyle);
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    PdfGridCell cell = row.Cells[i];
                    if (i == 0)
                        cell.StringFormat = new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle);
                    else
                        cell.StringFormat = new PdfStringFormat(PdfTextAlignment.Right, PdfVerticalAlignment.Middle);

                    if (i > 1)
                    {
                        if (cell.Value.ToString().Contains("$"))
                        {
                            cell.Value = cell.Value.ToString();
                        }
                        else
                        {
                            if (cell.Value is double)
                                cell.Value = "$" + ((double)cell.Value).ToString("#,###.00", CultureInfo.InvariantCulture);
                            else
                                cell.Value = "$" + cell.Value.ToString();
                        }
                    }
                }
            }

            PdfGridLayoutFormat layoutFormat = new PdfGridLayoutFormat();
            layoutFormat.Layout = PdfLayoutType.Paginate;

            PdfGridLayoutResult gridResult = grid.Draw(page, new RectangleF(new PointF(0, result.Bounds.Bottom + 40), new SizeF(g.ClientSize.Width, g.ClientSize.Height - 100)), layoutFormat);
            float pos = 0.0f;
            for (int i = 0; i < grid.Columns.Count - 1; i++)
                pos += grid.Columns[i].Width;

            PdfFont font = new PdfStandardFont(PdfFontFamily.TimesRoman, 12f, PdfFontStyle.Bold);
            gridResult.Page.Graphics.DrawString("TOTAL DUE", font, new PdfSolidBrush(new PdfColor(34, 83, 142)), new RectangleF(new PointF(pos, gridResult.Bounds.Bottom + 10), new SizeF(grid.Columns[3].Width - pos, 20)), new PdfStringFormat(PdfTextAlignment.Right));
            gridResult.Page.Graphics.DrawString("Thank you for your business!", new PdfStandardFont(PdfFontFamily.TimesRoman, 12), new PdfSolidBrush(new PdfColor(0, 0, 0)), new PointF(pos - 210, gridResult.Bounds.Bottom + 60));
            pos += grid.Columns[3].Width;

            decimal total = newInvoiceViewModel.Total;

            gridResult.Page.Graphics.DrawString("$" + total.ToString("#,###.00", CultureInfo.InvariantCulture), font, new PdfSolidBrush(new PdfColor(0, 0, 0)), new RectangleF(pos, gridResult.Bounds.Bottom + 10, grid.Columns[3].Width - pos, 20), new PdfStringFormat(PdfTextAlignment.Right));




            document.Save(completeName);
            //Message box confirmation to view the created PDF document.
            if (MessageBox.Show("Do you want to view the PDF file?", "PDF File Created",
                 MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                //Launching the PDF file using the default Application.[Acrobat Reader]
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo = new System.Diagnostics.ProcessStartInfo(completeName) { UseShellExecute = true };
                process.Start();
            }

            document.Close(true);
            document.Dispose();

        }

        public void CreatePDF(Invoice invoice, Business business)
        {
            var globalization = System.Globalization.CultureInfo.CurrentCulture;

            string invoiceFolder = SettingsConfiguration.GetInvoiceFolder(this.businessStore.CurrentBusiness);
            string invoicefileName = invoice.ref_no + ".pdf";

            string completeName = Path.Combine(invoiceFolder, invoicefileName);

            PdfDocument document = new PdfDocument();
            document.PageSettings.Orientation = PdfPageOrientation.Portrait;
            document.PageSettings.Margins.All = 30;
            PdfPage page = document.Pages.Add();
            PdfGraphics g = page.Graphics;

            PdfTextElement element = new PdfTextElement(@$"{business.BusinessName}
            {business.Adresss}
            {business.PhoneNo}");
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

            element = new PdfTextElement("INVOICExx " + invoice.ref_no, subHeadingFont);
            element.Brush = PdfBrushes.White;
            result = element.Draw(page, new PointF(10, result.Bounds.Bottom + 48));
            string currentDate = "DATE : " + invoice.InvoiceDate.ToShortDateString();
            SizeF textSize = subHeadingFont.MeasureString(currentDate);
            g.DrawString(currentDate, subHeadingFont, element.Brush, new PointF(g.ClientSize.Width - textSize.Width - 10, result.Bounds.Y));

            element = new PdfTextElement("BILL TO ", new PdfStandardFont(PdfFontFamily.TimesRoman, 12));
            element.Brush = new PdfSolidBrush(new PdfColor(34, 83, 142));
            result = element.Draw(page, new PointF(10, result.Bounds.Bottom + 25));

            g.DrawLine(new PdfPen(new PdfColor(34, 83, 142), 0.70f), new PointF(0, result.Bounds.Bottom + 3), new PointF(g.ClientSize.Width, result.Bounds.Bottom + 3));

            element = new PdfTextElement(invoice.Customer.Name, new PdfStandardFont(PdfFontFamily.TimesRoman, 11));
            element.Brush = new PdfSolidBrush(new PdfColor(0, 0, 0));
            result = element.Draw(page, new RectangleF(10, result.Bounds.Bottom + 5, g.ClientSize.Width / 2, 100));

            element = new PdfTextElement(invoice.Adress, new PdfStandardFont(PdfFontFamily.TimesRoman, 11));
            element.Brush = new PdfSolidBrush(new PdfColor(0, 0, 0));
            result = element.Draw(page, new RectangleF(10, result.Bounds.Bottom + 3, g.ClientSize.Width / 2, 100));



            PdfGrid grid = new PdfGrid();
            grid.DataSource = ConvertToDataTable(invoice.ProductsSelected.ToList());
            PdfGridCellStyle cellStyle = new PdfGridCellStyle();
            cellStyle.Borders.All = PdfPens.White;
            PdfGridRow header = grid.Headers[0];

            PdfGridCellStyle headerStyle = new PdfGridCellStyle();
            headerStyle.Borders.All = new PdfPen(new PdfColor(34, 83, 142));
            headerStyle.BackgroundBrush = new PdfSolidBrush(new PdfColor(34, 83, 142));
            headerStyle.TextBrush = PdfBrushes.White;
            headerStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 12f, PdfFontStyle.Regular);

            for (int i = 0; i < header.Cells.Count; i++)
            {
                if (i == 0)
                    header.Cells[i].StringFormat = new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle);
                else
                    header.Cells[i].StringFormat = new PdfStringFormat(PdfTextAlignment.Right, PdfVerticalAlignment.Middle);
            }
            header.Cells[0].Value = "ITEM";
            header.Cells[1].Value = "QUANTITY";
            header.Cells[2].Value = "RATE";
            header.Cells[3].Value = "TAXES";
            //header.Cells[4].Value = "AMOUNT";
            header.ApplyStyle(headerStyle);
            grid.Columns[0].Width = 180;
            cellStyle.Borders.Bottom = new PdfPen(new PdfColor(34, 83, 142), 0.70f);
            cellStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 11f);
            cellStyle.TextBrush = new PdfSolidBrush(new PdfColor(0, 0, 0));
            foreach (PdfGridRow row in grid.Rows)
            {
                row.ApplyStyle(cellStyle);
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    PdfGridCell cell = row.Cells[i];
                    if (i == 0)
                        cell.StringFormat = new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle);
                    else
                        cell.StringFormat = new PdfStringFormat(PdfTextAlignment.Right, PdfVerticalAlignment.Middle);

                    if (i > 1)
                    {
                        if (cell.Value.ToString().Contains("$"))
                        {
                            cell.Value = cell.Value.ToString();
                        }
                        else
                        {
                            if (cell.Value is double)
                                cell.Value = "$" + ((double)cell.Value).ToString("#,###.00", CultureInfo.InvariantCulture);
                            else
                                cell.Value = "$" + cell.Value.ToString();
                        }
                    }
                }
            }

            PdfGridLayoutFormat layoutFormat = new PdfGridLayoutFormat();
            layoutFormat.Layout = PdfLayoutType.Paginate;

            PdfGridLayoutResult gridResult = grid.Draw(page, new RectangleF(new PointF(0, result.Bounds.Bottom + 40), new SizeF(g.ClientSize.Width, g.ClientSize.Height - 100)), layoutFormat);
            float pos = 0.0f;
            for (int i = 0; i < grid.Columns.Count - 1; i++)
                pos += grid.Columns[i].Width;

            PdfFont font = new PdfStandardFont(PdfFontFamily.TimesRoman, 12f, PdfFontStyle.Bold);
            gridResult.Page.Graphics.DrawString("TOTAL DUE", font, new PdfSolidBrush(new PdfColor(34, 83, 142)), new RectangleF(new PointF(pos, gridResult.Bounds.Bottom + 10), new SizeF(grid.Columns[3].Width - pos, 20)), new PdfStringFormat(PdfTextAlignment.Right));
            gridResult.Page.Graphics.DrawString("Thank you for your business!", new PdfStandardFont(PdfFontFamily.TimesRoman, 12), new PdfSolidBrush(new PdfColor(0, 0, 0)), new PointF(pos - 210, gridResult.Bounds.Bottom + 60));
            pos += grid.Columns[3].Width;

            decimal total = Helper.Helper.CalculateInvoiceTotal(invoice);

            gridResult.Page.Graphics.DrawString("$" + total.ToString("#,###.00", CultureInfo.InvariantCulture), font, new PdfSolidBrush(new PdfColor(0, 0, 0)), new RectangleF(pos, gridResult.Bounds.Bottom + 10, grid.Columns[3].Width - pos, 20), new PdfStringFormat(PdfTextAlignment.Right));




            document.Save(completeName);
            //Message box confirmation to view the created PDF document.
            //if (MessageBox.Show("Do you want to view the PDF file?", "PDF File Created",
            //     MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            //{
            //    //Launching the PDF file using the default Application.[Acrobat Reader]
            //    System.Diagnostics.Process process = new System.Diagnostics.Process();
            //    process.StartInfo = new System.Diagnostics.ProcessStartInfo(completeName) { UseShellExecute = true };
            //    process.Start();
            //}

            document.Close(true);
            document.Dispose();

        }
        private DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
           TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
            {
                //if (prop.PropertyType == typeof(double))
                //    table.Columns.Add(prop.Name, typeof(string));
                //else
                //    table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                if (prop.Name == "Tax" || prop.Name== "Quantity"|| prop.Name == "PricePerItem" || prop.Name== "Discount")
                {
                    table.Columns.Add(prop.Name, typeof(string));
                }

            }
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                DataColumnCollection dataColumn = table.Columns;
                foreach (PropertyDescriptor prop in properties)
                {
                    if(prop.Name == "Tax" || prop.Name == "Quantity" || prop.Name == "PricePerItem" || prop.Name == "Discount")
                    {
                        object value = prop.GetValue(item) ?? DBNull.Value;
                        if (value is decimal)
                            row[prop.Name] = ((decimal)value).ToString("#,###.00", CultureInfo.InvariantCulture);
                        else
                            row[prop.Name] = value;
                    }

                   
                }

                table.Rows.Add(row);
            }
            return table;
        }


    }
}
