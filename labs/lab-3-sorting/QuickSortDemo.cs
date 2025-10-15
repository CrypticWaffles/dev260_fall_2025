using System;
using System.Linq;

namespace SortingAlgorithmsDemo
{
    /// <summary>
    /// Quick Sort demonstration with step-by-step visualization
    /// Educational Focus: Understanding divide-and-conquer and recursive sorting
    /// </summary>
    public static class QuickSortDemo
    {
        /// <summary>
        /// Demonstrate Quick Sort with detailed explanation
        /// </summary>
        public static void Demonstrate(int[] originalArray)
        {
            Console.WriteLine("4. QUICK SORT");
            Console.WriteLine("-------------");
            Console.WriteLine("Time Complexity: O(n log n) average, O(n²) worst | Space Complexity: O(log n)");
            Console.WriteLine("Description: Divides array around pivot and recursively sorts partitions\n");
            int[] quickArray = (int[])originalArray.Clone();
            Console.WriteLine($"Starting array: [{string.Join(", ", quickArray)}]");
            QuickSortWithSteps(quickArray, 0, quickArray.Length - 1, 0);
            Console.WriteLine($"Final sorted:   [{string.Join(", ", quickArray)}]");
        }

        /// <summary>
        /// Quick Sort with step-by-step visualization
        /// Time Complexity: O(n log n) average/best case, O(n²) worst case (poor pivot selection)
        /// Space Complexity: O(log n) due to recursion stack depth
        /// 
        /// Key Features:
        ///  Divide-and-Conquer Strategy: Recursively partitions array around pivot elements
        ///  In-Place Sorting: Sorts within the original array with minimal extra memory
        ///  Not Stable: Equal elements may change relative order during partitioning
        ///  Cache-Efficient: Good locality of reference makes it fast in practice
        ///  Pivot Selection Critical: Performance heavily depends on choosing good pivots
        ///  Industry Standard: Basis for most standard library sort implementations
        /// </summary>
        static void QuickSortWithSteps(int[] arr, int low, int high, int depth)
        {
            // Base case: If low >= high, the subarray has 0 or 1 elements and is already sorted
            if (low < high)
            {
                // Create indentation for visualization of recursion depth
                // This helps students see the recursive call structure
                string indent = new string(' ', depth * 2);
                Console.WriteLine($"{indent}Sorting subarray from index {low} to {high}: [{string.Join(", ", arr.Skip(low).Take(high - low + 1))}]");
                
                // PARTITION PHASE: Divide the array around a pivot
                // After partitioning, the pivot will be in its final sorted position
                // All elements to the left will be ≤ pivot, all elements to the right will be ≥ pivot
                int pi = PartitionWithSteps(arr, low, high, depth);
                
                Console.WriteLine($"{indent}Pivot {arr[pi]} is now at correct position {pi}");
                Console.WriteLine($"{indent}Array state: [{string.Join(", ", arr)}]");
                
                // CONQUER PHASE: Recursively sort the two partitions
                // The pivot is already in its correct position, so we exclude it from further sorting
                
                // Left partition: elements from low to (pi-1)
                // Only recurse if there are at least 2 elements to sort
                if (pi > low + 1)
                {
                    Console.WriteLine($"{indent}Recursively sorting left partition (indices {low} to {pi - 1})");
                    QuickSortWithSteps(arr, low, pi - 1, depth + 1);
                }
                
                // Right partition: elements from (pi+1) to high
                // Only recurse if there are at least 2 elements to sort
                if (pi < high - 1)
                {
                    Console.WriteLine($"{indent}Recursively sorting right partition (indices {pi + 1} to {high})");
                    QuickSortWithSteps(arr, pi + 1, high, depth + 1);
                }
                
                // COMBINE PHASE: No explicit combining needed!
                // Once both partitions are sorted, the entire subarray is sorted
                // This is the beauty of quicksort - the partitioning does the heavy lifting
            }
            // If low >= high, this recursive call does nothing (base case reached)
        }

