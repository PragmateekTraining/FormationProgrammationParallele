using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TaskDelaySample
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpClient httpClient = new HttpClient();

            Task<HttpResponseMessage> slowDownloadTask = httpClient.GetAsync("http://download.opensuse.org/distribution/11.0/iso/dvd/openSUSE-11.0-DVD-i386.iso?mirrorlist");
            Task<HttpResponseMessage> fastDownloadTask = httpClient.GetAsync("http://pragmateek.com");

            Task timeoutTask = Task.Delay(10000);

            Task firstToCompleteTask = Task.WhenAny(fastDownloadTask, timeoutTask).Result;

            if (firstToCompleteTask == fastDownloadTask)
            {
                Console.WriteLine("File downloaded.");
            }
            else
            {
                Console.WriteLine("Download is too long!");
            }
        }
    }
}
