using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace RxThrottlingSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            input.TextChanged += (_1, _2) =>
                {
                    output1.Text += input.Text + "\n";
                    scrollViewer1.ScrollToBottom();
                };

            IObservable<EventPattern<TextChangedEventArgs>> textChangedEventsSource = Observable.FromEventPattern<TextChangedEventHandler, TextChangedEventArgs>(handler => input.TextChanged += handler, handler => input.TextChanged -= handler);
            
            textChangedEventsSource.Sample(TimeSpan.FromSeconds(1))
                                   .ObserveOn(SynchronizationContext.Current)
                                   .Subscribe(_ =>
                                    {
                                        output2.Text += input.Text + "\n";
                                        scrollViewer2.ScrollToBottom();
                                    });

            textChangedEventsSource.Throttle(TimeSpan.FromSeconds(1))
                                   .ObserveOn(SynchronizationContext.Current)
                                   .Subscribe(_ =>
                                   {
                                       output3.Text += input.Text + "\n";
                                       scrollViewer3.ScrollToBottom();
                                   });
        }
    }
}
