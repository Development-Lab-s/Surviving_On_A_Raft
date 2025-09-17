using _00.Work.CheolYee._01.Codes.Core.Buffs;
using _00.Work.CheolYee._01.Codes.Managers;
using _00.Work.CheolYee._01.Codes.Players;
using UnityEngine.Events;

namespace _00.Work.CheolYee._01.Codes.Agents.Healths
{
    public class EnemyHealth : AgentHealth, IBuffable
    {
        private Player _player;

        private void Start()
        {
            _player = GameManager.Instance.playerTransform.GetComponent<Player>();
            StatManager.Instance.OnEnemyBuff += ApplyBuff;
            StatManager.Instance.OnResetEnemyBuff += ResetBuff;
        }

        public void ApplyBuff(StatType stat, float buff)
        {
            if (stat == StatType.Health) AddMultiplier("Event", StatManager.Instance.GetEnemyBuff(StatType.Health));
        }

        public void ResetBuff(StatType statType, float buff)
        {
            if (statType == StatType.Health) RemoveMultiplier("Event");
        }
    }
}