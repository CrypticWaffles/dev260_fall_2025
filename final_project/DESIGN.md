## Data Model & Entities

**Core entities:**

**Entity A:**

- Name: Skill
- Key fields: Name (string), Description (string), PointCost (int), IsUnlocked (bool), Dependencies (List<Skill>)
- Identifiers: Name (Unique string)
- Relationships: A Skill can have many Dependencies (other Skills). This creates a directed graph/tree structure.

**Entity B (if applicable):** N/A

**Identifiers (keys) and why they're chosen:**
I chose the **Skill Name** as the unique identifier. Since this is a user-facing console app, it is easier for a user to type "Fireball" than it is to remember a numeric ID like "104". I enforced case-insensitivity so "fireball" and "Fireball" are treated as the same key.

---

## Data Structures — Choices & Justification

### Structure #1

**Chosen Data Structure:**
`Dictionary<string, Skill>`

**Purpose / Role in App:**
This acts as the "Master Catalog" or database for the application. It stores every skill created in the system.

**Why it fits:**
The primary user interaction is typing a name to find a skill (to unlock it, view it, or link it).
- **Performance:** Dictionaries provide $O(1)$ (constant time) lookups.
- **Why not a List?** As the skill tree grows to 100+ items, iterating through a list ($O(n)$) to find "Zeus Bolt" would be inefficient.

**Alternatives considered:**
`List<Skill>`: I considered this for simplicity, but rejected it because searching requires looping through the entire collection every time a command is entered.

---

### Structure #2

**Chosen Data Structure:**
`List<Skill>` (Nested inside the `Skill` class)

**Purpose / Role in App:**
This holds the **Dependencies** for a specific skill. For example, "Fireball" might have a list containing the "Mana Flow" object.

**Why it fits:**
- **Size:** Most skills only have 1 or 2 prerequisites. A `List` is very lightweight for small collections.
- **Flexibility:** It is easy to iterate over to check `IsUnlocked` status for all parents.

**Alternatives considered:**
`Array`: Rejected because I don't know how many dependencies a skill might have when I create it. Lists resize dynamically, and arrays do not.

---

### Structure #3

**Chosen Data Structure:**
`Stack<string>`

**Purpose / Role in App:**
This powers the **Undo** feature. It tracks the history of unlocked skills.

**Why it fits:**
- **LIFO (Last-In, First-Out):** The "Undo" button needs to revert the *most recent* action. A Stack is mathematically designed for this.
- **Simplicity:** `Push()` and `Pop()` map perfectly to "Do" and "Undo."

**Alternatives considered:**
`Queue<string>`: Rejected because a Queue is FIFO (First-In, First-Out). If I used a Queue, hitting "Undo" would re-lock the *first* skill I ever bought, which is not how undo should work.

---

### Additional Structures (if applicable)

N/A

---

## Comparers & String Handling

**Comparer choices:**
I used `StringComparer.OrdinalIgnoreCase` in the Dictionary constructor.

**For keys:**
This allows the dictionary to treat "Fireball", "fireball", and "FIREBALL" as the same key, preventing duplicate entries with different casing.

**For display sorting (if different):**
Display is currently unsorted (insertion order), but could easily be sorted alphabetically using LINQ.

**Normalization rules:**
I use `.Trim()` on user input to remove accidental leading/trailing spaces.

**Bad key examples avoided:**
I avoided using `int` IDs (e.g., 1, 2, 3) because it forces the user to memorize numbers instead of meaningful names.

---

## Performance Considerations

**Expected data scale:**
A typical RPG skill tree has between 50 and 500 skills.

**Performance bottlenecks identified:**
The current implementation is very fast. The only potential bottleneck is if a skill had thousands of dependencies, the `List` iteration inside `UnlockSkill` might slow down, but that is unrealistic for this domain.

**Big-O analysis of core operations:**

- Add: $O(1)$ (Amortized, thanks to Dictionary)
- Search: $O(1)$
- List: $O(n)$ (Must iterate all items to print)
- Update (Add Dependency): $O(1)$ (Lookup is constant, List Add is constant)
- Delete (Undo): $O(1)$ (Stack Pop is constant)

---

## Design Tradeoffs & Decisions

**Key design decisions:**
I decided to store references to `Skill` objects inside the `Dependencies` list rather than just storing the string names.

**Tradeoffs made:**
- **Pro:** Performance. I can check `dependency.IsUnlocked` instantly without looking it up in the Dictionary again.
- **Con:** Complexity. I have to be careful about circular references (A requires B, B requires A), which required adding a validation check in the `AddDependency` method.

**What you would do differently with more time:**
I would implement a full `Graph` class to handle more complex relationships, like "One-of-many" requirements (e.g., "Unlock EITHER Fire OR Ice to get this skill").