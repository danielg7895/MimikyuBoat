using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shizui
{
    class Player
    {
        public int cp;
        public int hp = 0;
        public int mp;
        public int race;
        public int playerClass;
        public string nickName = "";

        // en que pixel comienza la barra
        public int cpBarStart;
        public int hpBarStart;
        public int mpBarStart;

        public int hpRow = -1;
        public int mpRow = -1;
        public int cpRow = -1;

        public string imagePath = "temp/player.jpeg";

        Target target;
        List<Skill> skills;

        #region singleton
        private static Player _instance;
        public static Player Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Player();
                }
                return _instance;
            }
        }
        #endregion

        public Player ()
        {
            Debug.WriteLine("INSTANCIA DE PLAYER!!!");
            skills = new List<Skill>();
        }
        public List<Skill> GetSkills()
        {
            return skills;
        }

        public void AddSkill(Skill skill)
        {
            skills.Add(skill);
        }

        public void RemoveSkill(Skill skill)
        {
            skills.Remove(skill);
        }

        public void SetTarget(Target target)
        {
            this.target = target;
            this.target.HPChanged += OnTargetHPChanged;
            this.target.Dead += OnTargetDead;
        }

        public void OnTargetHPChanged()
        {
            if (target == null || target.hp <= 0) return;
            foreach (Skill currentSkill in skills)
            {
                if (!currentSkill.IsEnabled) continue;

                int conditionValue = (int)currentSkill.GetUsageCondition();

                if (currentSkill.GetUsageCondition() == Skill.Condition.PLAYER_CP_GREATER_THAN)
                {
                    // Verifico skills que requieren cierta hp en el player para ser usados.
                    if (this.cp >= conditionValue)
                    {
                        Bot.Instance.UseSkill(currentSkill);
                    }
                }
                else if (currentSkill.GetUsageCondition() == Skill.Condition.PLAYER_CP_LESS_THAN)
                {
                    // Verifico skills que requieren cierta hp en el player para ser usados.
                    if (this.cp <= conditionValue)
                    {
                        Bot.Instance.UseSkill(currentSkill);
                    }
                }
                else if (currentSkill.GetUsageCondition() == Skill.Condition.PLAYER_HP_GREATER_THAN)
                {
                    // Verifico skills que requieren cierta hp en el player para ser usados.
                    if (this.hp >= conditionValue)
                    {
                        Bot.Instance.UseSkill(currentSkill);
                    }
                }
                else if (currentSkill.GetUsageCondition() == Skill.Condition.PLAYER_HP_LESS_THAN)
                {
                    // Verifico skills que requieren cierta hp en el player para ser usados.
                    if (this.hp <= conditionValue)
                    {
                        Bot.Instance.UseSkill(currentSkill);
                    }
                }
                else if (currentSkill.GetUsageCondition() == Skill.Condition.PLAYER_MP_GREATER_THAN)
                {
                    // Verifico skills que requieren cierta hp en el player para ser usados.
                    if (this.mp >= conditionValue)
                    {
                        Bot.Instance.UseSkill(currentSkill);
                    }
                }
                else if (currentSkill.GetUsageCondition() == Skill.Condition.PLAYER_MP_LESS_THAN)
                {
                    // Verifico skills que requieren cierta hp en el player para ser usados.
                    if (this.mp <= conditionValue)
                    {
                        Bot.Instance.UseSkill(currentSkill);
                    }
                }
                else if (currentSkill.GetUsageCondition() == Skill.Condition.TARGET_HP_GREATER_THAN)
                {
                    // Verifico skills que requieren cierta hp en el player para ser usados.
                    if (target.hp >= conditionValue)
                    {
                        Bot.Instance.UseSkill(currentSkill);
                    }
                }
                else if (currentSkill.GetUsageCondition() == Skill.Condition.TARGET_HP_LESS_THAN)
                {
                    // Verifico skills que requieren cierta hp en el player para ser usados.
                    if (target.hp <= conditionValue)
                    {
                        Bot.Instance.UseSkill(currentSkill);
                    }
                }
                else if (currentSkill.GetUsageCondition() == Skill.Condition.None)
                {
                    // ataque normal.
                }
            }
        }

        public void OnTargetDead()
        {
            if (target == null || target.hp >= 0) return;

            foreach (Skill currentSkill in skills)
            {
                if (!currentSkill.IsEnabled) continue;

                if (currentSkill.GetUsageCondition() == Skill.Condition.TARGET_DEAD)
                {
                    if (mp < currentSkill.MPCost)
                    {
                        // no tengo mp para usar el skill, que hago? no se, dios sabra.
                        Utils.Instance.ConsoleWrite("Intentando usar un skill del que no tengo mp...");
                        Bot.Instance.UseSkill(currentSkill);
                    }
                    else
                    {
                        Bot.Instance.UseSkill(currentSkill);
                    }
                }
            }
        }

        public void SetSkillEnabledState(string skillName, bool state)
        {
            // funcion encargada de loopear en todos los skills del player y setear un skill como activo o inactivo
            foreach(Skill skill in skills)
            {
                if (skill.name == skillName)
                {
                    skill.IsEnabled = state;
                    Debug.WriteLine("Skill ahora seteado como " + (skill.IsEnabled == true ? "true" : "false"));
                    break;
                }
            }
        }

    }

}
