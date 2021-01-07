using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shizui
{
    public class Skill
    {
        public int id;
        public string name;
        public int MPCost;
        public int HPCost;
        public int CPCost;
        public int reuseTime;
        public string targetType;
        public string affectScope;
        public bool IsEnabled = true; // pueden haber skills del player desactivados para su  uso
        public int minConditionValue;
        public int maxConditionValue;

        public enum Condition
        {
            None,
            TARGET_DEAD,
            TARGET_FULL_HP,
            TARGET_HP_LESS_THAN = 0,
            TARGET_HP_GREATER_THAN = 0,
            PLAYER_CP_LESS_THAN = 0,
            PLAYER_CP_GREATER_THAN = 0,
            PLAYER_HP_LESS_THAN = 0,
            PLAYER_HP_GREATER_THAN = 0,
            PLAYER_MP_LESS_THAN = 0,
            PLAYER_MP_GREATER_THAN = 0
        }
        Condition condition = Condition.None;

        public Skill(int id, string name, string targetType, int reuseTime = 3000)
        {
            this.id = id;
            this.name = name;
            this.reuseTime = reuseTime;
            this.targetType = targetType;
        }

        public Skill()
        {

        }

        public Condition GetUsageCondition()
        {
            return condition;
        }

        public void SetUsageCondition(Condition condition)
        {
            this.condition = condition;
        }

    }
}
