using IP1.Imaging.ColorNS;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace IP1.Imaging.Filters.OpenCV
{
    public class HSV2RGB : OpenCVFilter
    {
        protected override RequiredColorType requiredInputColorType => RequiredColorType.ColorHSV;

        protected override RequiredColorType requiredOutputColorType => RequiredColorType.ColorRGB;

        public override Mat Run(Mat mat)
        {
            return mat.CvtColor(ColorConversionCodes.HSV2RGB);
        }
    }
}
