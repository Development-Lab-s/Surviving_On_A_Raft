using _00.Work.CheolYee._01.Codes.Core.Buffs;
using _00.Work.CheolYee._01.Codes.Managers;
using _00.Work.CheolYee._01.Codes.SO;

namespace _00.Work.CheolYee._01.Codes.Agents.Movements
{
    public class EnemyMovement : AgentMovement, IBuffable
    {
        private void Start()
        {
            SpeedMultiplier = StatManager.Instance.GetEnemyBuff(StatType.MoveSpeed);
            StatManager.Instance.OnEnemyBuff += ApplyBuff;
            StatManager.Instance.OnResetEnemyBuff += ResetBuff;
        }
        public void ApplyBuff(StatType stat, float buff)
        {
            if (stat == StatType.MoveSpeed) SpeedMultiplier = buff;
        }

        public void ResetBuff(StatType statType, float buff)
        {
            if (statType == StatType.MoveSpeed) SpeedMultiplier = 1f;
        }

        public void Initialize(EnemyDataSo data)
        {
            MoveSpeed = data.moveSpeed;
            JumpForce = data.jumpForce;
            KnockBackDuration = data.knockbackDuration;
        }
        
    }
}