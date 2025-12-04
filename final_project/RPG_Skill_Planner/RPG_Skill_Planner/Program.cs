namespace RPG_Skill_Planner
{
    class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        static void Main()
        {
            // Display application header
            Console.WriteLine("RPG Skill Planner - Final Project");
            Console.WriteLine("=============================================");
            Console.WriteLine();

            try
            {
                // Initialize the skill system and navigator
                var skillSystem = new SkillSystem();
                var navigator = new SkillSystemNavigator(skillSystem);

                // Start the interactive skill system navigator
                navigator.Run();
            }
            catch (Exception ex)
            {
                // Handle any unexpected exceptions
                Console.WriteLine($"\nFatal Application Error: {ex.Message}");
                Console.WriteLine("\nDebug Information:");
                Console.WriteLine($"   Error Type: {ex.GetType().Name}");
                Console.WriteLine($"   Stack Trace: {ex.StackTrace}");

                Console.WriteLine("\nPress any key to exit...");
                Console.ReadKey();
            }
        }
    }
}