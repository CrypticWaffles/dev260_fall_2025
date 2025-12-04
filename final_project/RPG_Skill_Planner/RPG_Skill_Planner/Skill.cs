using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Skill_Planner
{
    /// <summary>
    /// Skill class representing an individual skill in the RPG skill system.
    /// </summary>
    public class Skill
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int PointCost { get; set; }
        public bool IsUnlocked { get; set; }

        // List of prerequisite skills
        public List<Skill> Dependencies { get; set; } = new List<Skill>();

        // Constructor
        public Skill(string name, string desc, int cost)
        {
            Name = name;
            Description = desc;
            PointCost = cost;
            IsUnlocked = false;
        }
    }
}
