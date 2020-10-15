using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace IP1.Imaging
{
    class ConvertOpenCV
    {
        //convert Ip1.Imaging.Image to Mat
        public Mat ImageToMat(Image img)
        {
            //int stride = 0;
            Bitmap bmp = new Bitmap(img.Width, img.Height);
            for (int y = 0; y < bmp.Height; ++y)
            {
                for (int x = 0; x < bmp.Width; ++x)
                {
                    bmp.SetPixel(x, y, System.Drawing.Color.FromArgb(img[y, x].r, img[y, x].g, img[y, x].b));
                }
            }

            /*System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height);
            System.Drawing.Imaging.BitmapData bmpData = bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, bmp.PixelFormat);

            System.Drawing.Imaging.PixelFormat pf = bmp.PixelFormat;
            if (pf == System.Drawing.Imaging.PixelFormat.Format32bppArgb)
            {
                stride = bmp.Width * 4;
            }
            else
            {
                stride = bmp.Width * 3;
            }

            //System.Drawing.Image<Bgra, byte> cvImage = new System.Drawing.Image<Bgra, byte>(bmp.Width, bmp.Height, stride, (IntPtr)bmpData.Scan0);

            bmp.UnlockBits(bmpData);*/

            return bmp.ToMat();
        }

        //Convert to grayscale
        public System.Drawing.Image ConvertToGray(Image img)
        {
            Mat MatImage = ImageToMat(img);
            Mat imageGray = MatImage.CvtColor(ColorConversionCodes.RGB2GRAY);

            return imageGray.ToBitmap();
        }

        //Convert from RGB to HSV
        public System.Drawing.Image RGB2HSV(Image img)
        {
            Mat MatImage = ImageToMat(img);
            Mat imageHSV = MatImage.CvtColor(ColorConversionCodes.RGB2HSV);

            return imageHSV.ToBitmap();
        }

        //Convert HSV to RGB
        public System.Drawing.Image HSV2RGB(Image img)
        {
            Mat MatImage = ImageToMat(img);
            Mat imageRGB = MatImage.CvtColor(ColorConversionCodes.HSV2RGB);

            return imageRGB.ToBitmap();
        }
    }
}
