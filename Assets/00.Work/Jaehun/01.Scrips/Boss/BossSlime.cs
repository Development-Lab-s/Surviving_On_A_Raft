using _00.Work.CheolYee._01.Codes.Enemys;
using _00.Work.CheolYee._01.Codes.Enemys.Anim;
using _00.Work.CheolYee._01.Codes.Enemys.Attacks;
using _00.Work.CheolYee._01.Codes.Enemys.Boss.BossSkillAttack;
using _00.Work.CheolYee._01.Codes.Enemys.Boss.FSM.TestBoss;
using UnityEngine;

public class BossSlime : GroundEnemy
{
    [Header("Skill1 Settings")]
    [SerializeField] private float skill1Cooldown;
    [SerializeField] private DamageCaster skill1Caster;
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

        var triggers = GetComponentsInChildren<EnemyAnimatorTrigger>(true);
        foreach (var t in triggers) t.Initialize(this);

        //���� ���� �߰�
        //����: ���� ���� �ȿ� ��ų ������Ʈ �ӽ��� �����Ƿ�, ��ų�ӽ��� new�� ���� ���� ���� ���ƾ� ��
        StateMachine.AddState(EnemyBehaviourType.Attack,
            _bossAttackState = new BossAttackState(this, StateMachine, "ATTACK"));

        //��ų �߰�
        _bossAttackState.AddSkill(SkillType.Skill1, new BossSlimeSkill1(
            this, "ATTACK", skill1Cooldown, skill1Caster, skill1Range));
        _bossAttackState.AddSkill(SkillType.Skill2, new TestBossSkill2(
            this, "MAGIC", skill2Cooldown, skill2Prefab, skill2FirePos, skill2Range));
    }

    public override void Attack()
    {
        _bossAttackState?.OnAnimationCast();
    }
    public override void AnimationEndTrigger()
    {
        // �⺻: ���� State�� AnimationEndTrigger ȣ�� (���� ���� ����)
        StateMachine.CurrentState.AnimationEndTrigger();
        _bossAttackState.AnimationEndTrigger();
    }
}

