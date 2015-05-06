using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WriteToImage
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Image origImage = Image.FromFile(@"C:\Users\Adam\Pictures\ScrapedMasteries\mastery_offense_bg.jpg"))
            {

                using (Bitmap newImage = new Bitmap(origImage.Width, origImage.Height))
                {
                    RectangleF rectf = new RectangleF(85, 480, 200, 200);
                    Rectangle destination = new Rectangle(0, 0, origImage.Width, origImage.Height);
                    using (Graphics g = Graphics.FromImage(newImage))
                    {
                        g.DrawImage(origImage, destination);

                        g.SmoothingMode = SmoothingMode.AntiAlias;
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        g.DrawString("30 Offense", new Font("Arial", 14), Brushes.White, rectf);

                        g.Flush();
                    }

                    newImage.Save(@"C:\Users\Adam\Pictures\ScrapedMasteries\mastery_offense_bg_tmp.jpg", ImageFormat.Jpeg);
                }
            }



        }
    }
}
