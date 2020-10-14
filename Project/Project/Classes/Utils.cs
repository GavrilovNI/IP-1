using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace IP1
{
    namespace Imaging
    {
        public static class Utils
        {
            public static float Clamp(float value, float min, float max)
            {
                if (min > max)
                    throw new Exception("Clamp error min>max");

                if (value < min)
                    return min;
                if (value > max)
                    return max;
                return value;
            }

            

            public static BitmapSource ImageToBitmapSource(IP1.Imaging.Image image)
            {
                PixelFormat pf = PixelFormats.Bgr24;
                int width = image.Width;
                int height = image.Height;
                int rawStride = (width * pf.BitsPerPixel + 7) / 8;
                byte[] rawImage = image.GetBytesBGR24().ToArray();

                return BitmapSource.Create(width, height,
                    96, 96, pf, null,
                    rawImage, rawStride);
            }

        }
    }
}
