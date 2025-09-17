using _00.Work.CheolYee._01.Codes.Managers;
using _00.Work.Hedonism._06.Scripts.SO.Manager;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Players
{
    public class PlayerAnimator : MonoBehaviour
    {
        private static readonly int Move = Animator.StringToHash("MOVE");
        private static readonly int Jump = Animator.StringToHash("JUMP");
        private static readonly int Death = Animator.StringToHash("DEATH");
        
        [SerializeField] private GameObject player;
        public Animator AnimatorComponent { get; set; }

        private bool _isFacingRight;
        private void Awake()
        {
            AnimatorComponent = GetComponent<Animator>();
            _isFacingRight = Mathf.Approximately(transform.rotation.eulerAngles.y, 0f);
        }
        
        public void HandleFlip(float moveX)
        {
            if (moveX > 0.01f && !_isFacingRight)
            {
                Flip(true);
            }
            else if (moveX < -0.01f && _isFacingRight)
            {
                Flip(false);
            }
        }

        public void MovePlayer(float moveX)
        {
            AnimatorComponent.SetFloat(Move, moveX);
        }

        public void SetJump(bool jump)
        {
            AnimatorComponent.SetBool(Jump, jump);
        }
        
        public void SetDead(bool dead)
        {
            AnimatorComponent.SetBool(Jump, false);
            SpawnManager.Instance.ClearAllEnemies();
            SpawnManager.Instance.DespawnCurrentPortals();
            AnimatorComponent.SetBool(Death, dead);
        }

        public void RealDeath()
        {
            if (GameManager.Instance.isThunami)
            {
                DeadScene.Instance.ActiveDeadScene(DeathReasonEnum.watarDie);
            }
            else
            {
                DeadScene.Instance.ActiveDeadScene(DeathReasonEnum.enemyDie);
            }
            
        }

        private void Flip(bool faceRight)
        {
            _isFacingRight = faceRight;
            transform.rotation = faceRight ? Quaternion.identity : Quaternion.Euler(0, 180f, 0);
        }
    }
}