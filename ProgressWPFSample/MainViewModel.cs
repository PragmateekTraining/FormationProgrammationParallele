using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgressWPFSample
{
    public class MainViewModel
    {
        private readonly Random rand = new Random();

        public async void DownloadInternet(IProgress<decimal> progress)
        {
            for (decimal p = 0; p <= 100; p += rand.Next(5))
            {
                progress.Report(p);

                await Task.Delay(250);
            }

            progress.Report(100);
        }
    }
}
