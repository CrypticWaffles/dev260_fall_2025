# Assignment 9: BST File System Navigator - Implementation Notes

**Name:** Miles Griffith

## Binary Search Tree Pattern Understanding

**How BST operations work for file system navigation:**
[Explain your understanding of how O(log n) searches, automatic sorting through in-order traversal, and hierarchical file organization work together for efficient file management]

Answer:
In a Binary Search Tree, each node represents a file or directory, and the tree is organized such that for any given node, all nodes in the left subtree are "less than" the node, and all nodes in the right subtree are "greater than" the node based on a defined comparison logic. 
This structure allows for efficient O(log n) search times because at each step of the search, half of the remaining nodes can be eliminated from consideration.

## Challenges and Solutions

**Biggest challenge faced:**
[Describe the most difficult part of the assignment - was it recursive tree algorithms, custom file/directory comparison logic, or complex BST deletion?]

Answer:
The biggest challenge I faced was implementing the recursive tree algorithms correctly, especially ensuring that the base cases were properly defined to avoid infinite recursion.
As i waa working on one of the recursive methods, I initially forgot to include a base case for when the current node was null, which led to a stack overflow error.

**How you solved it:**
[Explain your solution approach and what helped you figure it out - research, debugging, testing strategies, etc.]

Answer:
I added print statements to trace the flow of the recursion and identify where it was going wrong. 
I also reviewed my base cases and ensured that each recursive call was moving closer to those bases. 
Additionally, I consulted online resources and examples of recursive tree algorithms to reinforce my understanding.

**Most confusing concept:**
[What was hardest to understand about BST operations, recursive thinking, or file system hierarchies?]

Answer:
The most confusing concept for me was ensuring that my recursive functions had the correct base cases and that each recursive call was progressing towards those base cases. 
It was challenging to visualize how the recursion would unfold, especially in more complex operations like deletion.

## Code Quality

**What you're most proud of in your implementation:**
[Highlight the best aspect of your code - maybe your recursive algorithms, custom comparison logic, or efficient tree traversal]

Answer:
I am most proud of my recursive implementations for the BST operations. I ensured that each method was cleanly written and wouldn't cause my program to enter infinite loops.

**What you would improve if you had more time:**
[Identify areas for potential improvement - perhaps better error handling, more efficient algorithms, or additional features]

Answer:
I feel that my implementation is solid, but if I had more time, I would focus on enhancing error handling to cover more edge cases and possibly implement additional features like file pattern matching or directory size analysis.

## Real-World Applications

**How this relates to actual file systems:**
[Describe how your implementation connects to tools like Windows File Explorer, macOS Finder, database indexing, etc.]

Answer:
This implementation mirrors how real-world file systems manage and organize files.
File explorers like Windows File Explorer and macOS Finder use similar tree structures to allow users to navigate through directories and files efficiently.
I could use what i've learnt here to build a more complex file management system or even a database indexing system that requires efficient search and organization of data.

**What you learned about tree algorithms:**
[What insights did you gain about recursive thinking, tree traversal, and hierarchical data organization?]

Answer:
Incorrectly implemented recursion can trap your programs into endless loops. 
It's important to have clear base cases and ensure that each recursive call progresses towards those bases. 
Testing with various scenarios helped me solidify my understanding of recursion in tree structures.

## Stretch Features

[If you implemented any extra credit features like file pattern matching or directory size analysis, describe them here. If not, write "None implemented"]

Answer:
none implemented

## Time Spent

**Total time:** [4.5 hours]

**Breakdown:**

- Understanding BST concepts and assignment requirements: [1 hours]
- Implementing the 8 core TODO methods: [1.5 hours]
- Testing with different file scenarios: [0.75 hours]
- Debugging recursive algorithms and BST operations: [0.5 hours]
- Writing these notes: [0.75 hours]

**Most time-consuming part:** [Which aspect took the longest and why - recursive thinking, BST deletion, custom comparison logic, etc.]
Implementing each of the main methods took the longest because I had to ensure that each recursive function worked correctly and maintained the BST properties. 
I then had to test them thoroughly to ensure they handled all edge cases.
