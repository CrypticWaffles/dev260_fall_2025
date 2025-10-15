using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week2Foundations
{
    public class Benchmarks
    {
        // N values to test
        private static int[] NValues = { 1000, 10000, 100000};

        // Method to run benchmarks
        public static void RunBenchmarks()
        {
            Console.WriteLine("----------Part 3: Benchmarks------");
            // Benchmarking Contains operation for List, HashSet, and Dictionary
            foreach (var n in NValues)
            {
                // Initialize collections
                List<int> list = new List<int>();
                HashSet<int> hashSet = new HashSet<int>();
                Dictionary<int, bool> dictionary = new Dictionary<int, bool>();

                // Populate collections
                for (int i = 0; i < n; i++)
                {
                    list.Add(i);
                    hashSet.Add(i);
                    dictionary.Add(i, true);
                }

                // Elements to search for
                int target = n - 1;
                int missingElement = -1;

                // Looking for existing element
                double listTime = TimeOperation(() => list.Contains(target));
                double hashSetTime = TimeOperation(() => hashSet.Contains(target));
                double dictionaryTime = TimeOperation(() => dictionary.ContainsKey(target));

                // Looking for missing element
                double listMissingTime = TimeOperation(() => list.Contains(missingElement));
                double hashSetMissingTime = TimeOperation(() => hashSet.Contains(missingElement));
                double dictionaryMissingTime = TimeOperation(() => dictionary.ContainsKey(missingElement));

                // Output results
                Console.WriteLine($"N={n}");
                Console.WriteLine($"List: Found={listTime}ms, Missing={listMissingTime}ms");
                Console.WriteLine($"HashSet: Found={hashSetTime}ms, Missing={hashSetMissingTime}ms");
                Console.WriteLine($"Dictionary: Found={dictionaryTime}ms, Missing={dictionaryMissingTime}ms");
            }
        }

        Stopwatch sw = new Stopwatch();

        // Method to time an operation and return the median time over multiple iterations
        private static double TimeOperation(Action action, int iterations = 5)
        {
            // Warm up
            action();

            // Measure
            List<double> times = new List<double>();
            Stopwatch sw = new Stopwatch();

            // Run the action multiple times and record the time taken
            for (int i = 0; i < iterations; i++)
            {
                sw.Restart();
                action();
                sw.Stop();
                times.Add(sw.Elapsed.TotalMilliseconds);
            }

            // Return the median time
            times.Sort();
            return times[iterations / 2];
        }
    }
}