# Sorting Algorithms Educational Demo - Modular Structure

## Overview

This project has been refactored into a modular structure to make it easier for students to review and understand each component individually. Each sorting alg`orithm and major functionality is now in its own file.

## File Organization

### 📁 Main Program

- **`SortingDemonstration.cs`** - Main program entry point and interactive menu system

### 📁 Sorting Algorithm Demonstrations

- **`BubbleSortDemo.cs`** - Bubble Sort implementation with step-by-step visualization
- **`SelectionSortDemo.cs`** - Selection Sort implementation with step-by-step visualization
- **`InsertionSortDemo.cs`** - Insertion Sort implementation with step-by-step visualization
- **`QuickSortDemo.cs`** - Quick Sort implementation with step-by-step visualization

### � Analysis Modules

- **`PerformanceAnalyzer.cs`** - Performance testing and empirical analysis
- **`ComplexityAnalyzer.cs`** - Theoretical complexity analysis and insights

### 📁 Project Files

- **`SortingDemonstration.csproj`** - .NET project configuration
- **`README.md`** - This documentation file

## Educational Benefits of Modular Structure

1. **Focus on One Algorithm**: Students can study one sorting algorithm at a time without distractions
2. **Clear Separation of Concerns**: Each file has a single responsibility
3. **Easy Comparison**: Students can open two algorithm files side-by-side to compare implementations
4. **Progressive Learning**: Start with simple algorithms (Bubble Sort) before moving to complex ones (Quick Sort)

## How to Use

### Running the Program

```bash
cd lab-3-sorting
dotnet run
```

### Studying Individual Algorithms

1. **Start with `BubbleSortDemo.cs`** - Simplest algorithm, good for understanding basics
2. **Move to `SelectionSortDemo.cs`** - Introduces concept of finding minimum
3. **Progress to `InsertionSortDemo.cs`** - Shows incremental building of sorted portion
4. **Advanced: `QuickSortDemo.cs`** - Demonstrates recursion and divide-and-conquer

### Understanding Performance

- **`PerformanceAnalyzer.cs`** - Shows how to properly benchmark algorithms
- **`ComplexityAnalyzer.cs`** - Theoretical analysis and practical recommendations

## Key Features in Each File

### Algorithm Demo Files Include:

- ✅ **Detailed Comments**: Every line explained for educational purposes
- ✅ **Step-by-Step Visualization**: See how data moves through the algorithm
- ✅ **Complexity Analysis**: Time and space complexity documented
- ✅ **Performance Versions**: Optimized versions for benchmarking
- ✅ **Educational Insights**: When to use each algorithm

### Analysis Files Include:

- ✅ **Empirical Testing**: Real-world performance measurement
- ✅ **Big O Demonstration**: See theory in practice
- ✅ **Comparison Tables**: Easy-to-read algorithm comparisons
- ✅ **Practical Guidance**: When to choose which algorithm

## Learning Path Recommendation

### 🔰 **Beginner Path:**

1. Read `BubbleSortDemo.cs` completely
2. Run program and select Bubble Sort demonstration
3. Repeat for Selection Sort and Insertion Sort
4. Compare the three O(n²) algorithms

### 🔶 **Intermediate Path:**

1. Study `QuickSortDemo.cs` for divide-and-conquer concepts
2. Run Performance Comparisons to see Big O in action
3. Study `PerformanceAnalyzer.cs` to understand benchmarking

### 🔸 **Advanced Path:**

1. Modify algorithms to experiment with variations
2. Study `ComplexityAnalyzer.cs` for theoretical understanding
3. Create your own sorting algorithm using the same structure

## Code Style and Standards

- **Comprehensive Comments**: Every method and key line documented
- **Educational Focus**: Code written for learning, not just efficiency
- **Consistent Naming**: Clear, descriptive variable and method names
- **Step-by-Step Output**: Visualization shows algorithm progress
- **Error Handling**: Robust input validation and error messages

## Next Steps for Students

1. **Experiment**: Modify the algorithms to see how changes affect behavior
2. **Compare**: Run different algorithms on the same data
3. **Analyze**: Use the performance tools to understand scaling
4. **Extend**: Add new sorting algorithms using the same structure
5. **Apply**: Use the knowledge to choose algorithms for real projects

---

_Remember: The best way to understand sorting algorithms is to trace through them step by step. Use the interactive demonstrations and detailed comments to build your understanding gradually._

- **How it works**: Divides array around a pivot element and recursively sorts partitions
- **Best for**: Large random datasets, general-purpose sorting
- **Characteristics**: Generally fastest, in-place, cache-friendly

## 🚀 How to Run

### Run the Sorting Demonstration

```bash
# Navigate to the lab-3 directory
cd lab-3-sorting

