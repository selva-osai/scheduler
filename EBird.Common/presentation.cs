using System.Drawing;
using System.Windows.Forms;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;

namespace EBird.Common
{
    public class presentation
    {
        
        public void pptThumbnail(string sourceFile, string targetFile, int thumbW, int thumbH)
        {
            // Open the document and convert into HTML pages
            Microsoft.Office.Interop.PowerPoint.Application oApp = new Microsoft.Office.Interop.PowerPoint.Application();

            PowerPoint.Presentation oDoc = oApp.Presentations.Open(
                sourceFile,
                Microsoft.Office.Core.MsoTriState.msoTrue, // read only
                Microsoft.Office.Core.MsoTriState.msoTrue, // untitled
                Microsoft.Office.Core.MsoTriState.msoFalse); // with window
            string tmpHtmlFile = System.IO.Path.GetTempFileName() + ".html";
            oApp.Presentations[1].SaveCopyAs(
                tmpHtmlFile,
                PowerPoint.PpSaveAsFileType.ppSaveAsHTML, // format
                Microsoft.Office.Core.MsoTriState.msoTrue); // embed true type font

            // Create the thumbnail from the HTML pages
            Size browserSize = new Size(800, 800);
            WebBrowser browser = new WebBrowser();
            browser.Size = browserSize;
            browser.Navigate(tmpHtmlFile);
            while (WebBrowserReadyState.Complete != browser.ReadyState)
            {
                Application.DoEvents();
            }
            Bitmap bm = new Bitmap(browserSize.Width, browserSize.Height);
            browser.DrawToBitmap(bm,
                new Rectangle(0, 0, browserSize.Width, browserSize.Height));
            Bitmap thumbnail = new Bitmap(thumbW, thumbH);
            Graphics g = Graphics.FromImage(thumbnail);
            g.DrawImage(
                bm,
                new Rectangle(0, 0, thumbnail.Width, thumbnail.Height),
                new Rectangle(0, 0, browserSize.Width, browserSize.Height),
                GraphicsUnit.Pixel);
            thumbnail.Save(targetFile);
        } 

    }
}
