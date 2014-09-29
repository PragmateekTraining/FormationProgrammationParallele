using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncInstantiationSample
{
    class Internet
    {
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
            Console.WriteLine("[{0:hh:mm:ss.fff}] Loading internet...", DateTime.Now);
            Internet internet = await Internet.New();
            Console.WriteLine("[{0:hh:mm:ss.fff}] Internet loaded.", DateTime.Now);
        }

        static void Main(string[] args)
        {
            GetInternet();

            Console.ReadLine();
        }
    }
}
