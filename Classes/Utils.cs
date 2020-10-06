using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        }
    }
}
