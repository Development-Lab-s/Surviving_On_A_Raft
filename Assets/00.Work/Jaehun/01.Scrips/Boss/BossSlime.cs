using _00.Work.CheolYee._01.Codes.Enemys;
using _00.Work.CheolYee._01.Codes.Enemys.Anim;
using _00.Work.CheolYee._01.Codes.Enemys.Attacks;
using _00.Work.CheolYee._01.Codes.Enemys.Boss.BossSkillAttack;
using _00.Work.CheolYee._01.Codes.Enemys.Boss.FSM.TestBoss;
using UnityEngine;

public class BossSlime : GroundEnemy
{
    [Header("Skill1 Settings")]
    [SerializeField] private float skill1Cooldown = 2f;
    [SerializeField] private DamageCaster skill1Caster;

    [Header("Skill1 Detect Area")]
    [SerializeField] private Vector2 skill1Offset = new Vector2(0.8f, 0.2f);
    [SerializeField][Min(0f)] private float skill1Radius = 3f;
    [SerializeField] private bool showSkill1Gizmo = true;
    [SerializeField] private Color skill1GizmoColor = new Color(1f, 0.5f, 0f, 0.25f);

    [Header("Skill2 Settings")]
    [SerializeField] private float skill2Cooldown;
    [SerializeField] private GameObject skill2Prefab;
    [SerializeField] private Transform skill2FirePos;
    [SerializeField] private float skill2Range;

    [Header("Global Skill Lock")]
    [SerializeField] private float afterSkillDelay = 3f;
    [HideInInspector] public float nextSkillAllowedTime;


    private BossAttackState _bossAttackState;

    public Vector2 Skill1Offset => skill1Offset;
    public float Skill1Radius => skill1Radius;

    protected override void Awake()
    {
        base.Awake();
        Debug.Log($"[BossSlime] Awake on {name}");

        var triggers = GetComponentsInChildren<EnemyAnimatorTrigger>(true);
        foreach (var t in triggers) t.Initialize(this);


        StateMachine.AddState(EnemyBehaviourType.Attack,
            _bossAttackState = new BossAttackState(this, StateMachine, "ATTACK"));

        _bossAttackState.AddSkill(SkillType.Skill1, new BossSlimeSkill1(
            this, "FIRE", skill1Cooldown, skill1Caster, skill1Radius));

        _bossAttackState.AddSkill(SkillType.Skill2, new TestBossSkill2(
            this, "MAGIC", skill2Cooldown, skill2Prefab, skill2FirePos, skill2Range));
    }

    public override void Attack()
    {
        Debug.Log("[BossSlime] Attack() -> forward to _bossAttackState.OnAnimationCast()");
        _bossAttackState?.OnAnimationCast();
    }
    public override void AnimationEndTrigger()
    {
        Debug.Log("[BossSlime] AnimationEndTrigger()");
        StateMachine.CurrentState.AnimationEndTrigger();
        _bossAttackState.AnimationEndTrigger();
    }
    public void StartGlobalSkillLock()
    {
        nextSkillAllowedTime = Time.time + afterSkillDelay;
        Debug.Log($"[BossSlime] Global skill lock until {nextSkillAllowedTime:F2} (now {Time.time:F2})");
    }

    public bool IsGlobalSkillReady()
    {
        bool ok = Time.time >= nextSkillAllowedTime;
        if (!ok) Debug.Log($"[BossSlime] Global lock active (ready at {nextSkillAllowedTime:F2}, now {Time.time:F2})");
        return ok;
    }

#if UNITY_EDITOR
    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();

        if (!showSkill1Gizmo) return;

        Vector3 center = transform.position + (Vector3)skill1Offset;

        Gizmos.color = new Color(skill1GizmoColor.r, skill1GizmoColor.g, skill1GizmoColor.b, 0.1f);
        Gizmos.DrawSphere(center, 0.05f);
        Gizmos.color = skill1GizmoColor;
        Gizmos.DrawWireSphere(center, skill1Radius);


#if UNITY_EDITOR
        UnityEditor.Handles.color = skill1GizmoColor;
        var fmh_104_21_638931251220969309 = Quaternion.identity; Vector3 newCenter = UnityEditor.Handles.FreeMoveHandle(
            center, 0.1f, Vector3.zero, UnityEditor.Handles.SphereHandleCap);

        float newRadius = UnityEditor.Handles.RadiusHandle(Quaternion.identity, center, skill1Radius);

        if (newCenter != center) skill1Offset = (Vector2)(newCenter - transform.position);
        if (!Mathf.Approximately(newRadius, skill1Radius)) skill1Radius = Mathf.Max(0f, newRadius);
#endif
    }
#endif
}

