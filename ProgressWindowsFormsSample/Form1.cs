using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProgressWindowsFormsSample
{
    public partial class Form1 : Form
    {
        private readonly MainViewModel model = new MainViewModel();

        public Form1()
        {
            InitializeComponent();
        }

        private void downloadButton_Click(object sender, EventArgs e)
        {
            Progress<decimal> progress = new Progress<decimal>(p =>
            {
                progressBar.Value = (int)p;
            });

            model.DownloadInternet(progress);
        }
    }
}
