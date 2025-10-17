using System;
using System.Collections.Generic;
using System.IO;

namespace Week3ArraysSorting
{
    /// <summary>
    /// Book Catalog implementation for Assignment 2 Part B
    /// Demonstrates recursive sorting and multi-dimensional indexing for fast lookups
    /// 
    /// Learning Focus:
    /// - Recursive sorting algorithms (QuickSort or MergeSort)
    /// - Multi-dimensional array indexing for performance
    /// - String normalization and binary search
    /// - File I/O and CLI interaction
    /// </summary>
    public class BookCatalog
    {
        #region Data Structures
        
        // Book storage arrays - parallel arrays that stay synchronized
        private string[] originalTitles;    // Original book titles for display
        private string[] normalizedTitles;  // Normalized titles for sorting/searching
        
        // Multi-dimensional index for O(1) lookup by first two letters (A-Z x A-Z = 26x26)
        private int[,] startIndex;  // Starting position for each letter pair in sorted 4array
        private int[,] endIndex;    // Ending position for each letter pair in sorted array
        
        // Book count tracker
        private int bookCount;
        
        #endregion
        
        /// <summary>
        /// Constructor - Initialize the book catalog
        /// Sets up data structures for book storage and multi-dimensional indexing
        /// </summary>
        public BookCatalog()
        {
            // Initialize arrays (will be resized when books are loaded)
            originalTitles = Array.Empty<string>();
            normalizedTitles = Array.Empty<string>();
            
            // Initialize multi-dimensional index arrays (26x26 for A-Z x A-Z)
            startIndex = new int[26, 26];
            endIndex = new int[26, 26];
            
            // Initialize all index ranges as empty (-1 indicates no books for that letter pair)
            for (int i = 0; i < 26; i++)
            {
                for (int j = 0; j < 26; j++)
                {
                    startIndex[i, j] = -1;  // -1 means no books start with this letter pair
                    endIndex[i, j] = -1;    // -1 means no books end with this letter pair
                }
            }
            
            // Reset book count
            bookCount = 0;
            
            Console.WriteLine("BookCatalog initialized - Ready to load books and build index");
        }
        
        /// <summary>
        /// Load books from file and build sorted index
        /// </summary>
        /// <param name="filePath">Path to books.txt file</param>
        public void LoadBooks(string filePath)
        {
            try
            {
                Console.WriteLine($"Loading books from: {filePath}");
                
                // Step 1 - Load books from file
                LoadBooksFromFile(filePath);
                
                // Step 2 - Sort using recursive algorithm
                SortBooksRecursively();
                
                // Step 3 - Build multi-dimensional index
                BuildMultiDimensionalIndex();
                
                Console.WriteLine($"Successfully loaded and indexed {bookCount} books.");
                Console.WriteLine("Index built for A-Z x A-Z (26x26) letter pairs.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading books: {ex.Message}");
                throw;
            }
        }
        
        /// <summary>
        /// Start interactive lookup session
        /// </summary>
        public void StartLookupSession()
        {
            Console.Clear();
            Console.WriteLine("=== BOOK CATALOG LOOKUP (Part B) ===");
            Console.WriteLine();
            
            if (bookCount == 0)
            {
                Console.WriteLine("No books loaded! Please load a book file first.");
                return;
            }
            
            DisplayLookupInstructions();
            
            bool keepLooking = true;
            
            while (keepLooking)
            {
                Console.WriteLine();
                Console.Write("Enter a book title (or 'exit'): ");
                string? query = Console.ReadLine();
                
                if (string.IsNullOrEmpty(query) || query.ToLowerInvariant() == "exit")
                {
                    keepLooking = false;
                    continue;
                }
                
                PerformLookup(query);
            }
            
            Console.WriteLine("Returning to main menu...");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        
        /// <summary>
        /// Load book titles from text file
        /// </summary>
        /// <param name="filePath">Path to the books file</param>
        private void LoadBooksFromFile(string filePath)
        {
            // Check if file exists
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Book file not found: {filePath}");
            }
            
            Console.WriteLine($"Reading book titles from: {filePath}");
            
            try
            {
                // Read all lines from file
                string[] lines = File.ReadAllLines(filePath);
                
                // Filter out empty lines and whitespace-only lines
                var validLines = new List<string>();
                foreach (string line in lines)
                {
                    string trimmedLine = line.Trim();
                    if (!string.IsNullOrEmpty(trimmedLine))
                    {
                        validLines.Add(trimmedLine);
                    }
                }
                
                // Initialize arrays with the correct size
                bookCount = validLines.Count;
                originalTitles = new string[bookCount];
                normalizedTitles = new string[bookCount];
                
                // Store both original and normalized versions
                for (int i = 0; i < bookCount; i++)
                {
                    originalTitles[i] = validLines[i]; // Keep original formatting for display
                    normalizedTitles[i] = NormalizeTitle(originalTitles[i]); // Normalized for sorting/indexing
                }
                
                Console.WriteLine($"Successfully loaded {bookCount} book titles.");
            }
            catch (IOException ex)
            {
                throw new IOException($"Error reading file '{filePath}': {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Unexpected error loading books from '{filePath}': {ex.Message}", ex);
            }
        }
        
