using System;
using System.Collections;
using _00.Work.CheolYee._01.Codes.Agents;
using _00.Work.CheolYee._01.Codes.Enemy.Anim;
using _00.Work.CheolYee._01.Codes.Enemy.Attacks;
using _00.Work.CheolYee._01.Codes.SO;
using UnityEngine;
using UnityEngine.Events;

namespace _00.Work.CheolYee._01.Codes.Enemy
{
    public enum EnemyBehaviourType
    {
        Air = 0,
        Idle = 1,
        Chase = 2,
        Jump = 3,
        Attack = 4,
        Death = 5
    }
    
    public abstract class Enemy : Agent
    {
        public UnityEvent onDeath;

        [Header("Enemy Settings")] 
        public EnemyDataSo enemyData; 
        
        [Header("Attack Settings")]
        public float detectRadius; // 플레이어를 탐지하는 범위
        public float attackRadius; // 공격이 가능한 거리
        
        [HideInInspector] public float attackCooldown; // 공격 쿨타임
        [HideInInspector] public float knockbackPower; // 넉백 힘
        [HideInInspector] public float attackDamage; // 공격 데미지
        [HideInInspector] public float lastAttackTime; //마지막 공격 시간
        
        public ContactFilter2D whatIsPlayer; //플레이어를 탐지하는 필터
        public Transform targetTrm; //현재 타겟 위치

        protected int EnemyLayer; //자신의 레이어 ID
        private Collider2D[] _playerCollider; //탐지한 오브젝트 저장용

        public bool CanStateChangeable { get; set; } = true; //상태 변환이 가능한지 여부

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

        private void AttackSetting(EnemyDataSo data)
        {
            attackCooldown = data.attackCooldown;
            knockbackPower = data.knockbackPower;
            attackDamage = data.attackDamage;
        }

        public Collider2D GetPlayerInRange() //감지 범위 안에 플레이어가 있는가 검사
        {
            int cnt = Physics2D.OverlapCircle(transform.position, detectRadius, whatIsPlayer, _playerCollider); // detectRadius 내에서 플레이어 레이어 탐색
            return cnt > 0 ? _playerCollider[0] : null; // 탐지되었으면 첫 번째 콜라이더 반환, 아니면 null
        }


        public abstract void SetDead();
        
        //공격 메서드 (에너미 공격에 따라 따로 구현)
        public virtual void Attack() {}

        //애니메이션 끝났을 때 호출되는 메서드 (상속받은 에너미에서 구현 필요)
        public abstract void AnimationEndTrigger();
        
        #region DelayCallback routine

        // 일정 시간 후 callback을 실행해주는 코루틴 시작 함수
        public Coroutine DelayCallBack(float time, Action callback)
        {
            return StartCoroutine(DelayCallBackCoroutine(time, callback));
        }

        // 지정된 시간 후 callback 호출하는 실제 코루틴
        private IEnumerator DelayCallBackCoroutine(float time, Action callback)
        {
            yield return new WaitForSeconds(time); // time초 대기
            callback?.Invoke(); // null 체크 후 callback 실행
        }

        #endregion
        
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