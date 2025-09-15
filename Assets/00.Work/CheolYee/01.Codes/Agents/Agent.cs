using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Agents
{
    public class Agent : MonoBehaviour
    {
        //움직이는 생명체가 가질 기본 설정을 저장하고 있습니다.;

        [Header("Settings")]
        [SerializeField] private float extraGravity = 200f; //플레이어가 공중에 떠 있을 때 일정 시간 후 떨어지는 속도의 중력값
        [SerializeField] private float gravityDelay = 0.15f; //공중에 떠 있는 시간

        private bool _isFacingRight = true;
        public AgentMovement MovementComponent { get; private set; } //이동 담당
        public AgentHealth HealthComponent { get; private set; } //체력 담당

        public SpriteRenderer SpriteRendererComponent { get; private set; } //스프라이트 담당

        public Animator AnimatorComponent { get; private set; }


        public bool isDead; // 캐릭터가 죽었는가?

        private float _timeInAir; // 캐릭터가 공중에 떠 있는 시간
        protected virtual void Awake()
        {
            MovementComponent = GetComponentInChildren<AgentMovement>(); //무브먼트 가져오기
            HealthComponent = GetComponentInChildren<AgentHealth>(); //체력 가져오기
            AnimatorComponent = GetComponentInChildren<Animator>(); //애니메이터 가져오기
            SpriteRendererComponent = GetComponentInChildren<SpriteRenderer>(); //렌더러 가져오기
        }

        protected void CalculateInAirTime() //공중에 있는 시간 계산
        {
            // 만약 바닥에 닿아있지 않다면 공중 시간 누적
            if (MovementComponent.IsGround.Value == false)
            {
                //델타타임 더하는걸로 떠있는 시간 측정
                _timeInAir += Time.deltaTime;
            }
            else
            {
                // 땅에 닿아있으면 공중 시간 초기화
                _timeInAir = 0;
            }
        }

        protected void ApplyExtraGravity() //떨어지는 속도 증가 계산
        {
            // 공중에 떠 있는 시간이 일정 시간 이상이면 추가 중력을 적용
            if (_timeInAir > gravityDelay && MovementComponent.RbCompo.linearVelocityY < 0)
            {
                // 아래 방향으로 중력을 추가로 적용
                MovementComponent.AddGravity(Vector2.down * extraGravity);
            }
        }

        #region Flip Controller

        // 타겟 위치에 따라 캐릭터의 방향(스프라이트)을 좌우 반전
        public void HandleSpriteFlip(Vector3 targetPosition)
        {
            //만약에 타겟(마우스, 플레이어 등 움직이는 것)의 x좌표가 자신보다 크다면(오른쪽에 있다면)
            float dir = targetPosition.x - transform.position.x;

            if (dir > 0.1 && !_isFacingRight) // 타겟이 오른쪽에 있음
            {
                transform.eulerAngles = Vector3.zero; // 오른쪽 바라봄
                _isFacingRight = true;
            }
            else if (dir < -0.1 && _isFacingRight) // 타겟이 왼쪽에 있음
            {
                transform.eulerAngles = new Vector3(0, 180f, 0); // 왼쪽 바라봄
                _isFacingRight = false;
            }
        }

        #endregion
    }
}