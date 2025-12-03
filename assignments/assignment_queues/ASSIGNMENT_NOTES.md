# Assignment 6: Game Matchmaking System - Implementation Notes

**Name:** Miles Griffith

## Multi-Queue Pattern Understanding

**How the multi-queue pattern works for game matchmaking:**
[Explain your understanding of how the three different queues (Casual, Ranked, QuickPlay) work together and why each has different matching strategies]
A casual match is a low stakes game mode. This is when you just want to play the game and have fun. Skill level is irrelevant, all that matters is getting two players into a match.
A ranked match is important, it affects your stats and rankings. You want to fight someone on a similar rank to you. Only players of a similar rank should be matched together.
Quickplay is a blend of the two, itf for when you want to play soon but would still prefer someone similar to your rank if possible. In this we'd prioritize players of similar skill being placed into a match together, but if that's unavailable the next available player will be chosen instead regardless of skill difference.

## Challenges and Solutions

**Biggest challenge faced:**
[Describe the most difficult part of the assignment - was it the skill-based matching, queue management, or match processing?]
My biggest challenge in building this projected was adding ranked match making. 
I was negligent and didnt notice the helper methods so i originally had looked up how to remove one specific item from a queue as well as how to use absolute value in c#.

**How you solved it:**
[Explain your solution approach and what helped you figure it out]
After reviewing the provided codebase again i noticed the presence of helper methods and re created my code to use them instead.

**Most confusing concept:**
[What was hardest to understand about queues, matchmaking algorithms, or game mode differences?]
For me the most difficult aspect to understand was wrapping my head around how matches were supposed to worked. 


## Code Quality

**What you're most proud of in your implementation:**
[Highlight the best aspect of your code - maybe your skill matching logic, queue status display, or error handling]
I like how i was able to remove duplicate code throughout my program. Originally my TryCreateMatch() was duplicated several times for each mode.

**What you would improve if you had more time:**
[Identify areas for potential improvement - perhaps better algorithms, more features, or cleaner code structure]
If i could add more id like to add a way to visualize matches. Like a little console animation showing players fighting for a moment.

## Testing Approach

**How you tested your implementation:**
[Describe your overall testing strategy - how did you verify skill-based matching worked correctly?]
Once a method was completed i ran the program and fed it varipus players. 
Next i used those players to queue into the different queues and see if they worked as expected.

**Test scenarios you used:**
[List specific scenarios you tested, like players with different skill levels, empty queues, etc.]
i gave players dratically different skill levels and queued them in ranked to see if it would ignore them and not create a match lkike it''s supposed to. 

**Issues you discovered during testing:**
[Any bugs or problems you found and fixed during development]
matches would be created no matter what i inputted.

## Game Mode Understanding

**Casual Mode matching strategy:**
This one was simple, so long as there were at least two players in queue those two players would be added to a match.
If there were more than two then the first two would be added to a match, and then the next two and so on.

**Ranked Mode matching strategy:**
[Explain how you implemented skill-based matching (±2 levels) for Ranked mode]
I used the provided CanMatchInRanked() method to loop through the queue and check if a player is eligible.
If they were i removed them from the queue

**QuickPlay Mode matching strategy:**
[Explain your approach to balancing speed vs. skill matching in QuickPlay mode]
It would first try and matchmake based on skill like in ranked, if it cant find a match it will instead default to the first two people in queue.

## Real-World Applications

**How this relates to actual game matchmaking:**
[Describe how your implementation connects to real games like League of Legends, Overwatch, etc.]
My implementation is a very simplified version of what you may find in some games out there. 
Those games manage much more information and statistics for players to manage ranks and skill level. 
They also have much more sophisticated algorithms to handle all of that information.

**What you learned about game industry patterns:**
[What insights did you gain about how online games handle player matching?]
There's a lot of variables that go into deciding how players shoudl be matched to one another in a game. And in those games they match way more than jst two players.

## Stretch Features

[If you implemented any extra credit features like team formation or advanced analytics, describe them here. If not, write "None implemented"]

## Time Spent

**Total time:** 6 hours 7 minutes

**Breakdown:**

- Understanding the assignment and queue concepts:  1 hour
- Implementing the 6 core methods: 3 hours
- Testing different game modes and scenarios: 1 hours
- Debugging and fixing issues: 47 minutes
- Writing these notes: 20 minutes

**Most time-consuming part:** 
Buolding the main methods took me the longest as i'm the type to understand things best when i actually start working on them.

## Key Learning Outcomes

**Queue concepts learned:**
[What did you learn about managing multiple queues and different processing strategies?]
Searching through a queue and removing a specifc item from it is very annoying. I would much rather have used a list in this case.

**Algorithm design insights:**
[What did you learn about designing matching algorithms and handling different requirements?]
It's best not to try and overcomplicate your algoriths by adding special features and to instead focus on the core function of each one.

**Software engineering practices:**
[What did you learn about error handling, user interfaces, and code organization?]
I learned a lot about how best to reduce dupliacte code. My code is a lot cleaner than it would have been otherwise.
