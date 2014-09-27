using System;
using System.Collections.Immutable;
using System.Linq;

namespace ImmutableCollectionsSample
{
    class Program
    {
        static void Main(string[] args)
        {
            ImmutableList<string> strings = ImmutableList<string>.Empty;
            strings.Add("Hello");

            Console.WriteLine(strings.Count); // Still 0, original instance was not modified

            strings = strings.Add("Hello");

            ImmutableList<string> allStrings = strings.Add(" ").Add("World");

            Console.WriteLine("strings: [{0}] '{1}' ({2})", strings.Count, strings.Aggregate((agg, s) => agg + s), strings.GetHashCode()); // [1] 'Hello'

            Console.WriteLine("allStrings: [{0}] '{1}' ({2})", allStrings.Count, allStrings.Aggregate((agg, s) => agg + s), allStrings.GetHashCode()); // [3] 'Hello World'
        }
    }
}
