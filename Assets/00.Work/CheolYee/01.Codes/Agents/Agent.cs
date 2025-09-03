using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Agents
{
    public class Agent : MonoBehaviour
    {
        //움직이는 생명체가 가질 기본 설정을 저장하고 있습니다.

        [Header("Settings")]
        [SerializeField] private float extraGravity = 200f; //플레이어가 공중에 떠 있을 때 일정 시간 후 떨어지는 속도의 중력값
        [SerializeField] private float gravityDelay = 0.15f; //공중에 떠 있는 시간

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

        // 캐릭터가 오른쪽을 보고 있는지 확인
        private bool IsFacingRight()
        {
            // y축 회전값이 0이면 오른쪽을 보고 있는 것으로 간주
            //Approximately: 약, 대략이라는 뜻으로, 1번값과 2번값의 부동소수점을 비교하여 근사하면 true,
            //일정 이상으로 비슷하지 않으면 false를 반환한다.
            return Mathf.Approximately(transform.eulerAngles.y, 0);
        }

        // 타겟 위치에 따라 캐릭터의 방향(스프라이트)을 좌우 반전
        protected void HandleSpriteFlip(Vector3 targetPosition)
        {
            var rb = MovementComponent.RbCompo;
            float vx = MovementComponent.RbCompo.linearVelocityX;
            // 너무 느리게 움직일 땐 방향 유지
            if (Mathf.Abs(vx) < 0.01f) return;

            var t = SpriteRendererComponent.transform;
            var s = t.localScale;
            s.x = vx >= 0 ? Mathf.Abs(s.x) : -Mathf.Abs(s.x);
            t.localScale = s;
        }

        #endregion
    }
}