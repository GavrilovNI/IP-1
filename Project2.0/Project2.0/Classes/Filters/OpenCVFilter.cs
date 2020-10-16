using IP1.Imaging;
using IP1.Imaging.ColorNS;
using IP1.Imaging.Filters;
using Microsoft.VisualBasic.CompilerServices;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.Printing;
using System.Text;

namespace IP1.Imaging.Filters
{

    public abstract class OpenCVFilter : Filter
    {
        protected class RequiredColorType
        {
            public static readonly RequiredColorType ColorRGB = new RequiredColorType(typeof(ColorRGB));
            public static readonly RequiredColorType ColorHSV = new RequiredColorType(typeof(ColorHSV));
            public static readonly RequiredColorType IColor = new RequiredColorType(typeof(IColor));
            private Type type;
            private RequiredColorType(Type t)
            {
                type = t;
            }
            public static bool operator ==(RequiredColorType r, Type t)
            {
                return r.type.IsAssignableFrom(t) || r.type == t;
            }
            public static bool operator !=(RequiredColorType r, Type t)
            {
                return !(r.type == t);
            }
        }
        abstract protected RequiredColorType requiredInputColorType { get; }
        abstract protected RequiredColorType requiredOutputColorType { get; }

        public override Image<T> Run<T, Y>(Image<Y> image)
        {
            if (requiredInputColorType != typeof(Y))
                throw new Exception("Wrong input image type for this filter");
            if (requiredOutputColorType != typeof(T))
                throw new Exception("Wrong output image type for this filter");

            return (Image<T>)Run(((Bitmap)image).ToMat()).ToBitmap();
            
        }
        public abstract Mat Run(Mat mat);

    }
}
