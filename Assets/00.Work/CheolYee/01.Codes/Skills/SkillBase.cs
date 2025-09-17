using System;
using _00.Work.CheolYee._01.Codes.SO;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Skills
{
    public abstract class SkillBase : MonoBehaviour
    {
        public PlayerSkillDataSo playerSkillData;
        protected float LastSkillCooldown;

        public bool CanUse => Time.time >= LastSkillCooldown + playerSkillData.skillCooldown;
        
        private bool _firstTime = true;

        public void TryUseSkill()
        {
            if (_firstTime)
            {
                _firstTime = false;
                Activate();
                LastSkillCooldown = Time.time;
            }
            
            if (CanUse)
            {
                Activate();
                LastSkillCooldown = Time.time;
            }
        }

        protected abstract void Activate();
    }
}