using System.Collections;
using _00.Work.CheolYee._01.Codes.Core;
using _00.Work.CheolYee._01.Codes.SO;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Player
{
    public class PlayerMovement : MonoBehaviour //플레이어의 모든 이동을 담당
    {
        [Header("References")]
        [field:SerializeField] public Rigidbody2D RbCompo {get; private set;} //다른곳에서 리지드바디를 가져오기 위함
        
        [Header("Movement Settings")]
        [SerializeField] private float moveSpeed = 5f; //이동 속도
        [SerializeField] private float jumpForce = 7f; //점프 파워
        [SerializeField] private LayerMask groundLayer; //땅 레이어
        [SerializeField] private Vector2 groundCheckRadius; //땅 체크 범위
        [SerializeField] private float knockBackDuration = 0.2f; //넉백 시간

        public readonly NotifyValue<bool> IsGround = new NotifyValue<bool>(); //땅을 감지할 때마다 이벤트를 실행
        
        private float _xMove; //x축 이동 저장
        private bool _canMove = true; //움직일 수 있는가?
        private Coroutine _kbCoroutine; //넉백 코루틴 저장 (최적화)

        public void SetMovement(float xMovement) => _xMove = xMovement; //AgentPlayer에서 무브먼트 세팅하기 위함

        public void Initialize(CharacterDataSo characterData) //캐릭터 무브먼트 초기값 설정
        {
            moveSpeed = characterData.moveSpeed;
            jumpForce = characterData.jumpForce;
        }
        public void Jump(float multiplier = 1) //점프
        {
            RbCompo.linearVelocityY = 0;
            RbCompo.AddForce(Vector2.up * jumpForce * multiplier, ForceMode2D.Impulse);
        }

        public void AddGravity(Vector2 gravity) //중력 증가 (체공 시간 지난 후)
        {
            RbCompo.AddForce(gravity, ForceMode2D.Force);
        }

        private void CheckIfGrounded() //밟고 있는 곳이 땅인지 체크
        {
            Collider2D coll = Physics2D.OverlapBox(transform.position, groundCheckRadius, 0, groundLayer);
            
            IsGround.Value = coll != null;
        }

        private void FixedUpdate() //물리 연산을 함 (이동)
        {
            CheckIfGrounded(); //바닥 체크
            
            if (_canMove == false) return; //움직일 수 없다면 이동도 정지
            RbCompo.linearVelocityX = _xMove * moveSpeed; //움직이기
        }
        
        #region Knockback section

        public void GetKnockBack(Vector3 direction, float power) //넉백 받기
        {
            Vector3 difference = direction * (power * RbCompo.mass);
            RbCompo.AddForce(difference, ForceMode2D.Impulse);
            if (_kbCoroutine != null)
            {
                StopCoroutine(_kbCoroutine); //기존 넉백 코루틴 제거해주고
            }

            _kbCoroutine = StartCoroutine(KnockBackCoroutine());
        }

        private IEnumerator KnockBackCoroutine() //넉백 코루틴
        {
            _canMove = false; //움직이지 못하게 막아주고
            yield return new WaitForSeconds(knockBackDuration); //시간 대기
            RbCompo.linearVelocity = Vector2.zero;
            _canMove = true;
        }

        public void ClearKnockBack() //넉백 즉시 제거
        {
            RbCompo.linearVelocity = Vector2.zero;
            _canMove = true;
        }

        #endregion
        
#if UNITY_EDITOR
        private void OnDrawGizmosSelected() //기즈모로 땅 체크 오버랩을 보기 위함
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, groundCheckRadius);
            Gizmos.color = Color.white;
        }
#endif
    }
}