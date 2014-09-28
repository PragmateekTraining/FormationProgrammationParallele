using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace CancellationTokenSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async Task NukeThemAll(CancellationToken cancellationToken)
        {
            int progress = 0;

            Random rand = new Random();

            while (progress < 100)
            {
                cancellationToken.ThrowIfCancellationRequested();

                await Task.Delay(rand.Next(50));

                progress += 1;

                progressBar.Value = progress;
            }

            if (progress >= 100)
            {
                progressBar.Visibility = Visibility.Collapsed;
                cancelButton.Visibility = Visibility.Collapsed;
                boomOutput.Visibility = Visibility.Visible;
            }
        }

        CancellationTokenSource source = null;

        private async void launchButton_Click(object sender, RoutedEventArgs e)
        {
            source = new CancellationTokenSource();

            launchButton.Visibility = Visibility.Collapsed;
            progressBar.Visibility = Visibility.Visible;
            cancelButton.Visibility = Visibility.Visible;

            try
            {
                await NukeThemAll(source.Token);
            }
            catch (OperationCanceledException)
            {
                launchButton.Visibility = Visibility.Visible;
                progressBar.Visibility = Visibility.Collapsed;
                cancelButton.Visibility = Visibility.Collapsed;
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            source.Cancel();
        }
    }
}
