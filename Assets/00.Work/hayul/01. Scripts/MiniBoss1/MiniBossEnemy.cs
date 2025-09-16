using _00.Work.CheolYee._01.Codes.Enemys;
using _00.Work.CheolYee._01.Codes.Enemys.Boss.BossSkillAttack;
using _00.Work.CheolYee._01.Codes.Enemys.Boss.FSM.GolemBoss;
using _00.Work.CheolYee._01.Codes.Enemys.Boss.FSM.TestBoss;
using UnityEngine;

namespace _00.Work.hayul._01._Scripts.MiniBoss1
{
    public class MiniBossEnemy : GroundEnemy
    {
        [Header("Skill1 Settings")]
        [SerializeField] private float skill1Cooldown;
        [SerializeField] private GameObject skill1Prefab;
        [SerializeField] private Transform skill1FirePos;
        [SerializeField] private float skill1Range;
        [SerializeField] private float skill1Lifetime;
        
        [Header("Arm Attack Settings")]
        [SerializeField] private float armAttackCooldown;
        [SerializeField] private GameObject armPrefab;       // 팔 프리팹
        [SerializeField] private Transform armSpawnPoint;    // 팔 생성 위치
        [SerializeField] private float armRange;
        [SerializeField] private float armShotSpeed;

        private BossAttackState _bossAttackState;

        protected override void Awake()
        {
            base.Awake();

            // 공격 상태 추가 (기존 구조 유지)
            StateMachine.AddState(EnemyBehaviourType.Attack,
                _bossAttackState = new BossAttackState(this, StateMachine, "ATTACK"));
            
            // 스킬 1 레이저
            _bossAttackState.AddSkill(SkillType.Skill1, new GolemBossSkill1(
                this, "SKILL1", skill1Cooldown, skill1Prefab, skill1FirePos, skill1Range, skill1Lifetime));

            // 스킬 2 팔 발사
            _bossAttackState.AddSkill(SkillType.Skill2, new ArmAttackSkill(
                this, "SKILL2", armAttackCooldown, armPrefab, armSpawnPoint, armRange, armShotSpeed));
        }

        public override void Attack()
        {
            _bossAttackState?.OnAnimationCast();
        }

        public override void AnimationEndTrigger()
        {
            // 기본: 현재 State의 AnimationEndTrigger 호출 (기존 구현 유지)
            StateMachine.CurrentState.AnimationEndTrigger();
            _bossAttackState.AnimationEndTrigger();
        }
    }
}