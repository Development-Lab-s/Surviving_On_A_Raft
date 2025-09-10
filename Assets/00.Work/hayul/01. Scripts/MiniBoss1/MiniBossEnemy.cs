using _00.Work.CheolYee._01.Codes.Enemys.Boss.BossSkillAttack;
using _00.Work.CheolYee._01.Codes.Enemys.Boss.FSM.TestBoss;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Enemys.Boss
{
    public class MiniBossEnemy : GroundEnemy
    {
        [Header("Skill1 Settings")]
        [SerializeField] private float skill1Cooldown;
        [SerializeField] private GameObject skill1Prefab;
        [SerializeField] private Transform skill1FirePos;
        [SerializeField] private float skill1Range;
        
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
            _bossAttackState.AddSkill(SkillType.Skill1, new TestBossSkill1(
                this, "SKILL1", skill1Cooldown, skill1Prefab, skill1FirePos, skill1Range));

            // 스킬 2 팔 발사
            _bossAttackState.AddSkill(SkillType.Skill2, new ArmAttackSkill(
                this, "ArmAttack", armAttackCooldown, armPrefab, armSpawnPoint, armRange, armShotSpeed));
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

        // Animation Event에서 호출
        public void OnArmAttack()
        {
            if (!armPrefab || !armSpawnPoint) return;

            // 팔 프리팹 생성
            GameObject arm = Instantiate(armPrefab, armSpawnPoint.position, armSpawnPoint.rotation);

            // 팔 Animator 트리거 실행
            Animator armAnim = arm.GetComponent<Animator>();
            if (armAnim) armAnim.SetTrigger("ArmAttack");

            // 플레이어 방향으로 팔 회전
            if (targetTrm != null)
            {
                Vector3 dir = (targetTrm.position - armSpawnPoint.position).normalized;
                arm.transform.right = dir;
            }
        }

        // FSM에서 몸통 애니메이션 실행
        public void PlayArmAttackAnimation()
        {
            GetComponent<Animator>().SetTrigger("Attack");
        }
    }
}