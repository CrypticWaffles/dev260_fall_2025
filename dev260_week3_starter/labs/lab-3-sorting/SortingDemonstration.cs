using System;

namespace SortingAlgorithmsDemo
{
    /// <summary>
    /// Main program for interactive sorting algorithms demonstration
    /// Educational Focus: Modular design with interactive menu system
    /// 
    /// File Organization:
    ///  SortingDemonstration.cs - Main program and menu system
    ///  BubbleSortDemo.cs - Bubble sort implementation and demonstration
    ///  SelectionSortDemo.cs - Selection sort implementation and demonstration
    ///  InsertionSortDemo.cs - Insertion sort implementation and demonstration
    ///  QuickSortDemo.cs - Quick sort implementation and demonstration
    ///  PerformanceAnalyzer.cs - Performance testing and benchmarking
    ///  ComplexityAnalyzer.cs - Theoretical analysis and algorithm insights
    /// </summary>
    class SortingDemonstration
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Sorting Algorithms Demonstration ===\n");
            
            // Interactive menu system for educational exploration
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== SORTING ALGORITHMS EDUCATIONAL DEMO ===");
                Console.WriteLine("============================================\n");
                
                Console.WriteLine("Please select which section you would like to explore:\n");
                Console.WriteLine("1. Basic Sorting Demonstrations");
                Console.WriteLine("   → Step-by-step visualization of how each algorithm works");
                Console.WriteLine("   → Small dataset to clearly see algorithm behavior");
                Console.WriteLine("   → Perfect for understanding algorithm mechanics\n");
                
                Console.WriteLine("2. Performance Comparisons");
                Console.WriteLine("   → Empirical timing tests with different data sizes");
                Console.WriteLine("   → See Big O notation in action");
                Console.WriteLine("   → Understand when to use which algorithm\n");
                
                Console.WriteLine("3. Algorithm Complexity Analysis");
                Console.WriteLine("   → Theoretical complexity comparison table");
                Console.WriteLine("   → Key insights about algorithm characteristics");
                Console.WriteLine("   → Practical guidance for algorithm selection\n");
                
                Console.WriteLine("4. Exit Program\n");
                
                Console.Write("Enter your choice (1-4): ");
                
                string input = Console.ReadLine();
                Console.WriteLine();
                
                switch (input)
                {
                    case "1":
                        DemonstrateBasicSorting();
                        break;
                    case "2":
                        PerformanceAnalyzer.PerformanceComparison();
                        break;
                    case "3":
                        ComplexityAnalyzer.ComplexityAnalysis();
                        break;
                    case "4":
                        Console.WriteLine("Thank you for exploring sorting algorithms!");
                        Console.WriteLine("Remember: Choose the right algorithm for your specific use case!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please enter 1, 2, 3, or 4.");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        continue;
                }
                
                // After each section, pause and ask if user wants to continue
                Console.WriteLine("\n" + new string('=', 80));
                Console.WriteLine("Section completed!");
                Console.Write("Press any key to return to the main menu...");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Demonstrates basic sorting with small arrays to show how each algorithm works
        /// Interactive version allowing users to select specific algorithms to explore
        /// </summary>
        static void DemonstrateBasicSorting()
        {
            // Test data - intentionally small to see step-by-step execution
            int[] originalArray = { 64, 34, 25, 12, 22, 11, 90 };
            
            while (true)
            {
                Console.Clear();
                Console.WriteLine("BASIC SORTING DEMONSTRATIONS");
                Console.WriteLine("============================\n");
                
                Console.WriteLine($"Original Array: [{string.Join(", ", originalArray)}]\n");
                
                Console.WriteLine("Select which sorting algorithm to demonstrate:\n");
                Console.WriteLine("1. Bubble Sort (O(n²) - Simple swapping of adjacent elements)");
                Console.WriteLine("2. Selection Sort (O(n²) - Find minimum and place at beginning)");
                Console.WriteLine("3. Insertion Sort (O(n²) - Build sorted array one element at a time)");
                Console.WriteLine("4. Quick Sort (O(n log n) - Divide and conquer with pivot)");
                Console.WriteLine("5. Show All Algorithms");
                Console.WriteLine("6. Return to Main Menu\n");
                
                Console.Write("Enter your choice (1-6): ");
                string input = Console.ReadLine();
                Console.WriteLine();
                
                switch (input)
                {
                    case "1":
                        BubbleSortDemo.Demonstrate(originalArray);
                        break;
                    case "2":
                        SelectionSortDemo.Demonstrate(originalArray);
                        break;
                    case "3":
                        InsertionSortDemo.Demonstrate(originalArray);
                        break;
                    case "4":
                        QuickSortDemo.Demonstrate(originalArray);
                        break;
                    case "5":
                        DemonstrateAllSortingAlgorithms(originalArray);
                        break;
                    case "6":
                        return; // Return to main menu
                    default:
                        Console.WriteLine("Invalid choice. Please enter 1-6.");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        continue;
                }
                
                Console.WriteLine("\nPress any key to return to sorting algorithm menu...");
                Console.ReadKey();
            }
        }
        
        /// <summary>
        /// Demonstrate all sorting algorithms in sequence
        /// </summary>
        static void DemonstrateAllSortingAlgorithms(int[] originalArray)
        {
            Console.WriteLine("COMPLETE SORTING ALGORITHM DEMONSTRATION");
            Console.WriteLine("========================================\n");
            
            BubbleSortDemo.Demonstrate(originalArray);
            Console.WriteLine("\n" + new string('-', 60) + "\n");
            
            SelectionSortDemo.Demonstrate(originalArray);
            Console.WriteLine("\n" + new string('-', 60) + "\n");
            
            InsertionSortDemo.Demonstrate(originalArray);
            Console.WriteLine("\n" + new string('-', 60) + "\n");
            
            QuickSortDemo.Demonstrate(originalArray);
        }
    }
}