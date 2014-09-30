using System;
using System.Collections.Generic;
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

namespace TaskContinuationWPFSample
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

        private int GetAnswer()
        {
            return 42;
        }

        private void OutputAnswer(object answer)
        {
            output.Text = answer.ToString();
        }

        private void ShowError(Exception exception)
        {
            MessageBox.Show(exception.ToString());
        }

        private void badButton_Click(object sender, RoutedEventArgs e)
        {
            Task<int>.Run((Func<int>)GetAnswer)
                     .ContinueWith(task => OutputAnswer(task.Result))
                     .ContinueWith(task => ShowError(task.Exception), TaskContinuationOptions.OnlyOnFaulted);
        }

        private void goodButton_Click(object sender, RoutedEventArgs e)
        {
            Task<int>.Run((Func<int>)GetAnswer)
                     .ContinueWith(task => OutputAnswer(task.Result), TaskScheduler.FromCurrentSynchronizationContext())
                     .ContinueWith(task => ShowError(task.Exception), TaskContinuationOptions.OnlyOnFaulted);
        }
    }
}
