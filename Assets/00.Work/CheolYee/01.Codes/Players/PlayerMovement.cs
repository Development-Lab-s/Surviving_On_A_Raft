using _00.Work.CheolYee._01.Codes.Agents;
using _00.Work.CheolYee._01.Codes.SO;

namespace _00.Work.CheolYee._01.Codes.Players
{
    public class PlayerMovement : AgentMovement //플레이어의 모든 이동을 담당
    {
        public void Initialize(CharacterDataSo characterData) //캐릭터 무브먼트 초기값 설정
        {
            MoveSpeed = characterData.moveSpeed;
            JumpForce = characterData.jumpForce;
        }
    }
}