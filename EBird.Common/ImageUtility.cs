using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace EBird.Common
{
    public class ImageUtility
    {
        public Image ResizeImage(Image image, Size size, bool preserveAspectRatio = true)
        {
            int newWidth;
            int newHeight;
            if (preserveAspectRatio)
            {
                int originalWidth = image.Width;
                int originalHeight = image.Height;
                if (originalWidth < size.Width && originalHeight < size.Height)
                {
                    newWidth = originalWidth;
                    newHeight = originalHeight;
                }
                else
                {
                    float percentWidth = (float)size.Width / (float)originalWidth;
                    float percentHeight = (float)size.Height / (float)originalHeight;
                    
                    float percent = 0;
                    percent = percentWidth;
                    newWidth = (int)(originalWidth * percent);
                    newHeight = (int)(originalHeight * percent);
                    if (newHeight > size.Height)
                    {
                        percent = percentHeight < percentWidth ? percentHeight : percentWidth;
                        newWidth = (int)(originalWidth * percent);
                        newHeight = (int)(originalHeight * percent);
                    }
                }
            }
            else
            {
                newWidth = size.Width;
                newHeight = size.Height;
            }
            Image newImage = new Bitmap(newWidth, newHeight);
            using (Graphics graphicsHandle = Graphics.FromImage(newImage))
            {
                graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphicsHandle.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return newImage;
        }
    }
}
