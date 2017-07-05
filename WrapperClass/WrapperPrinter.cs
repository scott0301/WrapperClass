using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 
using System.Windows.Forms; // for printDialog
using System.Drawing.Printing; // for printDocument 
using System.Drawing;

namespace WrapperUnion
{
    public class WrapperPrinter
    {
        // 선행조건이다.
        // WrapperPrinter printer = new WrapperPrinter();
        // printer.printDocument.PrintPage += new PrintPageEventHandler(docToPrint_PrintPage);

        PrintDialog printDialog = new PrintDialog();
        public PrintDocument printDocument = new PrintDocument();

        public WrapperPrinter()
        {
            PageSettings ps = new PageSettings();
            ps.Margins = new Margins(10, 10, 10, 10);

            printDocument.DefaultPageSettings = ps;
        }
        public void ShowPrintPreview()
        {
            PrintPreviewDialog pd = new PrintPreviewDialog();
            pd.ClientSize = new Size(1024, 768);
            pd.UseAntiAlias = true;
            pd.Document = printDocument;
            
            pd.Show();
        }
        public string SetupPrint()
        {
            string printerName = string.Empty;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printerName = printDocument.PrinterSettings.PrinterName = printDialog.PrinterSettings.PrinterName;
                printDocument.PrinterSettings.Copies = printDialog.PrinterSettings.Copies;
            }
            return printerName;
        }

        public string GetPrinterName()
        {
            return printDocument.PrinterSettings.PrinterName = printDialog.PrinterSettings.PrinterName;
        }
        public  void DrawString(PrintPageEventArgs e, string s, Point pt, int nSize)
        {
            using (Font myFont = new Font("맑은고딕", nSize, FontStyle.Bold))
            {
                e.Graphics.DrawString(s, myFont, Brushes.Black, pt);
            }
        }

        public void FitToPage(PrintPageEventArgs e, string strImagePath)
        {
            System.Drawing.Image img = System.Drawing.Image.FromFile(@strImagePath);

            //Adjust the size of the image to the page to print the full image without loosing any part of it
            Rectangle m = e.MarginBounds;

            if ((double)img.Width / (double)img.Height > (double)m.Width / (double)m.Height) // image is wider
            {
                m.Height = (int)((double)img.Height / (double)img.Width * (double)m.Width);
            }
            else
            {
                m.Width = (int)((double)img.Width / (double)img.Height * (double)m.Height);
            }
            e.Graphics.DrawImage(img, m);
        }
    }
}
