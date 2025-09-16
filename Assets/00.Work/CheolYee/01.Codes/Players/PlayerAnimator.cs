using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Players
{
    public class PlayerAnimator : MonoBehaviour
    {
        private static readonly int Move = Animator.StringToHash("MOVE");
        private static readonly int Jump = Animator.StringToHash("JUMP");
        private static readonly int Death = Animator.StringToHash("DEATH");
        
        [SerializeField] private GameObject player;
        public Animator AnimatorComponent { get; private set; }

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
            AnimatorComponent.SetBool(Death, dead);
        }

        private void Flip(bool faceRight)
        {
            _isFacingRight = faceRight;
            player.transform.rotation = faceRight ? Quaternion.identity : Quaternion.Euler(0, 180f, 0);
        }
    }
}