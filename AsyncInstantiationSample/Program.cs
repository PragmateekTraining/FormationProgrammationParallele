using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncInstantiationSample
{
    class Internet
    {
        public Task Initialization { get; private set; }

        public Internet()
        {
            Initialization = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            await Task.Delay(1000);
        }

        public static async Task<Internet> New()
        {
            await Task.Delay(1000);

            return new Internet();
        }
    }

    class Program
    {
        private static async Task GetInternet()
        {
            Console.WriteLine("[{0:hh:mm:ss.fff}] Loading internet with factory...", DateTime.Now);
            Internet internet = await Internet.New();
            Console.WriteLine("[{0:hh:mm:ss.fff}] Internet loaded.", DateTime.Now);

            Console.WriteLine("[{0:hh:mm:ss.fff}] Loading internet with async initialization pattern...", DateTime.Now);
            internet = new Internet();
            await internet.Initialization;
            Console.WriteLine("[{0:hh:mm:ss.fff}] Internet loaded.", DateTime.Now);
        }

        static void Main(string[] args)
        {
            GetInternet();

            Console.ReadLine();
        }
    }
}
