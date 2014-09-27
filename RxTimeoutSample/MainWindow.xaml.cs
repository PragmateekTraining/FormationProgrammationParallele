using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RxTimeoutSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Storyboard storyboard = (Storyboard)button.Resources["storyboard"];

            Observable.FromEventPattern<RoutedEventHandler, RoutedEventArgs>(handler => button.Click += handler, handler => button.Click -= handler)
                      .Timeout(TimeSpan.FromSeconds(5))
                      .Subscribe(e =>
                          {                              
                              storyboard.Stop();
                              storyboard.Begin();
                          },
                          onError: e => MessageBox.Show("Too Late!"));

            storyboard.Begin();
        }
    }
}
