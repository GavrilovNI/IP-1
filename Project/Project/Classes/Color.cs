using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IP1
{
    namespace Imaging
    {
        public class Color
        {
            public static Color White => new Color(255, 255, 255);
            public static Color Black => new Color(0, 0, 0);
            public static Color Red => new Color(255, 0, 0);
            public static Color Green => new Color(0, 255, 0);
            public static Color Blue => new Color(0, 0, 255);
            public static Color Fuchsia => new Color(255, 0, 255);
            public static Color Yellow => new Color(255, 255, 0);
            public static Color Cyan => new Color(0, 255, 255);

            public byte r, g, b;
            public Color(byte r, byte g, byte b)
            {
                this.r = r;
                this.g = g;
                this.b = b;
            }

            public Color(Color other)
            {
                this.r = other.r;
                this.g = other.g;
                this.b = other.b;
            }
        }
    }
}
