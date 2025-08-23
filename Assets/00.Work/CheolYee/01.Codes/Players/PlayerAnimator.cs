using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Players
{
    public class PlayerAnimator : MonoBehaviour
    {
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

        private void Flip(bool faceRight)
        {
            _isFacingRight = faceRight;
            transform.rotation = faceRight ? Quaternion.identity : Quaternion.Euler(0, 180f, 0);
        }
    }
}