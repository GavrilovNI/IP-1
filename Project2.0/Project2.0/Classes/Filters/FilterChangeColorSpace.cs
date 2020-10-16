using IP1.Imaging.ColorNS;
using IP1.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IP1.Imaging.Filters
{
    public class FilterChangeColorSpace : PointFilter
    {
        public override T RunColor<T>(ColorRGB color)
        {
            return RunColor<T>(color);
        }
        public override T RunColor<T>(ColorHSV color)
        {
            return RunColor<T>(color);
            /*if (typeof(T) == typeof(ColorRGB))
                return (T)((IColor)(new ColorRGB(color)));
            else if (typeof(T) == typeof(ColorHSV))
                return (T)((IColor)(new ColorHSV(color)));
            else
                throw new Exception("Wrong return type");*/
        }

        public static T RunColor<T>(IColor color) where T: IColor
        {
            dynamic d = color;
            if (typeof(T) == typeof(ColorRGB))
                return (T)((IColor)(new ColorRGB(d)));
            else if (typeof(T) == typeof(ColorHSV))
                return (T)((IColor)(new ColorHSV(d)));
            else
                throw new Exception("Wrong return type");
        }
    }
}
