using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskFromResultSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Task<string> t1 = Task.FromResult("Hello");

            TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();
            tcs.SetResult("World");

            Task<string> t2 = tcs.Task;

            Console.WriteLine(t1.Result);
            Console.WriteLine(t2.Result);
        }
    }
}
