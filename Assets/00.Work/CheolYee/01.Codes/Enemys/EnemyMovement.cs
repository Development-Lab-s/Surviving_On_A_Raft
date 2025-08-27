using _00.Work.CheolYee._01.Codes.Agents;
using _00.Work.CheolYee._01.Codes.SO;

namespace _00.Work.CheolYee._01.Codes.Enemys
{
    public class EnemyMovement : AgentMovement
    {
        public void Initialize(EnemyDataSo data)
        {
            MoveSpeed = data.moveSpeed;
            JumpForce = data.jumpForce;
            KnockBackDuration = data.knockbackDuration;
        }
    }
}