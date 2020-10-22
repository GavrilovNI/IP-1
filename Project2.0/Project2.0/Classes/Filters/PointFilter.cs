using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IP1.Imaging.ColorNS;

namespace IP1.Imaging.Filters
{
    public abstract class PointFilter : Filter
    {
        public override Image<T> Run<T, Y>(Image<Y> image)
        {
            Image<T> result = new Image<T>((uint)image.Width, (uint)image.Height);
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    dynamic d = image[y, x];
                    result[y, x] = RunColor<T>(d);
                    
                }
            }
            return result;
        }

        /*private IColor RunColor(object color)
        {
            throw new Exception("Wrong type of Color");
        }*/

        public abstract T RunColor<T>(ColorRGB color) where T: IColor;

        public abstract T RunColor<T>(ColorHSV color) where T : IColor;
    }
}
