using _00.Work.CheolYee._01.Codes.Enemys.Boss.BossSkillAttack;
using _00.Work.CheolYee._01.Codes.Enemys.Boss.FSM.TestBoss;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Enemys.Boss
{
    public class BossEnemy : GroundEnemy
    {
        [Header("Skill1 Settings")]
        [SerializeField] private float skill1Cooldown;
        [SerializeField] private GameObject skill1Prefab;
        [SerializeField] private Transform skill1FirePos;
        [SerializeField] private float skill1Range;

        [Header("Skill2 Settings")]
        [SerializeField] private float skill2Cooldown;
        [SerializeField] private GameObject skill2Prefab;
        [SerializeField] private Transform skill2FirePos;
        [SerializeField] private float skill2Range;



        private BossAttackState _bossAttackState;
        protected override void Awake()
        {
            base.Awake();
            //공격 상태 추가
            //주의: 공격 상태 안에 스킬 스테이트 머신이 있으므로, 스킬머신을 new로 새로 생성 하지 말아야 함
            StateMachine.AddState(EnemyBehaviourType.Attack,
                _bossAttackState = new BossAttackState(this, StateMachine, "ATTACK"));

            //스킬 추가
            _bossAttackState.AddSkill(SkillType.Skill1, new TestBossSkill1(
                this, "SKILL1", skill1Cooldown, skill1Prefab, skill1FirePos, skill1Range));
            _bossAttackState.AddSkill(SkillType.Skill2, new TestBossSkill2(
                this, "SKILL2", skill2Cooldown, skill2Prefab, skill2FirePos, skill2Range));
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