# Run the sorting program
dotnet run
```

## 📊 Program Features

### 1. **Step-by-Step Visualization**

- Shows each comparison and swap operation
- Displays array state after each step
- Counts total comparisons and swaps/shifts
- Easy to follow for educational purposes

### 2. **Performance Comparison**

- Tests all algorithms with arrays of sizes: 100, 1,000, 5,000 elements
- Measures execution time in milliseconds
- Compares with built-in Array.Sort() method
- Uses consistent random data for fair comparison

### 3. **Complexity Analysis**

- Visual table showing Big O notation for all cases
- Space complexity analysis
- Practical insights for when to use each algorithm
- Real-world performance considerations

## 📋 Sample Output Structure

```
=== Sorting Algorithms Demonstration ===

BASIC SORTING DEMONSTRATIONS
============================
Original Array: [64, 34, 25, 12, 22, 11, 90]

1. BUBBLE SORT
--------------
Time Complexity: O(n²) | Space Complexity: O(1)
[Step-by-step execution with comparisons and swaps]

2. SELECTION SORT
-----------------
[Detailed walkthrough of algorithm execution]

[... and so on for each algorithm]

PERFORMANCE COMPARISON
======================
Array Size: 1000 elements
----------------------------------------
Bubble Sort    :    25 ms ✓
Selection Sort :    15 ms ✓
Insertion Sort :     8 ms ✓
Quick Sort     :     2 ms ✓
Built-in Sort  :     1 ms ✓
```

## 🔧 Customization Options

### Modify Test Data:

```csharp
// Change the test array in DemonstrateBasicSorting()
int[] originalArray = { 64, 34, 25, 12, 22, 11, 90 };

// Adjust performance test sizes
int[] sizes = { 100, 1000, 5000 };
```

### Add New Algorithms:

- Extend the framework to include Merge Sort, Heap Sort, etc.
- Follow existing patterns for step-by-step visualization
- Add entries to the complexity analysis table

### Focus on Specific Algorithms:

- Comment out sections to focus on particular algorithms
- Adjust array sizes for different performance characteristics
- Modify visualization detail level

## 🧮 Complexity Reference

| Algorithm      | Best Case  | Average Case | Worst Case | Space    | Stable |
| -------------- | ---------- | ------------ | ---------- | -------- | ------ |
| Bubble Sort    | O(n)       | O(n²)        | O(n²)      | O(1)     | Yes    |
| Selection Sort | O(n²)      | O(n²)        | O(n²)      | O(1)     | No     |
| Insertion Sort | O(n)       | O(n²)        | O(n²)      | O(1)     | Yes    |
| Quick Sort     | O(n log n) | O(n log n)   | O(n²)      | O(log n) | No     |

## 💡 Advanced Topics for Discussion

- **Stability**: Whether equal elements maintain their relative order
- **Adaptive**: Performance improvement on partially sorted data
- **In-place**: Whether algorithm requires additional memory
- **Hybrid approaches**: How modern libraries combine multiple algorithms
- **Pivot selection**: Impact on Quick Sort performance
