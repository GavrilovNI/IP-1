using IP1.Imaging;
using IP1.Imaging.Filters;
using IP1.Imaging.Filters.OpenCV;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using IP1.Imaging.ColorNS;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace IP1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        public static Imaging.Image<ColorRGB> myImage;
        public static Imaging.Image<ColorRGB> CustomImage;
        public static System.Drawing.Image openCVImage;
        public static double TimeOpenCvWork;

        public MainWindow()
        {

            InitializeComponent();
            RenderOptions.SetBitmapScalingMode(CustomIm, BitmapScalingMode.NearestNeighbor);
            RenderOptions.SetBitmapScalingMode(OpenCVIm, BitmapScalingMode.NearestNeighbor);
            RenderOptions.SetBitmapScalingMode(OriginalIm, BitmapScalingMode.NearestNeighbor);

            /*Imaging.Image myImage = Imaging.Image.Load("image");

            //myImage = new FilterGrayScale(FilterGrayScale.GrayScaleType.Gimp).Run(myImage);

            ConvertOpenCV opcv = new ConvertOpenCV();
            System.Drawing.Image SecondIm = opcv.ConvertToGray(Imaging.Image.Load("image"));


            image2.Source = Utils.ImageToBitmapSource(myImage);
            
            var memory = new MemoryStream();
                SecondIm.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
            image.Source = bitmapImage;

            CustomIm.Source = Utils.ImageToBitmapSource(myImage);
            System.Drawing.Image three = opcv.RGB2HSV(Imaging.Image.Load("image"));

            var memory = new MemoryStream();
            three.Save(memory, ImageFormat.Png);
            memory.Position = 0;

            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = memory;
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.EndInit();
            OpenCVIm.Source = bitmapImage;*/
        }

        private void ButtonLoadImage_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.DefaultExt = ".png";
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg |PPM Files (*.ppm)|*.ppm";
            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                if (dlg.FileName.Split('.').Last() == "ppm")
                    myImage = Imaging.Image<ColorRGB>.Load(dlg.FileName.Substring(0, dlg.FileName.Length - 4));
                else
                {
                    myImage = (Image<ColorRGB>)new Bitmap(dlg.FileName);
                }

                OriginalIm.Source = Utils.ImageToBitmapSource(myImage);
            }
        }

        public void Clear() 
        {
            TimeCustomlabel.Content = "";
            TimeOpenCVlabel.Content = "";
            Qualitylabel.Content = "";
        }

        public double CheckTime(Action action)
        {
            DateTime StartTime = DateTime.Now;
            action.Invoke();
            return DateTime.Now.Subtract(StartTime).TotalSeconds;
        }

        public void RunConvertToGrayScale()
        {
            Clear(); //Clear labels
            Image<ColorRGB> myImageRGB = null;
            Image<ColorRGB> openCVImageRGB;
            Mat mat = ((Bitmap)myImage).ToMat();
            double seconds = CheckTime(() => { myImageRGB = new FilterGrayScale(FilterGrayScale.GrayScaleType.Gimp).Run<ColorRGB, ColorRGB>(myImage); });
            double seconds2 = CheckTime(() => { mat = new GrayScale().Run(mat); });
            openCVImageRGB = (Image<ColorRGB>)mat.ToBitmap();

            CustomIm.Source = Utils.ImageToBitmapSource(myImageRGB);
            OpenCVIm.Source = Utils.ImageToBitmapSource(openCVImageRGB);

            Metrics mt = new Metrics();
            Qualitylabel.Content = mt.CompareImage(openCVImageRGB, myImageRGB);
        }

        public void RunConvertRGBToHSV()
        {
            Clear(); //Clear labels
            Image<ColorHSV> myImageHSV = null;
            Image<ColorHSV> openCVImageHSV;
            Mat mat = ((Bitmap)myImage).ToMat();
            double seconds = CheckTime(()=> { myImageHSV = new FilterChangeColorSpace().Run<ColorHSV, ColorRGB>(myImage); });
            double seconds2 = CheckTime(()=> { mat = new RGB2HSV().Run(mat); });
            openCVImageHSV = (Image<ColorHSV>)mat.ToBitmap();
            
            CustomIm.Source = Utils.ImageToBitmapSource(myImageHSV);            
            OpenCVIm.Source = Utils.ImageToBitmapSource(openCVImageHSV);

            Metrics mt = new Metrics();
            Qualitylabel.Content = mt.CompareImage(openCVImageHSV, myImageHSV);
        }

        public void RunConvertHSVToRGB()
        {
            Clear(); //Clear labels
            Image<ColorHSV> myImageHSV = new FilterChangeColorSpace().Run<ColorHSV, ColorRGB>(myImage);
            Image<ColorRGB> myImageRGB = null;
            Image<ColorRGB> openCVImageRGB;
            Mat mat = ((Bitmap)myImage).ToMat();
            double seconds = CheckTime(() => { myImageRGB = new FilterChangeColorSpace().Run<ColorRGB, ColorHSV>(myImageHSV); });
            double seconds2 = CheckTime(() => { mat = new HSV2RGB().Run(mat); });
            openCVImageRGB = (Image<ColorRGB>)mat.ToBitmap();

            CustomIm.Source = Utils.ImageToBitmapSource(myImageRGB);
            OpenCVIm.Source = Utils.ImageToBitmapSource(openCVImageRGB);

            Metrics mt = new Metrics();
            Qualitylabel.Content = mt.CompareImage(openCVImageRGB, myImageRGB);
        }

        private void Convert_Click(object sender, RoutedEventArgs e)
        {
            if (radioButtonGrayScale.IsChecked == true)
            {
                RunConvertToGrayScale();
            }
            else if (radioButtonRGB2HSV.IsChecked == true)
            {
                RunConvertRGBToHSV();
            }
            else if (radioButtonHSV2RGB.IsChecked == true)
            {
                RunConvertHSVToRGB();
            }
        }
    }
}
