using IP1.Imaging;
using IP1.Imaging.Filters;
using System;
using System.Collections.Generic;
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

namespace IP1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            InitializeComponent();
            RenderOptions.SetBitmapScalingMode(image, BitmapScalingMode.NearestNeighbor);

            byte[] arr = new byte[3] { 1, 2, 3 };

            Imaging.Image myImage = new Imaging.Image(3, 2);



            myImage[0, 0] = Imaging.Color.Red;
            myImage[0, 1] = Imaging.Color.Green;
            myImage[0, 2] = Imaging.Color.Blue;
            myImage[1, 0] = Imaging.Color.Yellow;
            myImage[1, 1] = Imaging.Color.Cyan;
            myImage[1, 2] = Imaging.Color.Fuchsia;


            myImage = Imaging.Image.Load("image");

            myImage = new FilterGrayScale(FilterGrayScale.GrayScaleType.Gimp).Run(myImage);



            image.Source = Utils.ImageToBitmapSource(myImage);
        }
    }
}
