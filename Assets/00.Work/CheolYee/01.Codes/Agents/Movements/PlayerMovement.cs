using _00.Work.CheolYee._01.Codes.Core.Buffs;
using _00.Work.CheolYee._01.Codes.Managers;
using _00.Work.CheolYee._01.Codes.SO;

namespace _00.Work.CheolYee._01.Codes.Agents.Movements
{
    public class PlayerMovement : AgentMovement, IBuffable //플레이어의 모든 이동을 담당
    {
        private void Start()
        {
            StatManager.Instance.OnPlayerBuff += ApplyBuff;
            StatManager.Instance.OnResetPlayerBuff += ResetBuff;
        }
        public void ApplyBuff(StatType stat, float buff)
        {
            if (stat == StatType.MoveSpeed) SpeedMultiplier += buff;
        }

        public void ResetBuff(StatType statType, float buff)
        {
            if (statType == StatType.MoveSpeed) SpeedMultiplier -= buff; 
        }
        
        public void Initialize(CharacterDataSo characterData) //캐릭터 무브먼트 초기값 설정
        {
            MoveSpeed = characterData.moveSpeed;
            JumpForce = characterData.jumpForce;
        }
        
    }
}