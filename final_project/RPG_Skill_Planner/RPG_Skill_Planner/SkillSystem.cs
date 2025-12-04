using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Skill_Planner
{
    public class SkillSystem
    {
        // Dictionary to hold skills by their names
        private static Dictionary<string, Skill> skillTree = new Dictionary<string, Skill>();

        // Stack to maintain history of unlocked skills for undo functionality
        private Stack<string> unlockHistory = new Stack<string>();

        private int operationCount;
        private DateTime sessionStart;

        /// <summary>
        /// Constructor for the SkillSystem class.
        /// </summary>
        public SkillSystem()
        {
            operationCount = 0;
            sessionStart = DateTime.Now;
            Console.WriteLine("Skill System initialized.");
        }

        // --- Core Operations ---

        /// <summary>
        /// Adds a new skill to the skill system.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="desc"></param>
        /// <param name="cost"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public void AddSkill(string name, string desc, int cost)
        {
            // Validate skill uniqueness
            if (skillTree.ContainsKey(name))
                throw new InvalidOperationException($"Skill '{name}' already exists.");  

            // Create and add the new skill
            var newSkill = new Skill(name, desc, cost);
            skillTree.Add(name, newSkill);
            operationCount++;
        }

        /// <summary>
        /// Adds a dependancy between two skills.
        /// </summary>
        /// <param name="dependentName"></param>
        /// <param name="prerequisiteName"></param>
        /// <exception cref="KeyNotFoundException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public void AddDependency(string dependentName, string prerequisiteName)
        {
            // Validations
            if (!skillTree.ContainsKey(dependentName))
                throw new KeyNotFoundException($"Skill '{dependentName}' not found.");
            if (!skillTree.ContainsKey(prerequisiteName))
                throw new KeyNotFoundException($"Prerequisite '{prerequisiteName}' not found.");

            // Retrieve skills
            Skill dependent = skillTree[dependentName];
            Skill prerequisite = skillTree[prerequisiteName];

            // Avoid circular dependency (A requires A)
            if (dependentName.Equals(prerequisiteName, StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("A skill cannot depend on itself.");

            // Add the "Tree" link
            dependent.Dependencies.Add(prerequisite);
            operationCount++;
        }

        /// <summary>
        /// Unlocks a skill if all prerequisites are met.
        /// </summary>
        /// <param name="name"></param>
        /// <exception cref="KeyNotFoundException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public void UnlockSkill(string name)
        {
            if (!skillTree.ContainsKey(name))
                throw new KeyNotFoundException($"Skill '{name}' not found.");

            Skill target = skillTree[name];

            if (target.IsUnlocked)
                throw new InvalidOperationException($"Skill '{name}' is already unlocked.");

            // Check Tree Logic: Are all prerequisites unlocked?
            foreach (var req in target.Dependencies)
            {
                if (!req.IsUnlocked)
                {
                    throw new InvalidOperationException($"Cannot unlock '{name}'. Requires: {req.Name}");
                }
            }

            // Success: Unlock and Push to Stack
            target.IsUnlocked = true;
            unlockHistory.Push(name);
            operationCount++;
        }

        public string UndoLastUnlock()
        {
            if (unlockHistory.Count == 0)
                return "Nothing to undo.";

            // Pop from Stack
            string lastUnlockedName = unlockHistory.Pop();

            if (skillTree.ContainsKey(lastUnlockedName))
            {
                skillTree[lastUnlockedName].IsUnlocked = false;
                operationCount++;
                return $"Undid unlock for: {lastUnlockedName}";
            }
            return "Error: Skill in history no longer exists.";
        }

        // --- Helpers and Statistics ---

        public IEnumerable<Skill> GetAllSkills()
        {
            return skillTree.Values;
        }

        /// <summary>
        /// Summarizes the current statistics of the skill system.
        /// </summary>
        /// <returns></returns>
        public SkillSystemStats GetStatistics()
        {
            var stats = new SkillSystemStats
            {
                TotalSkills = skillTree.Count,
                UnlockedSkills = skillTree.Values.Count(s => s.IsUnlocked),
                SessionDuration = DateTime.Now - sessionStart,
                OperationsPerformed = operationCount
            };

            return stats;
        }

        public void LoadSampleData()
        {
            Console.WriteLine("Loading sample data...");

            // Create Skills (Name, Description, Cost)
            AddSkill("Basic Combat", "Foundational fighting techniques", 1);
            AddSkill("Sword Mastery", "Increases melee damage", 2);
            AddSkill("Shield Bash", "Stuns enemies with a shield", 3);

            AddSkill("Mana Flow", "Unlocks the ability to use magic", 1);
            AddSkill("Fireball", "Launches a fiery projectile", 3);
            AddSkill("Meteor Swarm", "Summons a massive meteor", 10);

            //  Create Dependencies (Child, Parent)
            AddDependency("Sword Mastery", "Basic Combat");
            AddDependency("Shield Bash", "Basic Combat");

            AddDependency("Fireball", "Mana Flow");
            AddDependency("Meteor Swarm", "Fireball");

            Console.WriteLine("Sample data loaded!");
        }

    }
}
