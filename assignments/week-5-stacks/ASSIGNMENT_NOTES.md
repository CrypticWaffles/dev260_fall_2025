# Assignment 5: Browser Navigation System - Implementation Notes

**Name:** Miles Griffith

## Dual-Stack Pattern Understanding

**How the dual-stack pattern works for browser navigation:**
The dual-stack pattern uses two stacks to manage the navigation history of a browser. 
The "back" stack keeps track of the pages the user has visited, allowing them to go back to previous pages.
When the user navigates to a new page, the current page is pushed onto the back stack. 
The "forward" stack, on the other hand, stores pages that the user has navigated away from using the back button. 
When the user clicks the back button, the current page is pushed onto the forward stack, and the top page from the back stack is popped and displayed. 
If the user then clicks the forward button, the current page is pushed onto the back stack, and the top page from the forward stack is popped and displayed. 
This lets users easily navigate back and forth through their browsing history, similar to an undo and redo function.

## Challenges and Solutions

**Biggest challenge faced:**
My biggest issue when doing this assignemnt was understandng exactly what each method was supposed to do and how they interacted with each other.

**How you solved it:**
At first i was looking in the BrowserNavigator class to find what i was supposed to be doing, I was thinking that for this one maybe id have to make each of them from scratch with no predefined methods.
Then I explored all of the files and realized that all the methods were already defined in the BrowserSession Class.

**Most confusing concept:**
Nothing here really confused me, I just had to take some time to understand what was going on.

## Code Quality

**What you're most proud of in your implementation:**
I am most proud of the fact that I was able to implement all 6 methods correctly and have them all pass the tests on the first try.

**What you would improve if you had more time:**
if i had the time i'd try and merge the goBack and goForward methods into one method that takes in a parameter to determine which stack to use, but I understand that this is not the point of the assignment.

## Testing Approach

**How you tested your implementation:**
Once i implemented each method i ran the program and provided it variosu inputs top make sure they all worked. 

**Issues you discovered during testing:**
I forgot a semicolon in one of the methods but other than that everything worked fine.

## Stretch Features

None implemented

## Time Spent

**Total time:** 1 hour 32 minutes

**Breakdown:**

- Understanding the assignment: 10 minutes
- Implementing the 6 methods: 47 minutes
- Testing and debugging: 20 minutes
- Writing these notes: 15 minutes

**Most time-consuming part:** Implementing the 6 methods, as I had to make sure I understood what each one was supposed to do.
