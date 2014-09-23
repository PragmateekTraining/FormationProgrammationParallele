using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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

namespace TaskWhenAnyWPFSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string[] imagesURLs = 
        {
            "http://www.technoetgames.com/wp-content/uploads/2014/03/chuck-norris-01.jpg",
            "http://img2.wikia.nocookie.net/__cb20081118030612/tesfanon/images/5/5c/Chuck_Norris.jpg",
            "http://www.empireonline.com/images/uploaded/chuck-norris-uzis.jpg",
            "http://imperfectmessenger.us/uploads/3/2/7/9/3279770/4381492_orig.jpg?262",
            "http://static.comicvine.com/uploads/scale_medium/2/20720/3259531-chuck_norris_jedi_master.jpg",
            "http://fixwillpower.com/wp-content/uploads/2013/06/Chuck-Norris-uncertainty.jpg"
        };

        public MainWindow()
        {
            InitializeComponent();
        }

        private static readonly Random rand = new Random();

        private async Task<byte[]> DownloadImage(string url)
        {
            HttpClient httpClient = new HttpClient();

            await Task.Delay(rand.Next(10000));

            byte[] bytes = await httpClient.GetByteArrayAsync(url);

            return bytes;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            progressMarker2.IsActive = true;

            image.Source = null;

            IEnumerable<Task<byte[]>> tasks = imagesURLs.Select(DownloadImage);

            Task<Task<byte[]>> taskWaitingForFirstToComplete = Task.WhenAny(tasks.ToArray());

            Task<byte[]> firstToComplete = await taskWaitingForFirstToComplete;

            byte[] bytes = firstToComplete.Result;

            MemoryStream ms = new MemoryStream(bytes);

            BitmapImage imageSource = new BitmapImage();
            imageSource.BeginInit();
            imageSource.StreamSource = ms;
            imageSource.EndInit();

            image.Source = imageSource;

            progressMarker2.IsActive = false;
        }

        private async void allButton_Click(object sender, RoutedEventArgs e)
        {
            progressMarker1.IsActive = true;

            imagesList.ItemsSource = null;

            IEnumerable<Task<byte[]>> tasks = imagesURLs.Select(DownloadImage);

            Task<byte[][]> taskWaitingForAllToComplete = Task.WhenAll(tasks.ToArray());

            byte[][] allBytes = await taskWaitingForAllToComplete;

            IList<Image> images = new List<Image>(imagesURLs.Length);
            foreach (byte[] bytes in allBytes)
            {
                MemoryStream ms = new MemoryStream(bytes);

                BitmapImage imageSource = new BitmapImage();
                imageSource.BeginInit();
                imageSource.StreamSource = ms;
                imageSource.EndInit();

                Image image = new Image { Source = imageSource };

                images.Add(image);
            }

            imagesList.ItemsSource = images;

            progressMarker1.IsActive = false;
        }
    }
}
