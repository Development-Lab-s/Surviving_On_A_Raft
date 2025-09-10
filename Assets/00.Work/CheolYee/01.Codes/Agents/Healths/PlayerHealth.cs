using _00.Work.CheolYee._01.Codes.Core.Buffs;
using _00.Work.CheolYee._01.Codes.Managers;

namespace _00.Work.CheolYee._01.Codes.Agents.Healths
{
    public class PlayerHealth : AgentHealth, IBuffable
    {
        
        private void Start()
        {
            StatManager.Instance.OnPlayerBuff += ApplyBuff;
            StatManager.Instance.OnResetPlayerBuff += ResetBuff;
        }
        
        public void ApplyBuff(StatType stat, float buff)
        {
            if (stat == StatType.Health) HealthMulti += buff;
        }

        public void ResetBuff(StatType statType, float buff)
        {
            if (statType == StatType.Health) HealthMulti -= buff;
        }
    }
}