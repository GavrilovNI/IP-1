using IP1.Imaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IP1.Classes
{
    class Metrics
    {
        private double _CalcMSE(Image first, Image second)
        {
            double res = 0;

            byte[] firstArr = first.GetBytesBGR24().ToArray();
            byte[] secondArr = second.GetBytesBGR24().ToArray();
            if (firstArr.Length != secondArr.Length)
                throw new Exception("Images have different sizes");
            for (int i = 0; i < firstArr.Length; i++)
                res += Math.Abs(firstArr[i] - secondArr[i]);
            return res /= firstArr.Length;
            /*for (int i = 0; i < first.Height; ++i)
            {
                for (int j = 0; j < first.Width; ++j)
                {
                    res += Math.Abs(first[i, j].r - second[i, j].r);
                    res += Math.Abs(first[i, j].g - second[i, j].g);
                    res += Math.Abs(first[i, j].b - second[i, j].b);
                }
            }
            return res /= (3 * first.Height * first.Width);*/
        }
        public double CompareImage(Image first, Image second)
        {
            if (first.Height != second.Height || first.Width != second.Width)
                throw new Exception("Images have different sizes");
            return 10 * Math.Log10(255 * 255 / _CalcMSE(first, second));
        }
    }
}
