using System;
using System.Linq;

namespace SortingAlgorithmsDemo
{
    /// <summary>
    /// Insertion Sort demonstration with step-by-step visualization
    /// Educational Focus: Understanding incremental sorting and element insertion
    /// </summary>
    public static class InsertionSortDemo
    {
        /// <summary>
        /// Demonstrate Insertion Sort with detailed explanation
        /// </summary>
        public static void Demonstrate(int[] originalArray)
        {
            Console.WriteLine("3. INSERTION SORT");
            Console.WriteLine("-----------------");
            Console.WriteLine("Time Complexity: O(n²) | Space Complexity: O(1)");
            Console.WriteLine("Description: Builds sorted array one element at a time\n");
            int[] insertionArray = (int[])originalArray.Clone();
            InsertionSortWithSteps(insertionArray);
        }

        /// <summary>
        /// Insertion Sort with step-by-step visualization
        /// Time Complexity: O(n²) worst/average case, O(n) best case (already sorted)
        /// Space Complexity: O(1) - sorts in place with minimal memory usage
        /// 
        /// Key Features:
        ///  Card-Playing Analogy: Like sorting playing cards in your hand - pick up one card at a time and insert it into correct position
        ///  Adaptive Algorithm: Performs better on partially sorted data (best case O(n) when already sorted)
        ///  Stable Sorting: Equal elements maintain their relative order
        ///  Online Algorithm: Can sort data as it arrives, doesn't need entire dataset upfront
        ///  Efficient for Small Arrays: Often used as final step in hybrid sorting algorithms
        /// </summary>
        static void InsertionSortWithSteps(int[] arr)
        {
            int n = arr.Length;
            int comparisons = 0, shifts = 0;
            
            Console.WriteLine($"Starting: [{string.Join(", ", arr)}]");
            Console.WriteLine("First element is considered sorted");
            
            // Outer loop: Process each element starting from the second element (index 1)
            // The first element (index 0) is considered already "sorted" by itself
            for (int i = 1; i < n; i++)
            {
                // Store the current element we want to insert into the sorted portion
                int key = arr[i];
                int j = i - 1; // Start comparing from the rightmost element of sorted portion
                
                Console.WriteLine($"\nPass {i}: Inserting {key} into sorted portion");
                Console.Write($"  Sorted: [{string.Join(", ", arr.Take(i))}] | ");
                Console.WriteLine($"Current element: {key} | Remaining: [{string.Join(", ", arr.Skip(i + 1))}]");
                
                // Inner loop: Shift elements in sorted portion to make room for the key
                // Move backwards through the sorted portion, shifting larger elements to the right
                // This creates the "insertion point" where key belongs
                while (j >= 0 && arr[j] > key)
                {
                    comparisons++; // Count each comparison for complexity analysis
                    Console.WriteLine($"  {arr[j]} > {key}, so shift {arr[j]} to the right");
                    
                    // Shift the larger element one position to the right
                    // This overwrites the position where key was, but we saved key in a variable
                    arr[j + 1] = arr[j];
                    shifts++; // Count shifts (similar to swaps in other algorithms)
                    j--; // Move to the next element to the left in sorted portion
                }
                
                // Handle the case where we compared but didn't shift (key >= arr[j])
                // This happens when we find the correct insertion point
                if (j >= 0)
                {
                    comparisons++; // Count the final comparison that broke the while loop
                }
                
                // Insert the key at its correct position (j + 1)
                // At this point, all elements from positions (j+2) to i have been shifted right
                // Position (j+1) is now the correct place for our key
                arr[j + 1] = key;
                Console.WriteLine($"  Insert {key} at position {j + 1}");
                Console.WriteLine($"  Result: [{string.Join(", ", arr)}]");
                
                // Invariant maintained: Elements from index 0 to i are now sorted
                // Next iteration will take the element at position (i+1) and insert it correctly
            }
            
            Console.WriteLine($"\nFinal result: [{string.Join(", ", arr)}]");
            Console.WriteLine($"Comparisons: {comparisons}, Shifts: {shifts}");
            
            // Educational note: Insertion sort is particularly efficient when:
            // 1. Array is small (< 50 elements)
            // 2. Array is already mostly sorted
            // 3. You need a stable sort (maintains relative order of equal elements)
            // 4. Memory is extremely limited (O(1) space complexity)
        }

        /// <summary>
        /// Optimized Insertion Sort for performance testing (without step-by-step output)
        /// </summary>
        public static void InsertionSort(int[] arr)
        {
            int n = arr.Length;
            for (int i = 1; i < n; i++)
            {
                int key = arr[i];
                int j = i - 1;
                while (j >= 0 && arr[j] > key)
                {
                    arr[j + 1] = arr[j];
                    j--;
                }
                arr[j + 1] = key;
            }
        }
    }
}