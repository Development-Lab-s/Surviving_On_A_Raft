using _00.Work.Bimtaeur30._01.Script;
using _00.Work.CheolYee._01.Codes.Agents;
using _00.Work.CheolYee._01.Codes.Agents.Movements;
using _00.Work.CheolYee._01.Codes.Core.Buffs;
using _00.Work.CheolYee._01.Codes.Managers;
using _00.Work.CheolYee._01.Codes.SO;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _00.Work.CheolYee._01.Codes.Players
{
    public class Player : Agent, IBuffable
    {
        //플레이어의 모든 컴포넌트를 관리하는 스크립트입니다.

        [Header("Character SO Data")]
        [field: SerializeField] public CharacterDataSo CharacterData { get; private set; } //캐릭터 데이터

        [Header("Agent SO Data")]
        [field: SerializeField] public PlayerInputSo PlayerInput { get; private set; } //인풋SO

        public float damageMulti = 1;
        public float critChanceMulti = 1;
        public float attackSpeedMulti = 1;

        private PlayerAnimator PlayerAnimatorComponent { get; set; } //플레이어 애니메이션 담당

        private bool _setDead;

        [Header("Attack Settings")]
        private float _damage;
        private float _attackSpeed;
        public float CurrentAttackSpeed => attackSpeedMulti * _attackSpeed;
        public float CurrentDamage
        {
            get
            {
                if (IsCritical())
                {
                    IsCrit = true;
                    return _damage * damageMulti * 2;
                }

                IsCrit = false;
                return _damage * damageMulti;
            }
        }

        public bool IsCrit { get; private set; }

        private int _critChance;
        public int CurrentCriticalChance => (int)(critChanceMulti * _critChance);

        public bool HaveBloodSuckingItem { get; set; }
        public float BloodSuckingHealMultiplier { get; set; }
        
        public bool HaveHealing { get; set; }
        public float HealingMultiplier { get; set; }

        private float _healTimer;
        public void BloodSucking()
        {
            if (HaveBloodSuckingItem)
            {
                HealthComponent.HealPer(BloodSuckingHealMultiplier);
            }
        }
        protected override void Awake()
        {
            base.Awake();
            CharacterData = GameSelectManager.Instance.currentCharacter;
            PlayerAnimatorComponent = GetComponentInChildren<PlayerAnimator>(); //애니메이터 가져오기

            _damage = CharacterData.attack;
            _critChance = CharacterData.criticalChance;
            _attackSpeed = CharacterData.attackSpeed;
            PlayerAnimatorComponent.AnimatorComponent.runtimeAnimatorController = CharacterData.animatorController;

            PlayerInput.OnJumpKeyPress += HandleJumpKeyPress; //점프키 이벤트에 점프 실행 로직 메서드 등록
            MovementComponent.GetComponent<PlayerMovement>().Initialize(CharacterData); //캐릭터 데이터로 기본값 설정
            HealthComponent.Initialize(this, CharacterData.health); //체력 컴포넌트에 오너 설정, 체력 설정
        }

        private void Start()
        {
            StatManager.Instance.OnPlayerBuff += ApplyBuff;
            StatManager.Instance.OnResetPlayerBuff += ResetBuff;
            
            ItemCreateManager.Instance.CreateStartItem(CharacterData.startItem);
        }

        private void OnDestroy()
        {
            PlayerInput.OnJumpKeyPress -= HandleJumpKeyPress; //점프키 이벤트에 점프 실행 로직 메서드 해제
        }


        private void Update()
        {
            CalculateInAirTime(); //공중 시간 계산
            if (!_setDead)
            {
                SetupMovementX(); //무브먼트 스크립트에 지속적으로 X값 전달
                UpdateAnimator(); //애니메이션 업데이트
            }

            _healTimer += Time.deltaTime;
            if (_healTimer >= 5)
            {
                _healTimer = 0;
                HealthComponent.HealPer(HealingMultiplier);
            }
        }

        private void FixedUpdate()
        {
            ApplyExtraGravity(); //추가 중력 적용
        }

        public bool IsCritical()
        {
            int roll = Random.Range(0, 100);
            return roll < CurrentCriticalChance;
        }

        private void HandleJumpKeyPress() //점프키 눌렀을 때 실행
        {
            //만약 바닥에 닿은 상태거나 코요테 타임 내라면 점프
            if (MovementComponent.IsGround.Value && !_setDead)
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
            PlayerAnimatorComponent.SetJump(!MovementComponent.IsGround.Value); //이동 전달
            PlayerAnimatorComponent.MovePlayer(Mathf.Abs(MovementComponent.RbCompo.linearVelocityX));
            PlayerAnimatorComponent.HandleFlip(PlayerInput.Movement.x);
        }

        public void SetDead()
        {
            _setDead = true;
            MovementComponent.StopImmediately();
            PlayerAnimatorComponent.SetDead(true);
        }

        public void ApplyBuff(StatType stat, float buff)
        {
            if (stat == StatType.Damage) damageMulti += buff;
            if (stat == StatType.CritChance) critChanceMulti += buff;
            if (stat == StatType.AttackSpeed) attackSpeedMulti += buff;
        }

        public void ResetBuff(StatType statType, float buff)
        {
            if (statType == StatType.Damage) damageMulti -= buff;
            if (statType == StatType.CritChance) critChanceMulti -= buff;
            if (statType == StatType.AttackSpeed) attackSpeedMulti -= buff;
        }
    }
}