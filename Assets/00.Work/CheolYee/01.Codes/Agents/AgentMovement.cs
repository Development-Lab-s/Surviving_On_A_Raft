using _00.Work.CheolYee._01.Codes.Core;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace _00.Work.CheolYee._01.Codes.Agents
{
    public class AgentMovement : MonoBehaviour
    {
        protected float SpeedMultiplier = 1f;
        
        [Header("Motor Options")]
        [SerializeField] private bool motorControlsX = true; // 추가: X속도를 내부에서 제어할지

        [Header("References")]
        [field: SerializeField] public Rigidbody2D RbCompo { get; private set; } //다른곳에서 리지드바디를 가져오기 위함

        [Header("Settings")]
        public float CurrnetMoveSpeed => MoveSpeed * SpeedMultiplier;

        protected float MoveSpeed = 5f; //이동 속도
        protected float JumpForce = 7f; //점프력
        protected float KnockBackDuration = 0.2f; //넉백 시간
        [SerializeField] protected LayerMask groundLayer; //땅 레이어
        [SerializeField] protected Vector2 groundCheckRadius; //땅 체크 범위

        //NotifyValue: 값이 변경되었을 때, 이벤트를 발행해주는 기능이다. 이벤트처럼 OnValueChange에 구독하는 형식으로 사용 가능하다
        public readonly NotifyValue<bool> IsGround = new NotifyValue<bool>();

        private float _xMove; //x축 이동 저장
        public bool canMove = true; //움직일 수 있는가?
        private Coroutine _kbCoroutine; //넉백 코루틴 저장 (최적화)

        public void SetMovement(float xMove) => _xMove = xMove;

        public void StopImmediately(bool isYAxis = false)
        {
            _xMove = 0;
            if (isYAxis)
            {
                RbCompo.linearVelocity = Vector2.zero;
            }
            else
            {
                RbCompo.linearVelocityX = 0;
            }
        }

        public void Jump(float multi = 1f)
        {
            RbCompo.linearVelocity = Vector2.zero;
            RbCompo.AddForce(Vector2.up * (JumpForce * multi), ForceMode2D.Impulse);
        }

        private void FixedUpdate()
        {
            CheckGround();

            if (canMove == false) return;
            if (motorControlsX)        // 토글 확인
                MoveAgent();
        }

        private void CheckGround()
        {
            Collider2D overlapBox = Physics2D.OverlapBox(transform.position, groundCheckRadius, 0, groundLayer);

            IsGround.Value = overlapBox != null;
        }

        private void MoveAgent()
        {
            RbCompo.linearVelocityX = _xMove * CurrnetMoveSpeed;
        }

        public void AddGravity(Vector2 force)
        {
            RbCompo.AddForce(force, ForceMode2D.Force);
        }

        public void JumpTo(Vector2 force)
        {
            SetMovement(force.x); //force X는 방향으로 설정하고
            RbCompo.AddForce(force, ForceMode2D.Impulse); //Impulse는 즉시 속도에 적용하는 힘.
        }

        #region Knockback section

        public void GetKnockBack(Vector3 direction, float power)
        {
            Vector3 difference = direction * (power * RbCompo.mass);
            RbCompo.AddForce(difference, ForceMode2D.Impulse);
            if (_kbCoroutine != null)
            {
                StopCoroutine(_kbCoroutine); //기존 넉백 코루틴 제거해주고
            }

            _kbCoroutine = StartCoroutine(KnockBackCoroutine());
        }

        private IEnumerator KnockBackCoroutine()
        {
            canMove = false; //움직이지 못하게 막아주고
            yield return new WaitForSeconds(KnockBackDuration); //시간 대기
            RbCompo.linearVelocity = Vector2.zero;
            canMove = true;
        }

        #endregion

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, groundCheckRadius);
            Gizmos.color = Color.white;
        }
#endif
        
    }
}