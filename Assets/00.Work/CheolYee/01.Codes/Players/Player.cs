using _00.Work.CheolYee._01.Codes.Agents;
using _00.Work.CheolYee._01.Codes.Enemys.Attacks;
using _00.Work.CheolYee._01.Codes.SO;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _00.Work.CheolYee._01.Codes.Players
{
    public class Player : Agent
    {
        //플레이어의 모든 컴포넌트를 관리하는 스크립트입니다.
        
        [Header("Character SO Data")]
        [field:SerializeField] public CharacterDataSo CharacterData {get; private set;} //캐릭터 데이터
        
        [Header("Agent SO Data")]
        [field:SerializeField] public PlayerInputSo PlayerInput {get; private set;} //인풋SO
        public PlayerAnimator PlayerAnimatorComponent { get; private set; } //플레이어 애니메이션 담당

        protected MeleeAttack AttackBehaviour; //공격을 할 수 있는가?
        
        

        [Header("Attack Settings")]
        public float damage;
        public float attackDuration;
        public float knockbackPower; // 넉백 힘
        [field:SerializeField] public DamageCaster DamageCaster { get; protected set; } //데미지 가하는 컴포넌트
        protected override void Awake()
        {
            base.Awake();
            PlayerAnimatorComponent = GetComponentInChildren<PlayerAnimator>(); //애니메이터 가져오기

            damage = CharacterData.attack;
            attackDuration = CharacterData.attackSpeed;

            AttackBehaviour = new MeleeAttack(); //임시로 기본공격을 만들어둠

            PlayerInput.OnJumpKeyPress += HandleJumpKeyPress; //점프키 이벤트에 점프 실행 로직 메서드 등록
            MovementComponent.GetComponent<PlayerMovement>().Initialize(CharacterData); //캐릭터 데이터로 기본값 설정
            HealthComponent.Initialize(this, CharacterData.health); //체력 컴포넌트에 오너 설정, 체력 설정
        }

        private void OnDestroy()
        {
            PlayerInput.OnJumpKeyPress -= HandleJumpKeyPress; //점프키 이벤트에 점프 실행 로직 메서드 해제
        }


        private void Update()
        {
            CalculateInAirTime(); //공중 시간 계산
            SetupMovementX(); //무브먼트 스크립트에 지속적으로 X값 전달
            UpdateAnimator(); //애니메이션 업데이트
            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                AttackBehaviour?.Attack(this);
            }
        }

        private void FixedUpdate()
        {
            ApplyExtraGravity(); //추가 중력 적용
        }

        private void HandleJumpKeyPress() //점프키 눌렀을 때 실행
        {
            //만약 바닥에 닿은 상태거나 코요테 타임 내라면 점프
            if (MovementComponent.IsGround.Value)
            {
                MovementComponent.Jump(); //점프해라
            }
        }

        private void SetupMovementX() //가로 이동을 실시간으로 무브먼트에 전달
        {
            MovementComponent.SetMovement(PlayerInput.Movement.x); //이동 전달
        }

        private void UpdateAnimator()
        {
            PlayerAnimatorComponent.HandleFlip(PlayerInput.Movement.x);
        }
    }
}