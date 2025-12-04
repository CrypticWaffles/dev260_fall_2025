# RPG Skill Planner

> A program to allow players to plan their character builds via a skill tree.

---

## What I Built (Overview)

**Problem this solves:**
In complex RPGs, players often struggle to plan their character builds because they don't know what prerequisites are needed for high-level abilities. This tool allows a user to map out skills, define dependencies, and simulate the unlocking process to ensure a build is valid.

**Core features:**
- **Create Skills:** Define skills with names, descriptions, and costs.
- **Link Dependencies:** Set rules (e.g., "Fireball" requires "Mana Flow").
- **Simulate Unlocking:** Try to unlock skills and get feedback if requirements aren't met.
- **Undo System:** Revert accidental unlocks using a history stack.

## How to Run

**Requirements:**
- .NET 8.0 SDK or later
- Windows, Mac, or Linux

```bash
git clone <your-repo-url>
cd <your-folder>
dotnet buildn
```

**Sample data:**  
You can load sample data by typing 7 or load in the main menu. This populates the system with a Warrior and Mage skill tree for testing.

---

# Using the App (Quick Start)
## Typical workflow:
1. Add Skills: Create the skills you want in your game.
2. Add Dependencies: specificy which skills unlock others.
3. Unlock: Attempt to unlock skills. The system will stop you if you haven't unlocked the parent skill yet.
4. Undo: If you make a mistake, use the Undo option to step back.

## Input tips:
- Input is not case-sensitive ("fireball" finds "Fireball").
- Costs default to 1 point if you enter invalid numbers.

---

## Data Structures (Brief Summary)
- Data structures used:Dictionary<string, Skill> → Powers the "Master List" for instant $O(1)$ lookups by name.
- List<Skill> → Stores the specific dependencies for each skill.
- Stack<string> → Powers the "Undo" feature, tracking history in Last-In-First-Out order.

---

## Manual Testing Summary
**Test scenarios:**  
**Scenario 1: Dependency Enforcement**
**Steps:** Load sample data. Try to unlock "Meteor Swarm" immediately.
**Expected result:** Error message stating "Requires: Fireball".
**Actual result:** System blocked the action and displayed the correct requirement.

**Scenario 2: Successful Unlock Chain**
**Steps:** Unlock "Mana Flow". Then unlock "Fireball". Then unlock "Meteor Swarm".
**Expected result:** All actions succeed.
**Actual result:** All skills unlocked successfully.

**Scenario 3: Undo Logic**
**Steps:** Unlock "Mana Flow". Select "Undo". Try to unlock "Fireball".
**Expected result:** "Mana Flow" becomes locked. "Fireball" fails because "Mana Flow" is locked again.
**Actual result:** Point refunded, skill re-locked, dependency check worked correctly.

---

## Known Limitations

**Limitations and edge cases:**  
- Circular Dependencies: The system prevents simple loops (A->A), but complex loops (A->B->C->A) might need more robust graph cycle detection.
- Persistence: Data is lost when the application closes (no database or file save).

## Comparers & String Handling
- Keys comparer: I used StringComparer.OrdinalIgnoreCase. This ensures that users don't have to remember if they capitalized "Fireball" or not.
- Normalization: Inputs are .Trim()ed to remove accidental spaces.

---

## Credits & AI Disclosure

**Resources:**  
- Microsoft .NET Documentation (Collections)
- C# Stack Class Documentation

- **AI usage:**  
AI was used to brainstorm ideas for the project as well as to help debug and clean up code. I verified it by running the code and looking for errors.

## Challenges and Solutions
**Biggest challenge faced:** The hardest part was visualizing how the objects linked together. Initially, I just stored the names of dependencies, but then I realized I had to search the dictionary every time I wanted to check a requirement.

**How you solved it:** I refactored the Skill class to hold a List<Skill> (references to the actual objects). This made checking requirements much faster, though I had to be careful to ensure the referenced objects actually existed before linking them.

**Most confusing concept:** Understanding the difference between List and LinkedList. I initially thought I needed LinkedList because I was building a "chain" of skills, but I learned that is usually better for general collections.

## Code Quality
**What you're most proud of in your implementation:** I am proud of the Undo feature using the Stack. It was a simple addition, but it added a very professional "feel" to the application and demonstrated a perfect use case for that data structure.

**What you would improve if you had more time:** I would add a "Save/Load" feature using JSON serialization so users can save their skill trees to a file and load them later.

## Real-World Applications
**How this relates to real-world systems:** This logic is exactly how package managers (like NuGet or npm) work. When you install a library, the system checks its dependencies tree to ensure you have all the required lower-level packages installed first.

**What you learned about data structures and algorithms:** I learned that choosing the right key for a Dictionary is crucial. By choosing the name as the key and ignoring case, I saved myself from writing dozens of lines of string comparison logic throughout the app.

## Submission Checklist

- [X] Public GitHub repository link submitted
- [X] README.md completed (this file)
- [X] DESIGN.md completed
- [X] Source code included and builds successfully
