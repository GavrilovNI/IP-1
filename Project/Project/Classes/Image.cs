using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace IP1
{
    namespace Imaging
    {
        public class Image
        {
            private Color[,] data;

            public int Height => data.GetLength(0);
            public int Width => data.GetLength(1);

            public Color this[int indexY, int indexX]
            {
                get { return data[indexY, indexX]; }
                set {
                    if (value == null)
                        throw new Exception("Color can't be null");
                    data[indexY, indexX] = value;
                    }
            }

            public Image(uint sizeX, uint sizeY)
            {
                data = new Color[sizeY, sizeX];
                for (int y = 0; y < data.GetLength(0); y++)
                    for (int x = 0; x < data.GetLength(1); x++)
                        data[y, x] = Color.White;

            }

            public void Fill(Color color)
            {
                for (int y = 0; y < data.GetLength(0); y++)
                    for (int x = 0; x < data.GetLength(1); x++)
                        data[y, x] = new Color(color);
            }

            public IEnumerable<byte> GetBytesBGR24()
            {
                for (int y = 0; y < data.GetLength(0); y++)
                {
                    for (int x = 0; x < data.GetLength(1); x++)
                    {
                        Color color = data[y, x];
                        yield return color.b;
                        yield return color.g;
                        yield return color.r;
                    }
                }
            }
            


        }
    }
}
