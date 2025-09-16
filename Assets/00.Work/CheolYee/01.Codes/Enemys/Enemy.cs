using _00.Work.CheolYee._01.Codes.Agents;
using _00.Work.CheolYee._01.Codes.Enemys.Anim;
using _00.Work.CheolYee._01.Codes.SO;
using System.Collections;
using _00.Work.CheolYee._01.Codes.Agents.Movements;
using _00.Work.CheolYee._01.Codes.Core.Buffs;
using _00.Work.CheolYee._01.Codes.Managers;
using _00.Work.Hedonism._06.Scripts.SO.Manager;
using UnityEngine;
using UnityEngine.Events;

namespace _00.Work.CheolYee._01.Codes.Enemys
{
    public enum EnemyBehaviourType
    {
        Air = 0,
        Idle = 1,
        Chase = 2,
        Jump = 3,
        Attack = 4,
        Death = 5,
    }

    public abstract class Enemy : Agent, IBuffable
    {
        public UnityEvent onDeath;

        [Header("Enemy Settings")]
        public EnemyDataSo enemyData;

        [Header("Attack Settings")] 
        public float damageMulti; //공격력 배율
        public float attackSpeedMulti; //공격력 배율
        public float detectRadius; // 플레이어를 탐지하는 범위
        public float attackRadius; // 공격이 가능한 거리
        
        public bool lockFlip; // 스킬쓸때 좌우 회전 잠금.
        public float CurrentAttackDamage => _attackDamage * damageMulti; //배율 적용된 공격 데미지
        public float CurrentAttackSpeed => attackCooldown * attackSpeedMulti;
        
        private float _attackDamage; // 공격 데미지
        [HideInInspector] public float attackCooldown; // 공격 쿨타임
        [HideInInspector] public float knockbackPower; // 넉백 힘
        [HideInInspector] public float lastAttackTime; //마지막 공격 시간

        
        public ContactFilter2D whatIsPlayer; //플레이어를 탐지하는 필터
        public Transform targetTrm; //현재 타겟 위치

        protected int EnemyLayer; //자신의 레이어 ID
        private Collider2D[] _playerCollider; //탐지한 오브젝트 저장용
        private EnemyAnimatorTrigger AnimatorTrigger { get; set; }

        protected override void Awake()
        {
            base.Awake();
            EnemyLayer = LayerMask.NameToLayer("Enemy");
            _playerCollider = new Collider2D[1]; //플레이어 탐지용 임시 1개 배열

            HealthComponent.Initialize(this, enemyData.maxHealth); //에너미 전용 체력 설정
            AttackSetting(enemyData); //에너미 전용 공격 설정
            MovementComponent.GetComponent<EnemyMovement>().Initialize(enemyData); //에너미 전용 무브먼트 설정

            AnimatorTrigger = GetComponentInChildren<EnemyAnimatorTrigger>(); //자식에서 컴포넌트 찾기
            AnimatorTrigger.Initialize(this); //초기화

        }

        public void Initialize()
        {
            HealthComponent.Initialize(this, enemyData.maxHealth); //에너미 전용 체력 설정
            AttackSetting(enemyData); //에너미 전용 공격 설정
            MovementComponent.GetComponent<EnemyMovement>().Initialize(enemyData); //에너미 전용 무브먼트 설정
        }

        private void OnEnable()
        {
            SpawnManager.Instance.Enemys.Add(this);
            StartCoroutine(MaskChange());
        }

        private void Start()
        {
            damageMulti = StatManager.Instance.GetEnemyBuff(StatType.Damage);
            StatManager.Instance.OnEnemyBuff += ApplyBuff;
            StatManager.Instance.OnResetEnemyBuff += ResetBuff;
        }

        private IEnumerator MaskChange()
        {
            SpriteRendererComponent.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
            yield return new WaitForSeconds(0.5f);
            SpriteRendererComponent.maskInteraction = SpriteMaskInteraction.None;
        }


        public void ApplyBuff(StatType stat, float buff)
        {
            if (stat == StatType.Damage) damageMulti = buff;
            if (stat == StatType.AttackSpeed) attackSpeedMulti = buff;
        }

        public void ResetBuff(StatType statType, float buff)
        {
            if (statType == StatType.Damage) damageMulti = 1f;
            if (statType == StatType.AttackSpeed) damageMulti = 1f;
        }

        private void AttackSetting(EnemyDataSo data)
        {
            attackCooldown = data.attackCooldown;
            knockbackPower = data.knockbackPower;
            _attackDamage = data.attackDamage;
        }

        public Collider2D GetPlayerInRange() //감지 범위 안에 플레이어가 있는가 검사
        {
            int cnt = Physics2D.OverlapCircle(transform.position, detectRadius, whatIsPlayer, _playerCollider); // detectRadius 내에서 플레이어 레이어 탐색
            return cnt > 0 ? _playerCollider[0] : null; // 탐지되었으면 첫 번째 콜라이더 반환, 아니면 null
        }


        public virtual void SetDead()
        {
            ItemManager.Instance.CreateRandomPickupItem(transform);
            GameManager.Instance.player.BloodSucking();
            SpawnManager.Instance.RemoveEnemy(this);
        }

        //공격 메서드 (에너미 공격에 따라 따로 구현)
        public virtual void Attack() { }

        //애니메이션 끝났을 때 호출되는 메서드 (상속받은 에너미에서 구현 필요)
        public abstract void AnimationEndTrigger();

#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, detectRadius); // 감지 범위 표시
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRadius); //공격 범위 표시
        }
#endif
    }
}