using System.Collections.Generic;
using _00.Work.CheolYee._01.Codes.Core.Buffs;
using _00.Work.CheolYee._01.Codes.Managers;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Agents.Healths
{
    public class EnemyHealth : AgentHealth, IBuffable
    {
        private void Start()
        {
            HealthMulti = StatManager.Instance.GetEnemyBuff(StatType.Health);
            StatManager.Instance.OnEnemyBuff += ApplyBuff;
            StatManager.Instance.OnResetEnemyBuff += ResetBuff;
        }
        
        public void ApplyBuff(StatType stat, float buff)
        {
            if (stat == StatType.Health) HealthMulti = buff;
        }

        public void ResetBuff(StatType statType)
        {
            if (statType == StatType.Health) HealthMulti = 1f;
        }
    }
}