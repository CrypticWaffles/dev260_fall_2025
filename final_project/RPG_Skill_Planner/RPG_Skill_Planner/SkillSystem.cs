using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Skill_Planner
{
    public class SkillSystem
    {
        private static Dictionary<string, Skill> skillTree = new Dictionary<string, Skill>();
        private int operationCount;
        private DateTime sessionStart;

        public SkillSystem()
        {
            operationCount = 0;
            sessionStart = DateTime.Now;

            Console.WriteLine("Skill System initialized.");
        }

    }
}
