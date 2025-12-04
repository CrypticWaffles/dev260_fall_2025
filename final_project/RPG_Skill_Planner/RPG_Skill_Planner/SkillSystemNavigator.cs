using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Skill_Planner
{
    /// <summary>
    /// A class that provides an interactive console-based navigator for the SkillSystem.
    /// </summary>
    public class SkillSystemNavigator
    {
        // Reference to the SkillSystem
        private readonly SkillSystem skillSystem;

        // State variable to track if the navigator is running
        private bool isRunning;

        // Constructor
        public SkillSystemNavigator(SkillSystem system)
        {
            this.skillSystem = system ?? throw new ArgumentNullException(nameof(system));
            this.isRunning = true;
        }

        /// <summary>
        /// Runs the interactive skill system navigator.
        /// </summary>
        public void Run()
        {
            Console.WriteLine("Skill System Navigator");
            Console.WriteLine("============================");
            Console.WriteLine("Welcome to the interactive skill system application!");
            Console.WriteLine();

            while (isRunning)
            {
                DisplayMainMenu();
                string choice = Console.ReadLine()?.ToLower() ?? "";

                Console.WriteLine($"You selected: {choice}");

                try
                {
                    ProcessCommand(choice);
                }
                catch (NotImplementedException ex)
                {
                    Console.WriteLine($"\nMethod not implemented yet: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n Error: {ex.Message}\n");
                }
            }
        }

        /// <summary>
        /// Processes a user command and performs the corresponding action.
        /// </summary>
        /// <remarks>If the command is unrecognized, an error message is displayed, and no action is
        /// performed.</remarks>
        /// <param name="command">The command string entered by the user. Valid commands include: <list type="bullet"> <item><description>"1",
        private void ProcessCommand(string input)
        {
            string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0) return;
            string command = parts[0].ToLower();
            string[] args = parts.Skip(1).ToArray();

            switch (command)
            {
                case "1":
                case "add skill":
                case "create":
                    HandleAddSkillCommand(args);
                    break;
                case "2":
                case "list skills":
                case "list":
                    HandleListSkillsCommand();
                    break;
                case "3":
                case "add dependancy":
                case "dependancy":
                    HandleAddDependancyCommand(args);
                    break;
                case "4":
                case "unlock skill":
                case "unlock":
                    HandleUnlockSkillCommand(args);
                    break;
                case "5":
                case "undo last unlock":
                case "undo":
                    HandleUndoLastUnlockCommand();
                    break;
                case "6":
                case "load":
                case "sample":
                    skillSystem.LoadSampleData();
                    break;
                case "7":
                case "exit":
                case "quit":
                    isRunning = false;
                    Console.WriteLine("Exiting Skill System Navigator. Goodbye!");
                    break;
                default:
                    Console.WriteLine("Invalid command. Please try again.");
                    break;
            }
        }

        /// <summary>
        /// Displays the main menu to the user.
        /// </summary>
        private void DisplayMainMenu()
        {
            //bool isEmpty = skillSystem.IsEmpty();
            var stats = skillSystem.GetStatistics();

            Console.WriteLine("┌─ Skill System Navigator ───────────────────────────────────┐");
            Console.WriteLine("│ 1. Add Skill    │ 2. List Skills      │ 3. Add Dependancy  │");
            Console.WriteLine("│ 4. Unlock Skill │ 5. Undo Last Unlock │ 6. Load Sample Data│");
            Console.WriteLine("│ 7. Exit         │                     │                    │");
            Console.WriteLine("└────────────────────────────────────────────────────────────┘");

            Console.WriteLine($"Status: {stats.TotalSkills} skills, {stats.UnlockedSkills} unlocked skills");
            Console.WriteLine($"Operations: {stats.OperationsPerformed}");
            Console.WriteLine();

            Console.WriteLine("💡 Tip: Use numbers (1-6) or keywords (create, find, etc.)");
            Console.Write("Enter your choice: ");
        }

        /// <summary>
        /// Adds a new skill to the skill system.
        /// </summary>
        /// <param name="args"></param>
        private void HandleAddSkillCommand(string[] args)
        {
            // If user didn't type args, ask for them interactively
            string name = args.Length > 0 ? args[0] : PromptUser("Enter Skill Name: ");
            string desc = args.Length > 1 ? args[1] : PromptUser("Enter Description: ");

            string costStr = args.Length > 2 ? args[2] : PromptUser("Enter Point Cost: ");
            if (!int.TryParse(costStr, out int cost))
            {
                Console.WriteLine("Invalid cost. Defaulting to 1.");
                cost = 1;
            }

            try
            {
                skillSystem.AddSkill(name, desc, cost);
                Console.WriteLine($"Successfully added skill: {name}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding skill: {ex.Message}");
            }
        }

        /// <summary>
        /// Handles the List Skills command.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void HandleListSkillsCommand()
        {
            var skills = skillSystem.GetAllSkills();

            Console.WriteLine("\n--- Current Skill Tree ---");
            if (!skills.Any())
            {
                Console.WriteLine("No skills defined yet.");
                return;
            }

            foreach (var skill in skills)
            {
                string status = skill.IsUnlocked ? "[UNLOCKED]" : "[LOCKED]";
                // Show dependencies if any exist
                string reqs = skill.Dependencies.Count > 0
                    ? $" (Requires: {string.Join(", ", skill.Dependencies.Select(d => d.Name))})"
                    : "";

                Console.WriteLine($"{status} {skill.Name} - {skill.Description} [Cost: {skill.PointCost}]{reqs}");
            }
            Console.WriteLine("--------------------------\n");
        }

        /// <summary>
        /// Handles the Add Dependancy command.
        /// </summary>
        /// <param name="args"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void HandleAddDependancyCommand(string[] args)
        {
            // Usage: dependancy [Target] [Required]
            string dependent = args.Length > 0 ? args[0] : PromptUser("Enter the skill that NEEDS a prerequisite: ");
            string prerequisite = args.Length > 1 ? args[1] : PromptUser("Enter the PREREQUISITE skill: ");

            try
            {
                skillSystem.AddDependency(dependent, prerequisite);
                Console.WriteLine($"Linked: '{dependent}' now requires '{prerequisite}'");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error linking skills: {ex.Message}");
            }
        }

        /// <summary>
        /// Handles the Unlock Skill command.
        /// </summary>
        /// <param name="args"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void HandleUnlockSkillCommand(string[] args)
        {
            string name = args.Length > 0 ? args[0] : PromptUser("Enter skill to unlock: ");

            try
            {
                skillSystem.UnlockSkill(name);
                Console.WriteLine($"UNLOCKED: {name}!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed: {ex.Message}");
            }
        }

        /// <summary>
        /// Handles the Undo Last Unlock command.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void HandleUndoLastUnlockCommand()
        {
            string result = skillSystem.UndoLastUnlock();
            Console.WriteLine(result);
        }

        // Helper for cleaner code
        private string PromptUser(string message)
        {
            Console.Write(message);
            return Console.ReadLine() ?? "";
        }
    }
}
