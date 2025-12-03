using System;
using System.Linq;

namespace SortingAlgorithmsDemo
{
    /// <summary>
    /// Selection Sort demonstration with step-by-step visualization
    /// Educational Focus: Understanding minimum selection and invariant maintenance
    /// </summary>
    public static class SelectionSortDemo
    {
        /// <summary>
        /// Demonstrate Selection Sort with detailed explanation
        /// </summary>
        public static void Demonstrate(int[] originalArray)
        {
            Console.WriteLine("2. SELECTION SORT");
            Console.WriteLine("-----------------");
            Console.WriteLine("Time Complexity: O(n²) | Space Complexity: O(1)");
            Console.WriteLine("Description: Finds minimum element and places it at the beginning\n");
            int[] selectionArray = (int[])originalArray.Clone();
            SelectionSortWithSteps(selectionArray);
        }

        /// <summary>
        /// Selection Sort with step-by-step visualization
        /// Time Complexity: O(n²) all cases - always performs the same number of comparisons
        /// Space Complexity: O(1) - sorts in place with minimal swaps
        /// 
        /// Key Features:
        ///  Invariant Maintenance: After pass i, the first i+1 elements are sorted
        ///  Minimal Swaps: At most n-1 swaps (one per pass)
        ///  Not Stable: Equal elements may change relative order (though your example doesn't show this)
        ///  In-Place: No extra memory needed beyond a few variables
        /// </summary>
        static void SelectionSortWithSteps(int[] arr)
        {
            int n = arr.Length;
            int comparisons = 0, swaps = 0;
            
            // Outer loop: Build the sorted portion one element at a time
            // We need (n-1) passes since the last element will be in correct position automatically
            for (int i = 0; i < n - 1; i++)
            {
                Console.WriteLine($"\nPass {i + 1}: Finding minimum in unsorted portion");
                int minIndex = i; // Assume current position has the minimum value
                
                // Show the current state: sorted vs unsorted portions
                Console.Write($"  Sorted portion: [{string.Join(", ", arr.Take(i))}] | ");
                Console.WriteLine($"Unsorted: [{string.Join(", ", arr.Skip(i))}]");
                
                // Inner loop: Find the minimum element in the remaining unsorted portion
                // Start from (i+1) because position i is our current candidate for minimum
                for (int j = i + 1; j < n; j++)
                {
                    comparisons++;
                    Console.Write($"  Compare arr[{j}]={arr[j]} with current min arr[{minIndex}]={arr[minIndex]} → ");
                    
                    // If we find a smaller element, update our minimum index
                    if (arr[j] < arr[minIndex])
                    {
                        minIndex = j; // Remember the position of the new minimum
                        Console.WriteLine($"New minimum found at index {j}");
                    }
                    else
                    {
                        // Current minimum is still the smallest we've seen
                        Console.WriteLine($"Keep current minimum");
                    }
                }
                
                // After scanning the entire unsorted portion, place the minimum at position i
                if (minIndex != i)
                {
                    // Swap the minimum element to its correct sorted position
                    Console.WriteLine($"  Swapping arr[{i}]={arr[i]} with arr[{minIndex}]={arr[minIndex]}");
                    (arr[i], arr[minIndex]) = (arr[minIndex], arr[i]);
                    swaps++;
                }
                else
                {
                    // The element at position i was already the minimum - no swap needed
                    Console.WriteLine($"  Element at position {i} is already the minimum");
                }
                
                Console.WriteLine($"  Result: [{string.Join(", ", arr)}]");
                // After this pass, positions 0 through i are in their final sorted positions
            }
            
            Console.WriteLine($"\nFinal result: [{string.Join(", ", arr)}]");
            Console.WriteLine($"Comparisons: {comparisons}, Swaps: {swaps}");
        }

        /// <summary>
        /// Optimized Selection Sort for performance testing (without step-by-step output)
        /// </summary>
        public static void SelectionSort(int[] arr)
        {
            int n = arr.Length;
            for (int i = 0; i < n - 1; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (arr[j] < arr[minIndex])
                        minIndex = j;
                }
                if (minIndex != i)
                    (arr[i], arr[minIndex]) = (arr[minIndex], arr[i]);
            }
        }
    }
}