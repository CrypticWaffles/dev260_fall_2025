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

**How you solved it:**
[Explain your solution approach and what helped you figure it out]

**Most confusing concept:**
[What was hardest to understand about HashSet operations, text processing, or case-insensitive comparisons?]

## Code Quality

**What you're most proud of in your implementation:**
[Highlight the best aspect of your code - maybe your normalization strategy, error handling, or efficient text analysis]

**What you would improve if you had more time:**
[Identify areas for potential improvement - perhaps better tokenization, more robust error handling, or additional features]

## Testing Approach

**How you tested your implementation:**
[Describe your overall testing strategy - how did you verify spell checking worked correctly?]

**Test scenarios you used:**
[List specific scenarios you tested, like mixed case words, punctuation handling, edge cases, etc.]

**Issues you discovered during testing:**
[Any bugs or problems you found and fixed during development]

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

**What you learned about text processing:**
[What insights did you gain about handling real-world text data and normalization?]

## Stretch Features

[If you implemented any extra credit features like vocabulary suggestions or advanced analytics, describe them here. If not, write "None implemented"]

## Time Spent

**Total time:** [X hours]
5:30 start time

**Breakdown:**
- Understanding HashSet concepts and assignment requirements: [0.5 hours]
- Implementing the 6 core methods: [X hours]
- Testing different text files and scenarios: [X hours]
- Debugging and fixing issues: [X hours]
- Writing these notes: [X hours]

**Most time-consuming part:** [Which aspect took the longest and why - text normalization, HashSet operations, file I/O, etc.]

## Key Learning Outcomes

**HashSet concepts learned:**
[What did you learn about O(1) performance, automatic uniqueness, and set-based operations?]

**Text processing insights:**
[What did you learn about normalization, tokenization, and handling real-world text data?]

**Software engineering practices:**
[What did you learn about error handling, user interfaces, and defensive programming?]