# Assignment 8: Spell Checker & Vocabulary Explorer - Implementation Notes

**Name:** Miles Griffith

## HashSet Pattern Understanding

**How HashSet<T> operations work for spell checking:**
[Explain your understanding of how O(1) lookups, automatic uniqueness, and set-based categorization work together for efficient text analysis]
O(1) lookups mean that the program doesnt have to look through every single word in your list till it finds the one you need. It can instantly find the item you need.
Automatic uniqueness means you don't have to worry about du0plicates messing anything up.
Set based categoration means that it will more easily filter out unrelated words from your search. 

## Challenges and Solutions

**Biggest challenge faced:**
[Describe the most difficult part of the assignment - was it text normalization, HashSet operations, or file I/O handling?]
My biggest issue when implementing this was actually reading the text files. It took me several different attempts to get the program to even be able to find and read the files correctly.

**How you solved it:**
[Explain your solution approach and what helped you figure it out]
I tried various different file paths and methods of reading the files until I found one that worked. I also looked up documentation on file I/O in C# to ensure I was using the correct methods. 
It seems my issue was that i forgot to add a $ before my string paths to make them include the varibles.

**Most confusing concept:**
[What was hardest to understand about HashSet operations, text processing, or case-insensitive comparisons?]
The most confusing part was understanding how to iterate through the text and remove punctuation and normalize the words correctly. I don't really understand how  Regex.Replace(content, @"[^\w\s]", ""); works.

## Code Quality

**What you're most proud of in your implementation:**
[Highlight the best aspect of your code - maybe your normalization strategy, error handling, or efficient text analysis]
I'm most proud of how i was able to accurately describe my methods and variables with clear names and comments that made it easy to understand what each part of the code was doing.

**What you would improve if you had more time:**
[Identify areas for potential improvement - perhaps better tokenization, more robust error handling, or additional features]
If I had more time, I would improve the tokenization process to better handle edge cases, such as hyphenated words or contractions.

## Testing Approach

**How you tested your implementation:**
[Describe your overall testing strategy - how did you verify spell checking worked correctly?]
I tested my implementation by creating several text files with known misspellings and correct words.

**Test scenarios you used:**
[List specific scenarios you tested, like mixed case words, punctuation handling, edge cases, etc.]
- A text file with all correctly spelled words to ensure no false positives.
- A text file with common misspellings to verify they are detected.
- A text file with mixed case words to test case insensitivity.
- A text file with punctuation to ensure it is properly removed during normalization.

**Issues you discovered during testing:**
[Any bugs or problems you found and fixed during development]
During testing, I discovered that my program was not correctly normalizing words with punctuation attached, such as "word," or "word.".

## HashSet vs List Understanding

**When to use HashSet:**
[Explain when you would choose HashSet over List based on your experience]
You should choose a hashset over a list anytime you need uniqueness or to combine datasets in special ways. 

**When to use List:**
[Explain when List is more appropriate than HashSet]
You should use a list when duplicates are allowed, you need to maintain aspecific order of elements, or you need to frequently acces elemnents by their index.

**Performance benefits observed:**
[Describe how O(1) lookups and automatic uniqueness helped your implementation]
It saved on memory immensely by never having to loop through the whole set.

## Real-World Applications

**How this relates to actual spell checkers:**
[Describe how your implementation connects to tools like Microsoft Word, Google Docs, etc.]
My implementation is similar to real-world spell checkers in that it uses a dictionary of known words to identify misspellings in a given text. 
Just like in Microsoft Word or Google Docs, the program checks each word against the dictionary and flags any words that are not found.

**What you learned about text processing:**
[What insights did you gain about handling real-world text data and normalization?]
I learned that real-world text data can be messy, with variations in case, punctuation, and formatting. 
Normalization is essential to ensure that words are compared accurately, regardless of how they appear in the text. 
This includes converting all words to lowercase and removing any extraneous characters.

## Stretch Features

[If you implemented any extra credit features like vocabulary suggestions or advanced analytics, describe them here. If not, write "None implemented"]

## Time Spent

**Total time:** [4 hours]

**Breakdown:**
- Understanding HashSet concepts and assignment requirements: [0.5 hours]
- Implementing the 6 core methods: [2 hours]
- Testing different text files and scenarios: [0.5 hours]
- Debugging and fixing issues: [0.5 hours]
- Writing these notes: [0.5 hours]

**Most time-consuming part:** [Which aspect took the longest and why - text normalization, HashSet operations, file I/O, etc.]
the most time consuming part was getting TODO 5 & 6 to work correctly. 
There was in issue in my code where the HasAnalyzedText boolean was not being set to true after the AnalyzeText method was called, which caused both methods to always return empty sets.

## Key Learning Outcomes

**HashSet concepts learned:**
[What did you learn about O(1) performance, automatic uniqueness, and set-based operations?]
I learned that HashSets are very efficient for lookups and that they automatically handle duplicates for you, which simplifies code when you need to ensure uniqueness. 
Set-based operations like unions and intersections are also very useful for comparing datasets.
I also learned that you can directly feed a hashset to a list constructor to quickly convert between the two data structures.

**Text processing insights:**
[What did you learn about normalization, tokenization, and handling real-world text data?]
I learned that text normalization is crucial for accurate spell checking, as it ensures that variations in case and punctuation do not affect word recognition. 
Tokenization is also important for breaking down text into manageable pieces for analysis.

**Software engineering practices:**
[What did you learn about error handling, user interfaces, and defensive programming?]
I learned just how important it is to check for small errors that can cascade into larger issues later on. Defensive programming is key to ensuring that your code can handle unexpected inputs or states without crashing.
Missing a ';' or a small typo can lead to hours of debugging if not caught early.