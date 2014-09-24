using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetResultOnTheFlySample
{
    class Program
    {
        async static Task<string> GetStringAsync(string s, int delay)
        {
            await Task.Delay(delay);

            return s;
        }

        async static Task AwaitAndPrint(Task<string> t)
        {
            Console.WriteLine("Before [{0}]", DateTime.Now);

            string s = await t;

            Console.WriteLine("After [{0}] {1}", DateTime.Now, s);
        }

        static Task<string>[] GetTasks()
        {
            Task<string> t1 = GetStringAsync("World", 300);
            Task<string> t2 = GetStringAsync("!", 400);
            Task<string> t3 = GetStringAsync("Hello", 100);
            Task<string> t4 = GetStringAsync(" ", 200);

            Task<string>[] tasks = { t1, t2, t3, t4 };

            return tasks;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("=== In Line Execution ===");

            Task<string>[] tasks = GetTasks();

            string[] all = Task.WhenAll(tasks).Result;

            Console.WriteLine(string.Join("", all));

            Console.WriteLine("=== Standard Live Execution ===");

            tasks = GetTasks();

            BlockingCollection<string> results = new BlockingCollection<string>();

            Task.WaitAll(tasks.Select(async t =>
                {
                    results.Add(await t);
                    Console.WriteLine("[{0:hh:mm:ss.fff}] Current result: [{1}]", DateTime.Now, string.Join("", results));
                }).ToArray());

            Console.WriteLine("=== Pragmateek Live Execution ===");

            results = new BlockingCollection<string>();

            tasks = GetTasks();

            Task.WaitAll(tasks.Select(t => t.ContinueWith(p =>
                {
                    results.Add(p.Result);
                    Console.WriteLine("[{0:hh:mm:ss.fff}] Current result: [{1}]", DateTime.Now, string.Join("", results));
                })).ToArray());
        }
    }
}
