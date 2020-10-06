using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IP1
{
    namespace Imaging
    {
        struct Color
        {
            private int _r;
            private int _g;
            private int _b;
            public int R { get { return _r; } set { _r = (int)Utils.Clamp(_r, 0f, 255f); } }
            public int G { get { return _g; } set { _g = (int)Utils.Clamp(_g, 0f, 255f); } }
            public int B { get { return _b; } set { _b = (int)Utils.Clamp(_b, 0f, 255f); } }



        }
    }
}
