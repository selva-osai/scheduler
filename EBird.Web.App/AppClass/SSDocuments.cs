using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using DocumentFormat.OpenXml.Packaging;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;

namespace EBird.Web.AppClass
{
    public class SSDocuments
    {
        public int PPTGetSlideCount(string fileName, bool includeHidden = true)
        {
            int slidesCount = 0;
            try
            {
                if (File.Exists(fileName))
                {
                    using (PresentationDocument doc = PresentationDocument.Open(fileName, false))
                    {
                        // Get the presentation part of the document.
                        PresentationPart presentationPart = doc.PresentationPart;
                        if (presentationPart != null)
                        {
                            if (includeHidden)
                            {
                                slidesCount = presentationPart.SlideParts.Count();
                            }
                            else
                            {
                                var slides = presentationPart.SlideParts.Where(
                                    (s) => (s.Slide != null) &&
                                    ((s.Slide.Show == null) ||
                                    (s.Slide.Show.HasValue && s.Slide.Show.Value)));
                                slidesCount = slides.Count();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
             
            }
            return slidesCount;
        }

        public int GetPPTSlideCount(string fileName)
        {
            int slideCount = 0;
            try
            {
                Application pptApplication = new Application();
                Presentation pptPresentation = pptApplication.Presentations.Open(fileName, MsoTriState.msoFalse, MsoTriState.msoFalse, MsoTriState.msoFalse);
                slideCount = pptPresentation.Slides.Count;
                //pptPresentation.Slides.Item[1].Export("slide.jpg", "jpg", 320, 240);
            }
            catch (Exception ex)
            {
            }
            return slideCount;
        }
    }
}