        /// <summary>
        /// Normalize book title for consistent sorting and indexing
        /// </summary>
        /// <param name="title">Original book title</param>
        /// <returns>Normalized title for sorting/indexing</returns>
        private string NormalizeTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return "";
            }
            
            // Step 1: Trim whitespace and convert to uppercase
            string normalized = title.Trim().ToUpperInvariant();
            
            // Step 2: Optional - Remove leading articles for better sorting
            // This helps group books by their main title rather than article
            string[] articles = { "THE ", "A ", "AN " };
            
            foreach (string article in articles)
            {
                if (normalized.StartsWith(article))
                {
                    normalized = normalized.Substring(article.Length).Trim();
                    break; // Only remove the first article found
                }
            }
            
            // Step 3: Handle edge case where title was only articles
            if (string.IsNullOrEmpty(normalized))
            {
                return title.Trim().ToUpperInvariant(); // Return original if normalization results in empty
            }
            
            return normalized;
        }
        
        /// <summary>
        /// Sort books using recursive algorithm MergeSort
        /// </summary>
        private void SortBooksRecursively()
        {
            // validate arrays
            if (normalizedTitles == null || originalTitles == null || normalizedTitles.Length != originalTitles.Length)
            {
                Console.WriteLine("Error: Titles arrays are not properly initialized or mismatched.");
                return;
            }

            MergeSort(normalizedTitles, originalTitles, 0, bookCount - 1);
        }

        /// <summary>
        /// Recursive MergeSort implementation. Divides the array and merges sorted halves.
        /// </summary>
        /// <param name="normalizedTitles"></param>
        /// <param name="originalTitles"></param>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        private void MergeSort(string[] normalizedTitles, string[] originalTitles, int left, int right)
        {
            if (left < right)
            {
                // Find the middle point
                int mid = left + (right - left) / 2;

                // Recursively sort first and second halves
                MergeSort(normalizedTitles, originalTitles, left, mid);
                MergeSort(normalizedTitles, originalTitles, mid + 1, right);

                // Merge the sorted halves
                Merge(normalizedTitles, originalTitles, left, mid, right);
            }
        }

        /// <summary>
        /// Helper method to merge two sorted subarrays
        /// </summary>
        /// <param name="normalizedTitles"></param>
        /// <param name="originalTitles"></param>
        /// <param name="left"></param>
        /// <param name="mid"></param>
        /// <param name="right"></param>
        private void Merge(string[] normalizedTitles, string[] originalTitles, int left, int mid, int right)
        {
            // Find sizes of two subarrays to be merged
            int n1 = mid - left + 1;
            int n2 = right - mid;

            // Create temporary arrays
            string[] leftNorm = new string[n1];
            string[] rightNorm = new string[n2];
            string[] leftOrig = new string[n1];
            string[] rightOrig = new string[n2];

            int i, j, k;

            // Copy data to temporary arrays
            for ( i = 0; i < n1; i++)
            {
                leftNorm[i] = normalizedTitles[left + i];
                leftOrig[i] = originalTitles[left + i];
            }
            for ( j = 0; j < n2; j++)
            {
                rightNorm[j] = normalizedTitles[mid + 1 + j];
                rightOrig[j] = originalTitles[mid + 1 + j];
            }

            // Merge the temporary arrays back into normalizedTitles and originalTitles
            i = 0; // Initial index of first subarray
            j = 0; // Initial index of second subarray
            k = left; // Initial index of merged subarray

            while (i < n1 && j < n2)
            {
                // Compare normalized titles for sorting
                if (string.Compare(leftNorm[i], rightNorm[j], StringComparison.Ordinal) <= 0)
                {
                    normalizedTitles[k] = leftNorm[i];
                    originalTitles[k] = leftOrig[i];
                    i++;
                }
                else
                {
                    normalizedTitles[k] = rightNorm[j];
                    originalTitles[k] = rightOrig[j];
                    j++;
                }
                k++;
            }

            // Copy remaining elements of leftNorm and leftOrig, if any
            while (i < n1)
            {
                normalizedTitles[k] = leftNorm[i];
                originalTitles[k] = leftOrig[i];
                i++;
                k++;
            }
            // Copy remaining elements of rightNorm and rightOrig, if any
            while (j < n2)
            {
                normalizedTitles[k] = rightNorm[j];
                originalTitles[k] = rightOrig[j];
                j++;
                k++;
            }
        }

        /// <summary>
        /// Build multi-dimensional index over sorted data
        /// </summary>
        private void BuildMultiDimensionalIndex()
        {
            // Reset all index ranges to -1 (empty)
            for (int i = 0; i < 26; i++)
            {
                for (int j = 0; j < 26; j++)
                {
                    startIndex[i, j] = -1;
                    endIndex[i, j] = -1;
                }
            }

            int currentStart = -1;
            (int, int) pcurrentPair = (-1, -1);

            for (int bookIndex = 0; bookIndex < bookCount; bookIndex++)
            {
                string normalizedTitle = normalizedTitles[bookIndex];
                // Get first two letters' indices
                char firstLetter = normalizedTitle.Length >= 1 ? normalizedTitle[0] : ' ';
                char secondLetter = normalizedTitle.Length >= 2 ? normalizedTitle[1] : ' ';

                int firstLetterIndex = GetLetterIndex(firstLetter);
                int secondLetterIndex = GetLetterIndex(secondLetter);

                (int, int) newPair = (firstLetterIndex, secondLetterIndex);

                // If this is a new letter pair, update the index
                if (newPair.Item1 != pcurrentPair.Item1 || newPair.Item2 != pcurrentPair.Item2)
                {
                    // Close off previous range if it exists
                    if (currentStart !=  -1)
                    {
                        endIndex[pcurrentPair.Item1, pcurrentPair.Item2] = bookIndex - 1;
                    }

                    // Start new range
                    currentStart = bookIndex;
                    startIndex[firstLetterIndex, secondLetterIndex] = currentStart;
                    pcurrentPair = (firstLetterIndex, secondLetterIndex);
                }

                if (bookIndex == bookCount - 1 && currentStart != -1)
                {
                    endIndex[pcurrentPair.Item1, pcurrentPair.Item2] = bookIndex;
                }
            }
        }

        /// <summary>
        /// Converts a character to its 0-25 index (A=0, Z=25). Non-letters map to 0.
        /// </summary>
        private int GetLetterIndex(char c)
        {
            char upperC = char.ToUpperInvariant(c);

            if (upperC >= 'A' && upperC <= 'Z')
            {
                return upperC - 'A';
            }
            return 0; // Map non-letters to 'A' index
        }

        /// <summary>
        /// Custom Binary Search implementation to find an exact match within a sorted subarray.
        /// </summary>
        /// <returns>The index if found (>= 0), or the bitwise complement of the insertion point (~index) if not found.</returns>
        private int BinarySearchInRange(string query, int start, int end)
        {
            int low = start;
            int high = end;

            while (low <= high)
            {
                int mid = low + (high - low) / 2;
                // Compare the normalized query against the normalizedTitles array
                int comparison = string.Compare(query, normalizedTitles[mid], StringComparison.Ordinal);

                if (comparison == 0)
                {
                    return mid;
                }
                else if (comparison < 0)
                {
                    high = mid - 1;
                }
                else
                {
                    low = mid + 1;
                }
            }
            return ~low;
        }

        /// <summary>
        /// Perform lookup with exact match and suggestions
        /// </summary>
        /// <param name="query">User's search query</param>
        private void PerformLookup(string query)
        {
            // Normalize query same way as indexing/sorting
            string normalizedQuery = NormalizeTitle(query);

            // 1. Get first two letters for indexing
            char c1 = normalizedQuery.Length >= 1 ? normalizedQuery[0] : ' ';
            char c2 = normalizedQuery.Length >= 2 ? normalizedQuery[1] : ' ';

            int idx1 = GetLetterIndex(c1);
            int idx2 = GetLetterIndex(c2);

            // 2. Look up [start,end] range from 2D index in O(1)
            int start = startIndex[idx1, idx2];
            int end = endIndex[idx1, idx2];

            Console.WriteLine($"\nSearching for '{query}' (Normalized: '{normalizedQuery}').");

            if (start == -1 || start > end)
            {
                Console.WriteLine($"> No books found starting with '{c1}{c2}'.");
                FindSuggestions(normalizedQuery, 5);
                return;
            }

            // 3. Binary search within the determined slice
            int foundIndex = BinarySearchInRange(normalizedQuery, start, end);

            if (foundIndex >= 0)
            {
                // Exact match found
                Console.WriteLine("\n=============================");
                Console.WriteLine($"> EXACT MATCH FOUND:");
                Console.WriteLine($"  Title: {originalTitles[foundIndex]}");
                Console.WriteLine("=============================");
            }
            else
            {
                // Not found exactly, show suggestions near the insertion point
                int insertionPoint = ~foundIndex;

                Console.WriteLine("\n> Exact match not found.");

                // 4. Show helpful suggestions
                FindSuggestions(normalizedQuery, 5, insertionPoint);
            }
        }

        /// <summary>
        /// Finds and displays suggestions around the insertion point or a given index.
        /// </summary>
        private void FindSuggestions(string normalizedQuery, int maxSuggestions, int insertionPoint = -1)
        {
            Console.WriteLine("\n> Suggestions (closest alphabetical matches):");

            // Define the center point for checking suggestions
            int center = (insertionPoint >= 0) ? insertionPoint : GetSuggestionStartingPoint(normalizedQuery);

            // Define a window around the center point
            int scanStart = Math.Max(0, center - 2);
            int scanEnd = Math.Min(bookCount - 1, center + maxSuggestions + 1);

            int suggestionsFound = 0;

            for (int i = scanStart; i <= scanEnd && suggestionsFound < maxSuggestions; i++)
            {
                if (i < 0 || i >= bookCount) continue;

                // only suggest titles that start with the same or nearby letter
                if (i == center || string.Compare(normalizedTitles[i].Substring(0, Math.Min(2, normalizedTitles[i].Length)), normalizedQuery.Substring(0, Math.Min(2, normalizedQuery.Length)), StringComparison.Ordinal) <= 2)
                {
                    Console.WriteLine($"  - {originalTitles[i]}");
                    suggestionsFound++;
                }
            }

            if (suggestionsFound == 0)
            {
                Console.WriteLine("  (No relevant suggestions in the immediate alphabetical range.)");
            }
        }

        /// <summary>
        /// Helper to estimate a good starting index for suggestions when the 2D index yields an empty range.
        /// </summary>
        private int GetSuggestionStartingPoint(string normalizedQuery)
        {
            if (string.IsNullOrEmpty(normalizedQuery)) return 0;

            char c1 = normalizedQuery[0];
            int idx1 = GetLetterIndex(c1);

            // Try the next column's start index
            for (int j = 0; j < 26; j++)
            {
                if (startIndex[idx1, j] != -1)
                {
                    return startIndex[idx1, j];
                }
            }

            // If the entire row is empty, jump to the start of the next letter 
            for (int i = idx1 + 1; i < 26; i++)
            {
                for (int j = 0; j < 26; j++)
                {
                    if (startIndex[i, j] != -1)
                    {
                        return startIndex[i, j];
                    }
                }
            }

            return 0;
        }

        /// <summary>
        /// Display lookup instructions
        /// </summary>
        private void DisplayLookupInstructions()
        {
            Console.WriteLine("BOOK LOOKUP INSTRUCTIONS (Indexed and Sorted):");
            Console.WriteLine("- Enter any book title to search (e.g., 'Gatsby' or 'catcher')");
            Console.WriteLine("- Exact matches shown, or suggestions provided.");
            Console.WriteLine("- Type 'exit' to return to main menu");
            Console.WriteLine();
            Console.WriteLine($"Catalog contains {bookCount} books, sorted and indexed for O(1) access.");
        }
    }
}