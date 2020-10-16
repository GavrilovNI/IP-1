using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using IP1.Imaging.Filters;
using IP1.Imaging.ColorNS;

namespace IP1.Imaging
{
    public class Image<T> where T:IColor
    {
        private T[,] data;

        public int Height => data.GetLength(0);
        public int Width => data.GetLength(1);

        public T this[int indexY, int indexX]
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
            data = new T[sizeY, sizeX];
            for (int y = 0; y < data.GetLength(0); y++)
                for (int x = 0; x < data.GetLength(1); x++)
                    data[y, x] = (T)typeof(T).GetProperty("White").GetValue(null, null);

        }

        public static explicit operator Image<T>(System.Drawing.Image image)
        {
            Image<ColorRGB> result = new Image<ColorRGB>((uint)image.Width, (uint)image.Height);
            Bitmap bm = new Bitmap(image);
            for (int y = 0; y < result.Height; y++)
                for (int x = 0; x < result.Width; x++)
                {
                    System.Drawing.Color color = bm.GetPixel(x, y);
                    result[y, x] = new ColorRGB(color.R, color.G, color.B);
                }
            return new FilterChangeColorSpace().Run<T, ColorRGB>(result);
        }

        public static implicit operator System.Drawing.Bitmap(Image<T> image)
        {
            Bitmap bm = new Bitmap(image.Width, image.Height);
            for (int y = 0; y < image.Height; y++)
                for (int x = 0; x < image.Width; x++)
                {
                    ColorRGB color = FilterChangeColorSpace.RunColor<ColorRGB>(image[y, x]);
                    bm.SetPixel(x, y, System.Drawing.Color.FromArgb(color.r, color.g, color.b));
                }
            return bm;
        }

        public void Fill(T color)
        {
            for (int y = 0; y < data.GetLength(0); y++)
                for (int x = 0; x < data.GetLength(1); x++)
                    data[y, x] = (T)Activator.CreateInstance(typeof(T), color);
        }

        public IEnumerable<byte> GetBytesBGR24()
        {
            for (int y = 0; y < data.GetLength(0); y++)
            {
                for (int x = 0; x < data.GetLength(1); x++)
                {
                    ColorRGB color = FilterChangeColorSpace.RunColor<ColorRGB>(data[y, x]);

                    yield return color.b;
                    yield return color.g;
                    yield return color.r;
                }
            }
        }

        public void Save(String savePath)
        {
            Image<ColorRGB> image = null;
            if (typeof(T) == typeof(ColorRGB))
            {
                image = this as Image<ColorRGB>;
            }
            else if (typeof(T) == typeof(ColorHSV))
            {
                image = new Image<ColorRGB>((uint)this.Width, (uint)this.Height);
                for (int i = 0; i < Width; i++)
                    for (int j = 0; j < Height; j++)
                        image[i, j] = new ColorRGB(data[i, j] as ColorHSV);
                       

            }
            else
                throw new Exception("Wrong type");
            int maxValue = 255;
            int maxLineSize = 70;

            using (System.IO.StreamWriter streamWriter = System.IO.File.CreateText(savePath + ".ppm"))
            {
                streamWriter.WriteLine("P3");
                streamWriter.WriteLine(image.Width + " " + image.Height);
                streamWriter.WriteLine(maxValue);

                String line = "";

                for (int y = 0; y < image.Height; y++)
                {
                    for (int x = 0; x < image.Width; x++)
                    {
                        String pixel = image[y, x].r + " " + image[y, x].g + " " + image[y, x].b;
                        if (line.Length + pixel.Length + 1 > maxLineSize)
                        {
                            streamWriter.WriteLine(line);
                            line = "";
                        }
                        line += " " + pixel;
                    }
                }

                streamWriter.WriteLine(line);
                streamWriter.Close();
            }
        }

        public static Image<ColorRGB> Load(String savePath)
        {
            string magicNumber=null;
            int? maxValue = null;
            int? width = null;
            int? height = null;
            Image<ColorRGB> image = null;
            byte?[] pixel = new byte?[3] { null, null, null};
            int nextPixel = 0;

            using (System.IO.StreamReader streamReader = System.IO.File.OpenText(savePath + ".ppm"))
            {
                string line = streamReader.ReadLine();
                while(line!=null)
                {
                    line = line.Split(new char[] { '#' },2)[0];
                    string[] words = line.ToLower().Split(new char[] { ' ', '\n', '\t' });

                    for (int i = 0; i < words.Length; i++)
                    {
                        if (words[i] == String.Empty)
                            continue;
                            
                        if(magicNumber==null)
                        {
                            if(words[i]!="p3")
                            {
                                throw new Exception("Magic number should be 'p3'");
                            }
                            magicNumber = words[i];
                        }
                        else
                        {
                            int value;
                            if(!Int32.TryParse(words[i], out value))
                            {
                                throw new Exception("Expected number, but got '" + words[i] + "'");
                            }

                            
                            if(width==null)
                            {
                                width = value;
                            }
                            else if(height==null)
                            {
                                height = value;
                                image = new Image<ColorRGB>((uint)width, (uint)height);
                            }
                            else if (maxValue == null)
                            {
                                maxValue = value;
                            }
                            else if(nextPixel>=width*height)
                            {
                                throw new Exception("Got more than need.");
                            }
                            else
                            {
                                for (int j = 0; j < 3; j++)
                                {
                                    if(pixel[j]==null)
                                    {
                                        pixel[j] = (byte)(255.0 * value / maxValue);
                                        break;
                                    }
                                }
                                if(pixel[2]!=null)
                                {
                                    int posX = (int)(nextPixel % width);
                                    int posY = (int)(nextPixel / width);
                                    image[posY, posX] = new ColorRGB((byte)pixel[0], (byte)pixel[1], (byte)pixel[2]);
                                    pixel = new byte?[3] { null, null, null };
                                    nextPixel++;
                                }
                            }
                        }
                    }
                    line = streamReader.ReadLine();
                }

                if(image==null || nextPixel!=width*height)
                {
                    throw new Exception("Got less than need.");
                }

                streamReader.Close();
            }

            return image;
        }

    }
}
