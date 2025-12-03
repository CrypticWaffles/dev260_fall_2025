using System;
using System.Linq;

namespace SortingAlgorithmsDemo
{
    /// <summary>
    /// Bubble Sort demonstration with step-by-step visualization
    /// Educational Focus: Understanding basic sorting through adjacent element comparison
    /// </summary>
    public static class BubbleSortDemo
    {
        /// <summary>
        /// Demonstrate Bubble Sort with detailed explanation
        /// </summary>
        public static void Demonstrate(int[] originalArray)
        {
            Console.WriteLine("1. BUBBLE SORT");
            Console.WriteLine("--------------");
            Console.WriteLine("Time Complexity: O(n²) | Space Complexity: O(1)");
            Console.WriteLine("Description: Repeatedly swaps adjacent elements if they're in wrong order\n");
            int[] bubbleArray = (int[])originalArray.Clone();
            BubbleSortWithSteps(bubbleArray);
        }

        /// <summary>
        /// Bubble Sort with step-by-step visualization
        /// Time Complexity: O(n²) worst/average case, O(n) best case (with optimization)
        /// Space Complexity: O(1) - sorts in place
        /// 
        /// Key Features:
        ///  Pass Reduction: After each pass, the range decreases because the largest element is in its final position
        ///  Early Termination: The algorithm can detect when sorting is complete
        ///  Stability: Bubble sort is stable (equal elements maintain relative order)
        ///  In-Place: No extra memory needed beyond a few variables
        /// </summary>
        static void BubbleSortWithSteps(int[] arr)
        {
            int n = arr.Length;
            int comparisons = 0, swaps = 0;
            
            // Outer loop: Controls the number of passes through the array
            // We need at most (n-1) passes to sort n elements
            for (int i = 0; i < n - 1; i++)
            {
                Console.WriteLine($"\nPass {i + 1}:");
                bool swapped = false; // Flag to track if any swaps occurred in this pass
                
                // Inner loop: Compare adjacent elements and swap if needed
                // Range decreases each pass because largest elements "bubble up" to the end
                // After pass i, the last i elements are already in their correct positions
                for (int j = 0; j < n - i - 1; j++)
                {
                    comparisons++;
                    Console.Write($"  Compare arr[{j}]={arr[j]} and arr[{j + 1}]={arr[j + 1]} → ");
                    
                    // If current element is greater than next element, they're out of order
                    if (arr[j] > arr[j + 1])
                    {
                        // Swap elements using C# tuple deconstruction (equivalent to temp variable swap)
                        (arr[j], arr[j + 1]) = (arr[j + 1], arr[j]);
                        swaps++;
                        swapped = true; // Mark that a swap occurred
                        Console.WriteLine($"Swap! → [{string.Join(", ", arr)}]");
                    }
                    else
                    {
                        // Elements are already in correct order, no swap needed
                        Console.WriteLine($"No swap → [{string.Join(", ", arr)}]");
                    }
                }
                
                // Optimization: If no swaps were made in this complete pass,
                // the array is already sorted and we can exit early
                if (!swapped)
                {
                    Console.WriteLine("  No swaps made - array is sorted!");
                    break; // This optimization improves best-case performance to O(n)
                }
            }
            
            Console.WriteLine($"\nFinal result: [{string.Join(", ", arr)}]");
            Console.WriteLine($"Comparisons: {comparisons}, Swaps: {swaps}");
        }

        /// <summary>
        /// Optimized Bubble Sort for performance testing (without step-by-step output)
        /// </summary>
        public static void BubbleSort(int[] arr)
        {
            int n = arr.Length;
            for (int i = 0; i < n - 1; i++)
            {
                bool swapped = false;
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        (arr[j], arr[j + 1]) = (arr[j + 1], arr[j]);
                        swapped = true;
                    }
                }
                if (!swapped) break; // Optimization: stop if no swaps made
            }
        }
    }
}