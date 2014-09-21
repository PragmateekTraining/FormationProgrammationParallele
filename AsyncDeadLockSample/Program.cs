using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyncDeadLockSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Button OKButton = new Button { Text = "OK!", ForeColor = Color.Green };
            OKButton.Click += OKButton_Click;

            Button KOButton = new Button { Text = "KO!", ForeColor = Color.Red };
            KOButton.Click += KOButton_Click;

            Form form = new Form();

            FlowLayoutPanel panel = new FlowLayoutPanel();
            panel.Controls.Add(OKButton);
            panel.Controls.Add(KOButton);

            form.Controls.Add(panel);

            Application.Run(form);
        }

        static async Task WaitOneSecond()
        {
            await Task.Delay(1);
        }

        static async void OKButton_Click(object sender, EventArgs e)
        {
            await WaitOneSecond();
        }

        static void KOButton_Click(object sender, EventArgs e)
        {
            WaitOneSecond().Wait();
        }
    }
}
