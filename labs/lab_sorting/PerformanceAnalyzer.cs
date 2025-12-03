using System;
using System.Diagnostics;
using System.Linq;

namespace SortingAlgorithmsDemo
{
    /// <summary>
    /// Performance comparison and benchmarking utilities for sorting algorithms
    /// Educational Focus: Empirical algorithm analysis and Big O demonstration
    /// </summary>
    public static class PerformanceAnalyzer
    {
        // For performance measurement
        private static readonly Stopwatch stopwatch = new Stopwatch();

        /// <summary>
        /// Performance comparison with different array sizes
        /// Educational Focus: Demonstrates empirical algorithm analysis and Big O notation in practice
        /// 
        /// Key Learning Objectives:
        ///  Empirical vs Theoretical Analysis: See how theoretical complexity translates to real performance
        ///  Scalability Testing: Observe how algorithms behave as input size increases
        ///  Algorithm Selection: Learn when to choose one algorithm over another based on data size
        ///  Performance Measurement: Understanding proper benchmarking techniques
        ///  Optimization Impact: Compare optimized vs naive implementations
        /// </summary>
        public static void PerformanceComparison()
        {
            Console.WriteLine("PERFORMANCE COMPARISON");
            Console.WriteLine("======================");
            
            // Test with progressively larger datasets to observe scaling behavior
            // Small (100): All algorithms perform reasonably well, differences barely noticeable
            // Medium (1000): O(n²) algorithms start showing significant slowdown
            // Large (5000): O(n²) vs O(n log n) difference becomes dramatic
            int[] sizes = { 100, 1000, 5000 };
            
            foreach (int size in sizes)
            {
                Console.WriteLine($"\nArray Size: {size} elements");
                Console.WriteLine(new string('-', 40));
                
                // CONTROLLED EXPERIMENT SETUP:
                // Use fixed seed (42) to ensure reproducible results across test runs
                // This eliminates randomness as a variable in our performance comparison
                Random rand = new Random(42); // Fixed seed for consistent results
                int[] originalArray = new int[size];
                
                // Generate random integers from 1 to 1000
                // This creates a "typical" unsorted dataset without extreme cases
                // Range 1-1000 ensures reasonable comparison operations
                for (int i = 0; i < size; i++)
                {
                    originalArray[i] = rand.Next(1, 1000);
                }
                
                // PERFORMANCE TESTING METHODOLOGY:
                // Each algorithm gets an identical copy of the same data
                // This ensures fair comparison - same input, measure only the algorithm differences
                // We test from slowest to fastest to build suspense and show dramatic differences!
                
                // O(n²) Algorithms - Expected to be slowest for large datasets
                TestSortingAlgorithm("Bubble Sort", arr => BubbleSortDemo.BubbleSort(arr), originalArray);
                TestSortingAlgorithm("Selection Sort", arr => SelectionSortDemo.SelectionSort(arr), originalArray);
                TestSortingAlgorithm("Insertion Sort", arr => InsertionSortDemo.InsertionSort(arr), originalArray);
                
                // O(n log n) Algorithm - Expected to be much faster for large datasets
                TestSortingAlgorithm("Quick Sort", arr => QuickSortDemo.QuickSort(arr, 0, arr.Length - 1), originalArray);
                
                // Built-in Sort - Highly optimized hybrid algorithm (typically Introsort)
                // Usually the fastest due to years of optimization and platform-specific tuning
                TestSortingAlgorithm("Built-in Sort", arr => Array.Sort(arr), originalArray);
                
                // EXPECTED RESULTS ANALYSIS:
                // Size 100: All algorithms should complete quickly (< 10ms typically)
                // Size 1000: O(n²) algorithms ~100x slower than size 100, O(n log n) only ~10x slower
                // Size 5000: O(n²) algorithms ~2500x slower than size 100, O(n log n) only ~50x slower
                // This demonstrates the practical impact of Big O notation!
            }
            
            // EDUCATIONAL TAKEAWAYS:
            // 1. Theoretical complexity (Big O) directly impacts real-world performance
            // 2. Algorithm choice matters more as data size increases
            // 3. Built-in library functions are usually highly optimized
            // 4. "Fast enough" depends on your specific use case and data size
            // 5. Performance testing requires controlled, reproducible experiments
        }

        /// <summary>
        /// Test a sorting algorithm and measure performance
        /// Educational Focus: Proper benchmarking methodology and result verification
        /// 
        /// Key Concepts:
        ///  Data Isolation: Each test gets a fresh copy to prevent interference
        ///  Timing Precision: Using Stopwatch for accurate millisecond measurement
        ///  Result Verification: Always validate that sorting actually worked correctly
        ///  Fair Comparison: Same input data, same environment, measure only algorithm differences
        /// </summary>
        static void TestSortingAlgorithm(string algorithmName, Action<int[]> sortMethod, int[] originalArray)
        {
            // STEP 1: DATA ISOLATION
            // Create a fresh copy for this test to ensure no algorithm affects another's input
            // Each algorithm must sort exactly the same data for fair comparison
            int[] testArray = (int[])originalArray.Clone();
            
            // STEP 2: PRECISE TIMING MEASUREMENT
            // Stopwatch provides high-resolution timing (much more accurate than DateTime)
            // Start timing just before the algorithm runs, stop immediately after
            stopwatch.Restart(); // Reset and start the timer
            sortMethod(testArray); // Execute the sorting algorithm
            stopwatch.Stop(); // Stop timing immediately
            
            // STEP 3: CORRECTNESS VERIFICATION
            // Performance means nothing if the algorithm doesn't work correctly!
            // Compare our result with the known correct result (LINQ's OrderBy)
            // SequenceEqual checks if arrays have same elements in same order
            bool isSorted = testArray.SequenceEqual(testArray.OrderBy(x => x));
            
            // STEP 4: RESULTS PRESENTATION
            // Display timing with consistent formatting for easy comparison
            // Include checkmark/X to immediately show correctness
            // Left-align algorithm names for easy scanning, right-align times for comparison
            Console.WriteLine($"{algorithmName,-15}: {stopwatch.ElapsedMilliseconds,6} ms {(isSorted ? "✓" : "✗")}");
            
            // PERFORMANCE ANALYSIS NOTES:
            // - Times will vary between runs due to system load, but relative performance should be consistent
            // - Millisecond precision is sufficient for educational purposes
            // - For production benchmarking, you'd want multiple runs and statistical analysis
            // - The checkmark (✓) or X (✗) quickly shows if the algorithm worked correctly
        }
    }
}