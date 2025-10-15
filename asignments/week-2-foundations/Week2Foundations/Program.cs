using System;
using System.Runtime.Serialization.Formatters;
using static Week2Foundations.Benchmarks;

namespace Week2Foundations
{
    class Program
    {
        static void Main(string[] args)
        {
            ArrayDemo();
            ListDemo();
            StackDemo();
            QueueDemo();
            DictionaryDemo();
            HashSetDemo();
            RunBenchmarks();
        }

        static void ArrayDemo()
        {
            // A: Array
            Console.WriteLine("----------Array Section------");
            int[] arr = new int[10];
            arr[0] = 1; arr[2] = 8; arr[9] = 156;

            Console.WriteLine(arr[2]);

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == 156) Console.WriteLine($"Value found at index: {i}");
            }
        }

        static void ListDemo()
        {
            // B: List
            Console.WriteLine("----------List Section------");
            var list = new List<int> { 1, 2, 3, 4, 5 };

            list.Insert(2, 99);

            list.Remove(2);

            Console.WriteLine(list.Count);
        }

        static void StackDemo()
        {
            // C: Stack
            Console.WriteLine("----------Stack Section------");
            var stack = new Stack<string>();
            var sites = new List<string> { "https://www.csharptutorial.net/csharp-collections/csharp-stack/", "https://bc.instructure.com", "https://music.youtube.com" };
            foreach (var site in sites)
            {
                stack.Push(site);
                Console.WriteLine($"Push the site '{site}' onto the stack.");
            }
            while (stack.Count > 0)
            {
                var site = stack.Pop();
                Console.WriteLine($"Pop the site '{site}' out of the stack.");
            }
        }

        static void QueueDemo()
        {
            // D: Queue
            Console.WriteLine("----------Queue Section------");
            var queue = new Queue<string>();
            var printJobs = new List<string> { "Resume.pdf", "Repor.Docx", "Jellybeans.txt", "ReportCard.PDF" };
            foreach (var job in printJobs)
            {
                queue.Enqueue(job);
                Console.WriteLine($"Enqueued the print job '{job}' into the queue.");
            }
            while (queue.Count > 0)
            {
                Console.WriteLine($"Next job is: {queue.Peek()}");
                var job = queue.Dequeue();
                Console.WriteLine($"Dequeued the print job '{job}' from the queue.");
            }
        }

        static void DictionaryDemo()
        {
            // E: Dictionary
            Console.WriteLine("----------Dictionary Section------");
            var SKUQuantities = new Dictionary<string, int>();
            SKUQuantities.Add("ELX-4275-B", 120);
            SKUQuantities.Add("CPH-3565-F", 58);
            SKUQuantities.Add("LMN-9734-D", 351);
            foreach (var kvp in SKUQuantities)
            {
                Console.WriteLine($"SKU: {kvp.Key}, Quantity: {kvp.Value}");
            }
            SKUQuantities["CPH-3565-F"] = 60; // Update quantity
            foreach (var kvp in SKUQuantities)
            {
                Console.WriteLine($"SKU: {kvp.Key}, Quantity: {kvp.Value}");
            }
            int quantity;
            Console.WriteLine(SKUQuantities.TryGetValue("CPH-3565-z", out quantity));
        }

        static void HashSetDemo()
        {
            // F: HashSet
            Console.WriteLine("----------HashSet Section------");
            var set = new HashSet<int> { 1, 2, 2, 4, 5 };
            foreach (var item in set)
            {
                Console.WriteLine(item);
            }
            set.Add(5);
            foreach (var item in set)
            {
                Console.WriteLine(item);
            }
            set.UnionWith(new List<int> { 3, 4, 5 });
            Console.WriteLine($"Count: { set.Count()}");
        }
    }


}
