using IP1.Imaging.ColorNS;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace IP1.Imaging.Filters.OpenCV
{
    public class GrayScale : OpenCVFilter
    {
        protected override RequiredColorType requiredInputColorType => RequiredColorType.ColorRGB;

        protected override RequiredColorType requiredOutputColorType => RequiredColorType.IColor;

        public override Mat Run(Mat mat)
        {
            return mat.CvtColor(ColorConversionCodes.RGB2GRAY);
        }
    }
}
