using _00.Work.CheolYee._01.Codes.SO;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Player
{
    public class AgentPlayer : MonoBehaviour
    {
        //플레이어의 모든 컴포넌트를 관리하는 스크립트입니다.
        
        [Header("Character SO Data")]
        [field:SerializeField] public CharacterDataSo CharacterData {get; private set;} //캐릭터 데이터
        
        [Header("Settings")] 
        [SerializeField] private float extraGravity = 200f; //플레이어가 공중에 떠 있을 때 일정 시간 후 떨어지는 속도의 중력값
        [SerializeField] private float gravityDelay = 0.15f; //공중에 떠 있는 시간
        [SerializeField] private float coyoteTime = 0.1f; // 코요테 타임 지속 시간
        
        [field:SerializeField] public PlayerInputSo PlayerInput {get; private set;} //인풋SO
        public PlayerMovement MovementComponent { get; private set; } //이동 담당
        
        public PlayerAnimator AnimatorComponent { get; private set; } //애니메이션 담당

        private float _timeInAir; // 캐릭터가 공중에 떠 있는 시간
        private float _lastGroundedTime; //마지막 착지 시간
        private void Awake()
        {
            MovementComponent = GetComponentInChildren<PlayerMovement>(); //무브먼트 가져오기
            AnimatorComponent = GetComponentInChildren<PlayerAnimator>(); //애니메이터 가져오기

            PlayerInput.OnJumpKeyPress += HandleJumpKeyPress; //점프키 이벤트에 점프 실행 로직 메서드 등록
            MovementComponent.Initialize(CharacterData); //캐릭터 데이터로 기본값 설정
        }

        private void OnDestroy()
        {
            PlayerInput.OnJumpKeyPress -= HandleJumpKeyPress; //점프키 이벤트에 점프 실행 로직 메서드 해제
        }


        private void Update()
        {
            SetupMovementX(); //무브먼트 스크립트에 지속적으로 X값 전달
            CalculateInAirTime(); //공중에 있는 시간 계산
            UpdateAnimator(); //애니메이션 업데이트
        }

        private void FixedUpdate()
        {
            ApplyExtraGravity(); //점프 시간과 중력 적용 계산
        }

        private void HandleJumpKeyPress() //점프키 눌렀을 때 실행
        {
            //만약 바닥에 닿은 상태거나 코요테 타임 내라면 점프
            if (MovementComponent.IsGround.Value || Time.time - _lastGroundedTime <= coyoteTime) 
            {
                MovementComponent.Jump(); //점프해라
                _timeInAir = 0; //공중에 떴으니 시간 계산
            }
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
                _lastGroundedTime = Time.time; //코요테 타임도 설정
            }
        }

        private void SetupMovementX() //가로 이동을 실시간으로 무브먼트에 전달
        {
            MovementComponent.SetMovement(PlayerInput.Movement.x); //이동 전달
        }

        private void UpdateAnimator()
        {
            AnimatorComponent.HandleFlip(PlayerInput.Movement.x);
        }

        private void ApplyExtraGravity() //떨어지는 속도 증가 계산
        {
            // 공중에 떠 있는 시간이 일정 시간 이상이면 추가 중력을 적용
            if (_timeInAir > gravityDelay && MovementComponent.RbCompo.linearVelocity.y < 0)
            {
                // 아래 방향으로 중력을 추가로 적용
                MovementComponent.AddGravity(Vector2.down * extraGravity);
            }
        }
    }
}