        /// <summary>
        /// Partition function for Quick Sort with visualization
        /// This is the heart of QuickSort - it rearranges the array so that:
        /// 1. All elements ≤ pivot are moved to the left side
        /// 2. All elements ≥ pivot are moved to the right side  
        /// 3. The pivot ends up in its final sorted position
        /// 
        /// Uses Lomuto Partition Scheme: Simple but effective partitioning algorithm
        /// Alternative: Hoare Partition Scheme (more efficient but complex)
        /// </summary>
        static int PartitionWithSteps(int[] arr, int low, int high, int depth)
        {
            string indent = new string(' ', depth * 2);
            
            // PIVOT SELECTION: Choose the last element as pivot (Lomuto scheme)
            // Other strategies: first element, random element, median-of-three
            // Pivot choice significantly affects performance on sorted/reverse-sorted data
            int pivot = arr[high]; // Choose last element as pivot
            Console.WriteLine($"{indent}Chosen pivot: {pivot} (at index {high})");
            
            // PARTITIONING SETUP:
            // i tracks the boundary between "small" and "large" elements
            // Elements from low to i are ≤ pivot
            // Elements from i+1 to j-1 are > pivot
            // Elements from j to high-1 are unprocessed
            int i = low - 1; // Index of the last element ≤ pivot (starts before array)
            
            // PARTITIONING LOOP: Process each element except the pivot
            for (int j = low; j < high; j++)
            {
                Console.Write($"{indent}Compare {arr[j]} with pivot {pivot}: ");
                
                // If current element is smaller than or equal to pivot,
                // it belongs in the "small elements" section
                if (arr[j] < pivot)
                {
                    i++; // Expand the "small elements" section
                    Console.WriteLine($"{arr[j]} < {pivot}, swap with element at index {i}");
                    
                    // Swap current element with the first element in "large elements" section
                    // This moves the small element to the correct side and grows the small section
                    (arr[i], arr[j]) = (arr[j], arr[i]);
                    Console.WriteLine($"{indent}  → [{string.Join(", ", arr)}]");
                    
                    // Invariant maintained: arr[low..i] contains elements ≤ pivot
                }
                else
                {
                    // Element is greater than pivot, so it stays in the "large elements" section
                    // No swap needed - just continue to next element
                    Console.WriteLine($"{arr[j]} >= {pivot}, no swap");
                    // Invariant maintained: arr[i+1..j] contains elements > pivot
                }
            }
            
            // FINAL PIVOT PLACEMENT:
            // Place pivot in its correct position (between small and large elements)
            // After this swap: arr[low..i] ≤ pivot, arr[i+1] = pivot, arr[i+2..high] ≥ pivot
            (arr[i + 1], arr[high]) = (arr[high], arr[i + 1]);
            Console.WriteLine($"{indent}Place pivot {pivot} at index {i + 1}");
            
            // Return the final position of the pivot
            // This position is now correctly sorted and won't move again
            return i + 1;
        }

        /// <summary>
        /// Optimized Quick Sort for performance testing (without step-by-step output)
        /// </summary>
        public static void QuickSort(int[] arr, int low, int high)
        {
            if (low < high)
            {
                int pi = Partition(arr, low, high);
                QuickSort(arr, low, pi - 1);
                QuickSort(arr, pi + 1, high);
            }
        }

        /// <summary>
        /// Optimized partition function for performance testing
        /// </summary>
        static int Partition(int[] arr, int low, int high)
        {
            int pivot = arr[high];
            int i = low - 1;
            
            for (int j = low; j < high; j++)
            {
                if (arr[j] < pivot)
                {
                    i++;
                    (arr[i], arr[j]) = (arr[j], arr[i]);
                }
            }
            (arr[i + 1], arr[high]) = (arr[high], arr[i + 1]);
            return i + 1;
        }
    }
}