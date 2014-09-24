using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwaitExceptionHandlingSample
{
    class Program
    {
        async static Task KillChuckNorris()
        {
            await Task.Delay(1);

            throw new NotSupportedException("You can't do that, and should not try!");
        }

        async static Task DoSomethingStupid()
        {
            try
            {
                await KillChuckNorris();
            }
            catch (Exception e)
            {
                Console.WriteLine("Got an exception in DoSomethingStupid: {0}", e);
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Before trying.");

            DoSomethingStupid().Wait();

            Console.WriteLine("Before dying.");
        }
    }
}
