using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Assignment8
{
    /// <summary>
    /// Core spell checker class that uses HashSet<string> for efficient word lookups and text analysis.
    /// This class demonstrates key HashSet concepts including fast Contains() operations,
    /// automatic uniqueness enforcement, and set-based text processing.
    /// </summary>
    public class SpellChecker
    {
        // Core HashSet for dictionary storage - provides O(1) word lookups
        private HashSet<string> dictionary;
        
        // Text analysis results - populated after analyzing a file
        private List<string> allWordsInText;
        private HashSet<string> uniqueWordsInText;
        private HashSet<string> correctlySpelledWords;
        private HashSet<string> misspelledWords;
        private string currentFileName;
        
        public SpellChecker()
        {
            dictionary = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            allWordsInText = new List<string>();
            uniqueWordsInText = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            correctlySpelledWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            misspelledWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            currentFileName = "";
        }
        
        /// <summary>
        /// Gets the number of words in the loaded dictionary.
        /// </summary>
        public int DictionarySize => dictionary.Count;
        
        /// <summary>
        /// Gets whether a text file has been analyzed.
        /// </summary>
        public bool HasAnalyzedText => !string.IsNullOrEmpty(currentFileName);
        
        /// <summary>
        /// Gets the name of the currently analyzed file.
        /// </summary>
        public string CurrentFileName => currentFileName;
        
        /// <summary>
        /// Gets basic statistics about the analyzed text.
        /// </summary>
        public (int totalWords, int uniqueWords, int correctWords, int misspelledWords) GetTextStats()
        {
            return (
                allWordsInText.Count,
                uniqueWordsInText.Count,
                correctlySpelledWords.Count,
                misspelledWords.Count
            );
        }
        
        /// <summary>
        /// TODO #1: Load Dictionary into HashSet
        /// 
        /// Load words from the specified file into the dictionary HashSet.
        /// Requirements:
        /// - Read all lines from the file
        /// - Normalize each word (trim whitespace, convert to lowercase)
        /// - Add normalized words to the dictionary HashSet
        /// - Handle file not found gracefully
        /// - Return true if successful, false if file cannot be loaded
        /// 
        /// Key Concepts:
        /// - HashSet automatically handles duplicates
        /// - StringComparer.OrdinalIgnoreCase for case-insensitive operations
        /// - File I/O with proper error handling
        /// </summary>
        public bool LoadDictionary(string filename)
        {

            string fileToLoad = @$"Files\{filename}";

            // Check if the file is not empty
            if (!IsFileEmpty(fileToLoad))
            {
                try
                {
                    // Read all lines from the dictionary file
                    string[] words = File.ReadAllLines(fileToLoad);

                    // Loop through each word and add to the dictionary
                    foreach (string word in words)
                    {
                        // Normalize the word
                        string normalizedWord = NormalizeWord(word);

                        // Add to dictionary if not empty
                        if (!string.IsNullOrEmpty(normalizedWord))
                        {
                            dictionary.Add(normalizedWord);
                        }
                    }

                    // Successfully loaded dictionary
                    return true;
                }
                catch (FileNotFoundException)
                {
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        
        /// <summary>
        /// TODO #2: Analyze Text File
        /// 
        /// Load and analyze a text file, populating all internal collections.
        /// Requirements:
        /// - Read the entire file content
        /// - Tokenize into words (split on whitespace and punctuation)
        /// - Normalize each token consistently
        /// - Populate allWordsInText with all tokens (preserving duplicates)
        /// - Populate uniqueWordsInText with unique tokens
        /// - Return true if successful, false if file cannot be loaded
        /// 
        /// Key Concepts:
        /// - Text tokenization and normalization
        /// - List<T> for preserving order and duplicates
        /// - HashSet<T> for automatic uniqueness
        /// - Regex for advanced text processing (stretch goal)
        /// </summary>
        public bool AnalyzeTextFile(string filename)
        {
            currentFileName = $@"Files\{filename}";

            // Check if the file is not empty
            if (!IsFileEmpty(currentFileName))
            {
                try
                {
                    // Read the entire content of the text file
                    string content = File.ReadAllText(currentFileName);

                    // Tokenize the content into words
                    char[] delimiters = new char[] { ' ', '\t', '\n', '\r' };

                    // Use Regex to remove punctuation
                    string cleanedContent = Regex.Replace(content, @"[^\w\s]", "");

                    // Split into tokens
                    string[] tokens = cleanedContent.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

                    // Loop through each token
                    foreach (string token in tokens)
                    {
                        // Normalize the token
                        string normalizedToken = NormalizeWord(token);

                        // Add to allWordsInText
                        allWordsInText.Add(normalizedToken);

                        // Add to uniqueWordsInText
                        uniqueWordsInText.Add(normalizedToken);
                    }

                    return true;
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("File not found.");
                    return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred while analyzing the text file:" + ex);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        
        /// <summary>
        /// TODO #3: Categorize Words by Spelling
        /// 
        /// After analyzing text, categorize unique words into correct and misspelled.
        /// Requirements:
        /// - Iterate through uniqueWordsInText
        /// - Use dictionary.Contains() to check each word
        /// - Add words to correctlySpelledWords or misspelledWords accordingly
        /// - Clear previous categorization before processing
        /// 
        /// Key Concepts:
        /// - HashSet.Contains() provides O(1) membership testing
        /// - Set partitioning based on criteria
        /// - Defensive programming (clear previous results)
        /// </summary>
        public void CategorizeWords()
        {
            // Clear previous results
            correctlySpelledWords.Clear();
            misspelledWords.Clear();

            // Iterate through unique words and categorize
            foreach (var word in uniqueWordsInText)
            {
                // Check if the word is in the dictionary
                if (dictionary.Contains(word))
                {
                    // Word is correctly spelled
                    correctlySpelledWords.Add(word);
                }
                else
                {
                    // Word is misspelled
                    misspelledWords.Add(word);
                }
            }
        }
        
        /// <summary>
        /// TODO #4: Check Individual Word
        /// 
        /// Check if a specific word is in the dictionary and/or appears in analyzed text.
        /// Requirements:
        /// - Normalize the input word consistently
        /// - Check if word exists in dictionary
        /// - If text has been analyzed, check if word appears in text
        /// - If word appears in text, count occurrences in allWordsInText
        /// - Return comprehensive information about the word
        /// 
        /// Key Concepts:
        /// - Consistent normalization across all operations
        /// - Multiple HashSet lookups for comprehensive analysis
        /// - LINQ or manual counting for frequency analysis
        /// </summary>
        public (bool inDictionary, bool inText, int occurrences) CheckWord(string word)
        {
            // Normalize the input word
            string normalizedWord = NormalizeWord(word);

            // Handle empty input
            if (string.IsNullOrEmpty(normalizedWord))
            {
                return (false, false, 0);
            }

            // Check presence in dictionary and text, and count occurrences
            if (dictionary.Contains(normalizedWord) && uniqueWordsInText.Contains(normalizedWord))
            {
                return (true, true, allWordsInText.Count(w => w.Equals(normalizedWord, StringComparison.OrdinalIgnoreCase)));
            }
            // Word is only in the dictionary
            else if (dictionary.Contains(normalizedWord))
            {
                return (true, false, 0);
            }
            // Word is only in the analyzed text
            else if (uniqueWordsInText.Contains(normalizedWord))
            {
                return (false, true, allWordsInText.Count(w => w.Equals(normalizedWord, StringComparison.OrdinalIgnoreCase)));
            }
            else
            {
                return (false, false, 0);
            }
        }
        
        /// <summary>
        /// TODO #5: Get Misspelled Words
        /// 
        /// Return a sorted list of all misspelled words from the analyzed text.
        /// Requirements:
        /// - Return words from misspelledWords HashSet
        /// - Sort alphabetically for consistent display
        /// - Limit results if there are too many (optional)
        /// - Return empty collection if no text analyzed
        /// 
        /// Key Concepts:
        /// - Converting HashSet to sorted List
        /// - LINQ for sorting and limiting results
        /// - Defensive programming for uninitialized state
        /// </summary>
        public List<string> GetMisspelledWords(int maxResults = 50)
        {
            // Check if a text has been analyzed
            if (HasAnalyzedText)
            {
                // Convert HashSet to List for sorting
                List<string> misspelledWordsList = new List<string>(misspelledWords);

                // Sort the list alphabetically
                misspelledWordsList.Sort(StringComparer.OrdinalIgnoreCase);

                // Limit results if necessary
                if (misspelledWordsList.Count > maxResults)
                {
                    return misspelledWordsList.Take(maxResults).ToList();
                }
                else
                {
                    return misspelledWordsList;
                }
            }
            else
            {
                return new List<string>();
            }
        }
        
        /// <summary>
        /// TODO #6: Get Unique Words Sample
        /// 
        /// Return a sample of unique words found in the analyzed text.
        /// Requirements:
        /// - Return words from uniqueWordsInText HashSet
        /// - Sort alphabetically for consistent display
        /// - Limit to specified number of results
        /// - Return empty collection if no text analyzed
        /// 
        /// Key Concepts:
        /// - HashSet enumeration and conversion
        /// - LINQ for data manipulation
        /// - Sampling large datasets
        /// </summary>
        public List<string> GetUniqueWordsSample(int maxResults = 20)
        {
            // Check if a text has been analyzed
            if (HasAnalyzedText)
            {
                // Convert HashSet to List for sorting
                List<string> uniqueWordsList = new List<string>(uniqueWordsInText);

                // Sort the list alphabetically
                uniqueWordsList.Sort(StringComparer.OrdinalIgnoreCase);

                // Limit results if necessary
                if (uniqueWordsList.Count > maxResults)
                {
                    return uniqueWordsList.Take(maxResults).ToList();
                }
                else
                {
                    return uniqueWordsList;
                }
            }
            else
            {
                return new List<string>();
            }
        }
        
        // Helper method for consistent word normalization
        private string NormalizeWord(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
                return "";
                
            // Remove punctuation and convert to lowercase
            word = Regex.Replace(word.Trim(), @"[^\w]", "");
            return word.ToLowerInvariant();
        }

        // Heler method to check if a file is empty
        public bool IsFileEmpty(string filePath)
        {
            try
            {
                // Create a FileInfo object for the file path
                FileInfo fileInfo = new FileInfo(filePath);

                //  Check if the file exists AND if its Length property is 0
                if (fileInfo.Exists && fileInfo.Length == 0)
                {
                    return true; // File exists and is empty
                }
                else if (fileInfo.Exists && fileInfo.Length > 0)
                {
                    return false; // File exists and is NOT empty
                }
                else
                {
                    // Handle the case where the file doesn't exist
                    Console.WriteLine($"Error: File not found at {filePath}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Handle path errors or other unexpected exceptions
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }
    }
}