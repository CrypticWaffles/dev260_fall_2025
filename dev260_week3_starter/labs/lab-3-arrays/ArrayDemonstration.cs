using System;

namespace ArrayDemonstration
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== C# Arrays Demonstration ===\n");
            
            // 1. Single Dimensional Arrays
            SingleDimensionalArrayDemo();
            
            Console.WriteLine("\n" + new string('=', 50) + "\n");
            
            // 2. Multi-Dimensional Arrays
            MultiDimensionalArrayDemo();
            
            Console.WriteLine("\n" + new string('=', 50) + "\n");
            
            // 3. Jagged Arrays
            JaggedArrayDemo();
            
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
        
        static void SingleDimensionalArrayDemo()
        {
            Console.WriteLine("1. SINGLE DIMENSIONAL ARRAYS");
            Console.WriteLine("=============================");
            
            // Declaration and initialization methods
            Console.WriteLine("\n--- Declaration and Initialization ---");
            
            // Method 1: Declare size then assign values
            int[] numbers1 = new int[5];
            numbers1[0] = 10;
            numbers1[1] = 20;
            numbers1[2] = 30;
            numbers1[3] = 40;
            numbers1[4] = 50;
            
            // Method 2: Declare and initialize in one line
            int[] numbers2 = {1, 2, 3, 4, 5};
            
            // Method 3: Using new keyword with initializer
            string[] fruits = new string[] {"Apple", "Banana", "Orange", "Grape"};
            
            // Method 4: Array.CreateInstance (less common)
            Array numbers3 = Array.CreateInstance(typeof(int), 3);
            numbers3.SetValue(100, 0);
            numbers3.SetValue(200, 1);
            numbers3.SetValue(300, 2);
            
            Console.WriteLine("\n--- Displaying Arrays ---");
            
            // Display arrays using different methods
            Console.Write("numbers1: ");
            for (int i = 0; i < numbers1.Length; i++)
            {
                Console.Write($"{numbers1[i]} ");
            }
            
            Console.Write("\nnumbers2: ");
            foreach (int num in numbers2)
            {
                Console.Write($"{num} ");
            }
            
            Console.Write("\nfruits: ");
            Console.WriteLine(string.Join(", ", fruits));
            
            Console.WriteLine("\n--- Array Properties and Methods ---");
            Console.WriteLine($"Length of numbers1: {numbers1.Length}");
            Console.WriteLine($"Rank of numbers1: {numbers1.Rank}");
            Console.WriteLine($"Max value in numbers1: {numbers1.Max()}");
            Console.WriteLine($"Min value in numbers1: {numbers1.Min()}");
            Console.WriteLine($"Sum of numbers1: {numbers1.Sum()}");
            
            // Array manipulation
            Console.WriteLine("\n--- Array Manipulation ---");
            Array.Sort(fruits);
            Console.WriteLine($"Sorted fruits: {string.Join(", ", fruits)}");
            
            Array.Reverse(numbers2);
            Console.WriteLine($"Reversed numbers2: {string.Join(", ", numbers2)}");
            
            int index = Array.IndexOf(fruits, "Orange");
            Console.WriteLine($"Index of 'Orange': {index}");
        }
        
        static void MultiDimensionalArrayDemo()
        {
            Console.WriteLine("2. MULTI-DIMENSIONAL ARRAYS (Rectangular)");
            Console.WriteLine("==========================================");
            
            Console.WriteLine("\n--- 2D Array Declaration and Initialization ---");
            
            // Method 1: Declare size then assign
            int[,] matrix1 = new int[3, 4]; // 3 rows, 4 columns
            
            // Populate the matrix
            int value = 1;
            for (int i = 0; i < matrix1.GetLength(0); i++) // Getting number of rows
            {
                for (int j = 0; j < matrix1.GetLength(1); j++) // Getting number of columns
                {
                    matrix1[i, j] = value++;
                }
            }
            
            // Method 2: Declare and initialize with values
            int[,] matrix2 = {
                {1, 2, 3},
                {4, 5, 6},
                {7, 8, 9}
            };
            
            // Method 3: More explicit initialization
            string[,] chessBoard = new string[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    chessBoard[i, j] = ((i + j) % 2 == 0) ? "⬜" : "⬛"; // Look for even/odd sum of indices
                }
            }
            
            Console.WriteLine("\n--- Displaying 2D Arrays ---");
            
            Console.WriteLine("Matrix1 (3x4):");
            DisplayMatrix(matrix1);
            
            Console.WriteLine("\nMatrix2 (3x3):");
            DisplayMatrix(matrix2);
            
            Console.WriteLine("\nChess Board Pattern (8x8) - First 4x4:");
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Console.Write($"{chessBoard[i, j]} ");
                }
                Console.WriteLine();
            }
            
            Console.WriteLine("\n--- 3D Array Example ---");
            
            // 3D Array: Think of it as multiple 2D arrays stacked
            int[,,] cube = new int[2, 3, 4]; // 2 layers, 3 rows, 4 columns
            
            // Fill the cube
            value = 1;
            for (int layer = 0; layer < cube.GetLength(0); layer++) // layers
            {
                for (int row = 0; row < cube.GetLength(1); row++) // rows
                {
                    for (int col = 0; col < cube.GetLength(2); col++) // columns
                    {
                        cube[layer, row, col] = value++;
                    }
                }
            }
            
            Console.WriteLine("3D Array (2x3x4) - Layer by layer:");
            for (int layer = 0; layer < cube.GetLength(0); layer++)
            {
                Console.WriteLine($"Layer {layer}:");
                for (int row = 0; row < cube.GetLength(1); row++)
                {
                    for (int col = 0; col < cube.GetLength(2); col++)
                    {
                        Console.Write($"{cube[layer, row, col]:D2} ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
            
            Console.WriteLine("\n--- Multi-Dimensional Array Properties ---");
            Console.WriteLine($"Matrix2 Rank: {matrix2.Rank}");
            Console.WriteLine($"Matrix2 Length: {matrix2.Length}");
            Console.WriteLine($"Matrix2 Rows: {matrix2.GetLength(0)}");
            Console.WriteLine($"Matrix2 Columns: {matrix2.GetLength(1)}");
        }

        static void JaggedArrayDemo()
        {
            Console.WriteLine("3. JAGGED ARRAYS (Arrays of Arrays)");
            Console.WriteLine("====================================");

            Console.WriteLine("\n--- Declaration and Initialization ---");

            // Method 1: Declare then initialize each row
            int[][] jaggedArray1 = new int[4][]; // 4 rows, but columns vary

            jaggedArray1[0] = new int[3] { 1, 2, 3 };
            jaggedArray1[1] = new int[5] { 4, 5, 6, 7, 8 };
            jaggedArray1[2] = new int[2] { 9, 10 };
            jaggedArray1[3] = new int[4] { 11, 12, 13, 14 };

            // Method 2: Initialize with values directly
            string[][] studentGrades = {
                new string[] {"Alice", "A", "B+", "A-"},
                new string[] {"Bob", "B", "C+"},
                new string[] {"Charlie", "A-", "A", "B", "A+", "B+"},
                new string[] {"Diana", "B+", "A"}
            };

            // Method 3: Different data types in each row (object arrays)
            object[][] mixedData = {
                new object[] {"Student Info", "Name", "Age", "GPA"},
                new object[] {1, "John Doe", 20, 3.75},
                new object[] {2, "Jane Smith", 19, 3.92},
                new object[] {3, "Mike Johnson", 21, 3.45}
            };

            Console.WriteLine("\n--- Displaying Jagged Arrays ---");

            Console.WriteLine("Jagged Array 1 (varying row lengths):");
            for (int i = 0; i < jaggedArray1.Length; i++)// rows
            {
                Console.Write($"Row {i}: ");
                for (int j = 0; j < jaggedArray1[i].Length; j++) // columns
                {
                    Console.Write($"{jaggedArray1[i][j]} ");
                }
                Console.WriteLine($"(Length: {jaggedArray1[i].Length})");
            }

            Console.WriteLine("\nStudent Grades (jagged string array):");
            for (int i = 0; i < studentGrades.Length; i++)
            {
                Console.WriteLine($"{studentGrades[i][0]}: {string.Join(", ", studentGrades[i].Skip(1))}");
            }

            Console.WriteLine("\nMixed Data (object jagged array):");
            for (int i = 0; i < mixedData.Length; i++)
            {
                Console.WriteLine($"Row {i}: {string.Join(" | ", mixedData[i])}");
            }

            Console.WriteLine("\n--- Jagged Array Operations ---");

            // Calculate total elements across all rows
            int totalElements = 0;
            foreach (int[] row in jaggedArray1)
            {
                totalElements += row.Length;
            }
            Console.WriteLine($"Total elements in jaggedArray1: {totalElements}");

            // Find longest and shortest rows
            int maxRowLength = jaggedArray1.Max(row => row.Length);
            int minRowLength = jaggedArray1.Min(row => row.Length);
            Console.WriteLine($"Longest row length: {maxRowLength}");
            Console.WriteLine($"Shortest row length: {minRowLength}");

            // Sum all elements
            int sum = jaggedArray1.SelectMany(row => row).Sum();
            Console.WriteLine($"Sum of all elements: {sum}");

            Console.WriteLine("\n--- Memory Efficiency Comparison ---");

            // Jagged arrays are more memory efficient when rows have different sizes
            // Multi-dimensional arrays allocate memory for the full rectangle

            int[][] efficientJagged = {
                new int[10],    // Small row
                new int[5],     // Medium row  
                new int[1],     // Tiny row
                new int[100]    // Large row
            };

            int[,] wastefulRectangular = new int[4, 100]; // Wastes memory for small rows

            Console.WriteLine($"Jagged array total allocated integers: {efficientJagged.Sum(row => row.Length)}");
            Console.WriteLine($"Rectangular array total allocated integers: {wastefulRectangular.Length}");
            Console.WriteLine("Jagged arrays are more memory efficient when row sizes vary significantly!");

            // game board -> array type -> multi-dimensional vs jagged -> fixed grid structure
            // student grades -> jagged array -> varying number of grades per student
            // image processing -> multi-dimensional array -> pixel grid
            // text file lines -> jagged array -> varying line lengths
            // mathematical matrices -> multi-dimensional array -> fixed size for operations
            // database query results -> jagged array -> varying number of columns per row
        }
        
        static void DisplayMatrix(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write($"{matrix[i, j]:D2} ");
                }
                Console.WriteLine();
            }
        }
    }
}