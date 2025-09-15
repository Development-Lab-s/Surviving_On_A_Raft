using _00.Work.CheolYee._01.Codes.Enemys;
using _00.Work.CheolYee._01.Codes.Enemys.Boss.BossSkillAttack;
using System;
using System.Collections;
using UnityEngine;

public class BossSlimeMeteorSkill : SkillState
{
    [Header("Pooling & Spawn")]
    [SerializeField] private MeteorPool meteorPool;
    [SerializeField] private Transform[] spawnPoints;    // 5~9개 랜덤 포인트

    [Header("Timing")]
    [SerializeField] private Vector2 delayRange = new Vector2(1f, 2f); // 스폰 간격(랜덤)
    [SerializeField] private float duration = 8f;                      // 스킬 지속시간

    [Header("Damage")]
    [SerializeField] private float meteorDamage = 20f;
    [SerializeField] private float meteorKnockback = 10f;

    private bool _running;
    private Coroutine _routine;
    private readonly Action _onFinished;

    public BossSlimeMeteorSkill(
         Enemy owner,
        string animBoolName,
        float coolDown,
        MeteorPool pool,
        Transform[] points,
        Vector2 delayRange,
        float duration,
        float damage,
        float knockback,
         Action onFinished = null
    ) : base(owner, animBoolName, coolDown)
    {
        this.meteorPool = pool;
        this.spawnPoints = points;
        this.delayRange = delayRange;
        this.duration = duration;
        this.meteorDamage = damage;
        this.meteorKnockback = knockback;
        this._onFinished = onFinished;
    }

    public override bool CanUse()
    {
        return Time.time >= LastAttackTime + CoolDown; // 쿨만 체크
    }

    public override void Enter()
    {
        base.Enter();
        if (_running) return;

        if (meteorPool == null || spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogWarning("[MeteorSkill] pool 또는 spawnPoints가 비었슴요.");
            return;
        }

        _running = true;
        _routine = Enemy.StartCoroutine(RunLoop());

        LastAttackTime = Time.time;
        //if (Enemy is BossSlime boss) boss.StartGlobalSkillLock();
    }

    public override void Exit()
    {
        base.Exit();
        if (_routine != null)
        {
            Enemy.StopCoroutine(_routine);
            _routine = null;
        }
        _running = false;
    }

    private IEnumerator RunLoop()
    {
        float endTime = Time.time + duration;

        while (_running && Time.time < endTime)
        {
            // 랜덤 포인트 선택 (null 제외)
            Transform p = null;
            for (int tries = 0; tries < 6 && p == null; tries++)
            {
                var cand = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
                if (cand != null) p = cand;
            }
            if (p != null)
                SpawnOneAt(p.position);

            yield return new WaitForSeconds(UnityEngine.Random.Range(delayRange.x, delayRange.y));
        }

        // 스킬 종료 → 쿨 시작
        LastAttackTime = Time.time;
        _running = false;

        Debug.Log($"[MeteorSkill] finished (duration {duration}s). CD start @ {LastAttackTime:F2}");
        _onFinished?.Invoke();
        // 끝났다는걸 알려야해!!!!
    }

    private void SpawnOneAt(Vector3 pos)
    {
        var go = meteorPool.Get();
        if (go == null) return;

        var meteor = go.GetComponent<MeteorObject>();
        if (meteor == null)
        {
            meteorPool.Return(go);
            Debug.LogWarning("[MeteorSkill] Pooled object에 MeteorObject가 없습니다.");
            return;
        }

        meteor.SetPool(meteorPool);
        meteor.InitializeAt(pos, meteorDamage, meteorKnockback);
    }
}
