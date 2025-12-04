using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Skill_Planner
{
    /// <summary>
    /// Statistics about skill system usage and operations
    /// Used for reporting and analytics
    /// </summary>
    public class SkillSystemStats
    {
        public int TotalSkills { get; set; }
        public int UnlockedSkills { get; set; }
        public int OperationsPerformed { get; set; }
        public TimeSpan SessionDuration { get; set; }

        public override string ToString()
        {
            return $"""
                📊 File System Statistics
                ========================
                Total Items: {TotalSkills:N0} ({UnlockedSkills:N0} unlocked Skills
                Operations Performed: {OperationsPerformed:N0}
                Session Duration: {SessionDuration:mm\\:ss}
                """;
        }
    }
}
