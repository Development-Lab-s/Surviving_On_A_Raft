using _00.Work.CheolYee._01.Codes.Player;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Agent
{
    public class Agent : MonoBehaviour
    {
        //움직이는 생명체가 가질 기본 설정을 저장하고 있습니다.
        
        [Header("Settings")] 
        [SerializeField] private float extraGravity = 200f; //플레이어가 공중에 떠 있을 때 일정 시간 후 떨어지는 속도의 중력값
        [SerializeField] private float gravityDelay = 0.15f; //공중에 떠 있는 시간
        public PlayerMovement MovementComponent { get; private set; } //이동 담당
        public AgentHealth HealthComponent { get; private set; } //체력 담당

        private float _timeInAir; // 캐릭터가 공중에 떠 있는 시간
        protected virtual void Awake()
        {
            MovementComponent = GetComponentInChildren<PlayerMovement>(); //무브먼트 가져오기
            HealthComponent = GetComponentInChildren<AgentHealth>(); //체력 가져오기
        }


        protected virtual void Update()
        {
            CalculateInAirTime(); //공중에 있는 시간 계산
        }

        protected virtual void FixedUpdate()
        {
            ApplyExtraGravity(); //점프 시간과 중력 적용 계산
        }
        
        private void CalculateInAirTime() //공중에 있는 시간 계산
        {
            // 만약 바닥에 닿아있지 않다면 공중 시간 누적
            if(MovementComponent.IsGround.Value == false)
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

        private void ApplyExtraGravity() //떨어지는 속도 증가 계산
        {
            // 공중에 떠 있는 시간이 일정 시간 이상이면 추가 중력을 적용
            if (_timeInAir > gravityDelay && MovementComponent.RbCompo.linearVelocityY < 0)
            {
                // 아래 방향으로 중력을 추가로 적용
                MovementComponent.AddGravity(Vector2.down * extraGravity);
            }
        }
    }
}