using System.Collections.Generic;
using _00.Work.CheolYee._01.Codes.Enemys;
using _00.Work.CheolYee._01.Codes.Enemys.Anim;
using _00.Work.CheolYee._01.Codes.Enemys.Attacks;
using _00.Work.CheolYee._01.Codes.Enemys.Boss.BossSkillAttack;
using _00.Work.CheolYee._01.Codes.Enemys.Boss.FSM.TestBoss;
using _00.Work.CheolYee._01.Codes.SO;
using UnityEngine;

namespace _00.Work.Jaehun._01.Scrips.Boss
{
    public class BossSlime : GroundEnemy
    {
        // -----------------------------------------------------------------------------------------------3페이즈---------

        [SerializeField] private BossHealthBarUI bossHpUI;
        [SerializeField] private GameObject[] phase3VanishTargets;
        [SerializeField] private Transform phase3ScaleTarget;
        private readonly List<Rigidbody2D> _phaseRb = new();
        private readonly List<Collider2D> _phaseCols = new();


        [SerializeField] private DamageCaster skill1CasterP3;
        [SerializeField] private DamageCaster skill3SlamCasterP3;
        [SerializeField] private DamageCaster skill4HitCasterP3;
        [SerializeField] private DamageCaster skill6CasterP3;

        [Header("Phase3 Settings")]
        [Header("Death Override")]
        [SerializeField] private string deathState = "Boss-Death";
        [SerializeField] private string deathTrigger = "DEATH"; // 상태명 못 찾을 때 폴백 트리거
        [SerializeField] private int deathStateLayer;
        private bool _deathUsingTrigger;
        private bool _isDying;

        [SerializeField] private string phase3ResumeState = "Idle"; // 등장 끝난 뒤 재생할 상태명
        [SerializeField] private string phase3DisappearState = "DISAPPEAR";   // 사라지는 애니메이션 트리거
        [SerializeField] private string phase3AppearState = "APPEAR";      // 나타나는 애니메이션 트리거
        [SerializeField] private float phase3VanishAnimTime = 1.0f;          // 사라짐 애니 길이(초)
        [SerializeField] private float phase3HiddenTime = 0.8f;          // 사라져 있는 시간(대기)
        [SerializeField] private float phase3AppearAnimTime = 1.0f;          // 등장 애니 길이(초)
        [SerializeField] private float phase3ScaleMultiplier = 1.6f;          // 커질 배율
        [SerializeField] private int phase3StateLayer;           // 재생할 레이어 인덱스(보통 0)

        [Header("Phase3 Meteor Rain")]
        [SerializeField] private bool phase3MeteorRain = true; // 사라지는 구간에 메테오 내리기
        private Coroutine _phase3MeteorCo;

        private bool _phase3Triggered;     // 한 번만 발동
        private bool _phase3Running;
        private Vector3 _origScale;

        private Animator _anim;              // 애니메이터 캐시


        // ---------- 불 뿜기 -------------------------------------------------------------------------
        [Header("Skill1 Settings")]
        [SerializeField] private float skill1Cooldown = 2f;
        [SerializeField] private DamageCaster skill1Caster;

        [Header("Skill1 Detect Area")]
        [SerializeField] private Vector2 skill1Offset = new Vector2(0.8f, 0.2f);
        [SerializeField][Min(0f)] private float skill1Radius = 3f;
        [SerializeField] private bool showSkill1Gizmo = true;
        [SerializeField] private Color skill1GizmoColor = new Color(1f, 0.5f, 0f, 0.25f);

        // 총알발사 -------------- (FireBullet) ------------------------------------------------------------------------------
        [Header("Skill2 Settings")]
        [SerializeField] private float skill2Cooldown;
        [SerializeField] private GameObject skill2Prefab;
        [SerializeField] private Transform skill2FirePos;
        [SerializeField] private float skill2Range;

        // ------ 스킬 썼으면 기다리기 ----------------------------------------------------------------------------------------
        [Header("Global Skill Lock")]
        [SerializeField] private float afterSkillDelay = 1f;
        [HideInInspector] public float nextSkillAllowedTime;


        // 점프 공격 ------------(JumpAttack) -----------------------------------------------------------------------0_----------
        [Header("Skill3 Jump Settings")]
        [SerializeField] private float skill3Cooldown = 6f;
        [SerializeField] private float skill3DetectRadius = 7f;
        [SerializeField] private float skill3JumpPower = 10f;
        [SerializeField] private float skill3ForwardPower = 5f;

        [Header("Phase3 Landing Gate")]
        [SerializeField] private bool phase3WaitForLanding = true;    // 착지 대기 ON/OFF
        [SerializeField] private float phase3LandingExtraDelay = 0.1f; // 착지 후 짧은 여유

        private bool _phase3Pending; // 착지 대기 중 여부

        [Header("Skill3 Jump Caster")]
        [SerializeField] private DamageCaster skill3SlamCaster;   // 박스/원형 아무거나 가능(권장: Box)

        /*[Header("Skill3 Slam Box")]
    [SerializeField] private Vector2 skill3SlamBoxSize = new Vector2(3f, 2f);
    [SerializeField] private Vector2 skill3SlamBoxOffset = new Vector2(0.5f, -0.2f);*/
        [SerializeField] private Color skill3DetectColor = new Color(1f, 0.3f, 0.2f, 0.9f);


        // 강한 공격 -------------- Skill4 (BigAttack) ----------------------------------------------------------------------------
        [Header("Skill4 HeavySlam Settings")]
        [SerializeField] private float skill4Cooldown = 5f;
        [SerializeField] private float skill4DetectRadius = 6f;

        [Header("Skill4 HeavySlam Caster")]
        [SerializeField] private DamageCaster skill4HitCaster;    // 박스/원형 아무거나
        //[SerializeField] private Vector2 skill4HitBoxSize = new Vector2(3.0f, 2.0f);
        //[SerializeField] private Vector2 skill4HitBoxOffset = new Vector2(0.8f, -0.2f);

        [Header("Gizmos (Skill4)")]
        [SerializeField] private bool showSkill4Gizmos = true;
        [SerializeField] private Color skill4DetectColor = new Color(1f, 0.8f, 0.2f, 0.9f);

        // 메테오 ---------------- Skill5 (Meteor) ---------------------------------------------------------------------------------
        [Header("Skill5 Meteor Settings")]
        [SerializeField] private MeteorPool meteorPool;
        [SerializeField] private Transform[] meteorSpawnPoints;
        [SerializeField] private Vector2 meteorDelayRange = new Vector2(1f, 2f);
        [SerializeField] private float meteorDuration = 8f;
        [SerializeField] private float meteorDamage = 20f;
        [SerializeField] private float meteorKnockback = 10f;
        [SerializeField] private float skill5Cooldown;

        // 연속공격 ------------ Skill6 (연속이 영어로 뭐더라?) ------------------------------------------------------------------------
        [Header("Skill6 Combo Attack Settings")]
        [SerializeField] private float skill6Cooldown = 10f;
        [SerializeField] private DamageCaster skill6Caster;

        private BossAttackState _bossAttackState;
        [SerializeField] internal EnemyDataSo data;

        public bool IsInAttack => StateMachine is { CurrentState: BossAttackState };

        public bool IsAnyBossSkillRunning => _bossAttackState is { IsSkillRunning: true };

        private float[] _layerWeightsBackup;

        private DamageCaster[] _damageCasters;

        public Vector2 Skill1Offset => skill1Offset;
        public float Skill1Radius => skill1Radius;

        private bool _deathAnimStarted;

        [Header("Death Landing Gate")]
        [SerializeField] private bool deathWaitForLanding = true;   // 착지 후 죽기
        [SerializeField] private float deathLandingExtraDelay = 0.08f; // 착지 후 살짝 여유

        [Header("Visual Root (Death Hide)")]        // 죽으면 Visual 숨기기.
        [SerializeField] private GameObject visualRoot;

        protected override void Awake()
        {
            base.Awake();

            _damageCasters = GetComponentsInChildren<DamageCaster>(true);

            BuildPhysicsCacheOnce();

            if (phase3ScaleTarget == null) phase3ScaleTarget = transform;
            _origScale = phase3ScaleTarget.localScale;

            _anim = GetComponentInChildren<Animator>(true);

            if (HealthComponent != null)
            {
                HealthComponent.onHit.AddListener(OnBossHit);
                HealthComponent.onDeath.AddListener(OnBossDeath);
            }

            // UI 자동 바인딩 (인스펙터 미지정 시)
            if (bossHpUI == null) bossHpUI = GetComponent<BossHealthBarUI>();
            if (bossHpUI != null && HealthComponent != null)
                bossHpUI.Bind(HealthComponent);
            else
                Debug.LogWarning("[BossSlime] bossHpUI 또는 HealthComponent 없음. HP UI 바인딩 실패");

            var triggers = GetComponentsInChildren<EnemyAnimatorTrigger>(true);
            foreach (var t in triggers) t.Initialize(this);


            StateMachine.AddState(EnemyBehaviourType.Attack,
                _bossAttackState = new BossAttackState(this, StateMachine, "ATTACK"));

            _bossAttackState.AddSkill(SkillType.Skill1, new BossSlimeSkill1(
                this, "FIRE", skill1Cooldown, skill1Caster, skill1Radius));

            _bossAttackState.AddSkill(SkillType.Skill2, new TestBossSkill2(
                this, "MAGIC", skill2Cooldown, skill2Prefab, skill2FirePos, skill2Range));

            _bossAttackState.AddSkill(
                SkillType.Skill3,
                new BossSlimeJumpAttack(
                    this, "JUMP", skill3Cooldown,
                    skill3DetectRadius,
                    /*skill3SlamBoxSize, skill3SlamBoxOffset,*/  skill3SlamCaster,
                    skill3JumpPower, skill3ForwardPower
                )
            );
            _bossAttackState.SetAllowsMovement(SkillType.Skill3);


            _bossAttackState.AddSkill(SkillType.Skill4, new BossSlimeBigAttack(
                this, "BIGATTACK", skill4Cooldown,
                skill4DetectRadius, skill4HitCaster
                /*skill4HitBoxSize, skill4HitBoxOffset*/));
            // 이동 허용 X (기본값이 고정이므로 추가 설정 불필요)

            _bossAttackState.AddSkill(
                SkillType.Skill5,
                new BossSlimeMeteorSkill(
                    this,
                    "METEOR",
                    skill5Cooldown,
                    meteorPool,
                    meteorSpawnPoints,          // Transform[] 전달하기
                    meteorDelayRange,
                    meteorDuration,
                    meteorDamage,
                    meteorKnockback,
                    onFinished: () =>           // 스킬 끝나면 Chase로 전환하기
                    {
                        if (StateMachine != null)
                            StateMachine.ChangeState(EnemyBehaviourType.Chase);
                    }
                )
            );

            _bossAttackState.AddSkill(
                SkillType.Skill6,                       // ← enum에 Skill6 추가 필요
                new BossSlimeComboAttack(
                    this,
                    "COMBOATTACK",                      // 애니메이션 스테이트/파라미터명(프로젝트 규칙과 맞추기)
                    skill6Cooldown,
                    skill6Caster
                )
            );

            var meteor = GetComponentInChildren<BossSlimeMeteorController>(true);
            if (meteor != null)
            {
                // 필요시 세부값 조정 가능 (기본은 인스펙터)
                // meteor.AllowDuringOtherSkills = false; // 등등
            }

        }

        private void BuildPhysicsCacheOnce()
        {
            if (_phaseRb.Count > 0 || _phaseCols.Count > 0) return; // 이미 캐시됨

            // 루트(+자식) 전부 대상: 루트에 있는 RB/Collider도 포함됨
            GetComponentsInChildren(true, _phaseRb);
            GetComponentsInChildren(true, _phaseCols);
        }
        private void SetCombatEnabled(bool enableded)
        {
            if (_damageCasters == null) return;
            foreach (var dc in _damageCasters)
                if (dc) dc.enabled = enableded;
        }
        private void HideVisualOnDeath()
        {
            if (visualRoot != null)
            {
                visualRoot.SetActive(false);
                return;
            }

            // visualRoot 안 넣었으면, 있던 vanish 대상들로라도 꺼주기(옵션)
            if (phase3VanishTargets != null)
            {
                foreach (var go in phase3VanishTargets)
                    if (go) go.SetActive(false);
            }
        }

        public void AnimEvent_ComboFlip()
        {
            // 좌/우 토글
            var t = transform;
            float ny = Mathf.Approximately(t.eulerAngles.y, 0f) ? 180f : 0f;
            t.eulerAngles = new Vector3(0f, ny, 0f);

            // 잔류 속도 제거(연속 찍기 동안 미끄러짐 방지)
            var rb = MovementComponent?.RbCompo;
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0f;
            }
        }

        private void SetPhysicsEnabled(bool enableded)
        {
            // Rigidbody2D: 물리 완전 정지/재개
            foreach (var rb in _phaseRb)
            {
                if (!rb) continue;
                if (!enableded)
                {
                    rb.linearVelocity = Vector2.zero;
                    rb.angularVelocity = 0f;
                    rb.simulated = false;
                }
                else
                {
                    rb.simulated = true;
                    rb.WakeUp();
                }
            }

            // Collider2D: 판정 제거/복구
            foreach (var c in _phaseCols)
            {
                if (!c) continue;
                c.enabled = enableded;
            }
        }
        private bool IsGroundedFast()
        {
            // 팀 공용의 MovementComponent.IsGround 값만 사용 (공용 코드 변경 없음)
            return MovementComponent != null &&
                   MovementComponent.IsGround is { Value: true };
        }
        private void MuteAnimatorLayersExcept(int keepLayer)
        {
            if (_anim == null) return;
            int lc = _anim.layerCount;
            if (_layerWeightsBackup == null || _layerWeightsBackup.Length != lc)
                _layerWeightsBackup = new float[lc];

            for (int i = 0; i < lc; i++)
            {
                _layerWeightsBackup[i] = _anim.GetLayerWeight(i);
                if (i != keepLayer) _anim.SetLayerWeight(i, 0f);
            }
        }
        private void RestoreAnimatorLayers()
        {
            if (_anim == null || _layerWeightsBackup == null) return;
            for (int i = 0; i < _layerWeightsBackup.Length; i++)
                _anim.SetLayerWeight(i, _layerWeightsBackup[i]);
        }

        private bool TryForcePlay(string stateName, int layer)
        {
            if (_anim == null || !_anim.isActiveAndEnabled || string.IsNullOrEmpty(stateName))
                return false;

            // "레이어명.state명" 해시와 "state명" 해시 둘 다 시도
            string layerName = _anim.GetLayerName(layer);
            int fullHash = Animator.StringToHash(layerName + "." + stateName);
            int shortHash = Animator.StringToHash(stateName);

            if (_anim.HasState(layer, fullHash))
            {
                _anim.Play(fullHash, layer, 0f);
                _anim.Update(0f);
                return true;
            }
            if (_anim.HasState(layer, shortHash))
            {
                _anim.Play(shortHash, layer, 0f);
                _anim.Update(0f);
                return true;
            }
            Debug.LogWarning($"[BossSlime] Death state not found: {layerName}.{stateName} / {stateName}");
            return false;
        }

        public void OnSkillTakeoff() => _bossAttackState?.OnAnimationTakeoff(); // JumpTakeoff용

        public override void Attack()
        {
            if (_phase3Running || _phase3Pending) return;
            _bossAttackState?.OnAnimationCast();
        }
        private void ForcePlayState(string stateName, int layer)
        {
            if (_anim == null || !_anim.isActiveAndEnabled) return;
            _anim.Play(stateName, layer, 0f); // 즉시 그 스테이트 0%로 점프
            _anim.Update(0f);                 // 프레임 내 반영(동일 프레임에 확정)
        }

        protected override void Update()
        {
            // GroundEnemy.Update() 를 재구현: 공격 중엔 플립만 건너뜀
            bool inAttack = StateMachine is { CurrentState: BossAttackState };

            if (targetTrm != null && isDead == false && !inAttack)
            {
                // 공격 중이 아닐 때만 바라보는 방향 자동 플립
                HandleSpriteFlip(targetTrm.position);
            }

            // 상태 업데이트는 항상 수행
            StateMachine?.CurrentState?.Update();

            if (_phase3Running) ResetAllTriggersExcept(phase3DisappearState, phase3AppearState);
            // 죽음 시퀀스 동안 들어오는 다른 트리거/Bool을 매 프레임 지워서 재진입 방지

            if (_deathAnimStarted && _anim && _anim.isActiveAndEnabled)
            {
                if (_deathUsingTrigger) ResetAllTriggersExcept(deathTrigger);
                else ResetAllTriggersExcept();
            }
        }

        private void ResetAllTriggersExcept(params string[] allow)
        {
            if (_anim == null || !_anim.isActiveAndEnabled) return;

            var ps = _anim.parameters;
            foreach (var p in ps)
            {
                // allow 목록은 트리거 이름만 허용
                bool keep = false;
                foreach (var t in allow)
                    if (p.name == t) { keep = true; break; }

                if (keep) continue;

                if (p.type == AnimatorControllerParameterType.Trigger)
                    _anim.ResetTrigger(p.name);
                else if (p.type == AnimatorControllerParameterType.Bool)
                    _anim.SetBool(p.name, false);
            }
        }
        private void PlayVanishAnim()
        {
            if (_anim == null) return;
            _anim.ResetTrigger(phase3AppearState);
            ResetAllTriggersExcept(phase3DisappearState);  // 안전
            _anim.CrossFadeInFixedTime(phase3DisappearState, 0.02f, 0);
        }
        private void PlayAppearAnim()
        {
            if (_anim == null) return;
            _anim.ResetTrigger(phase3DisappearState);
            ResetAllTriggersExcept(phase3AppearState);     // 안전
            _anim.CrossFadeInFixedTime(phase3AppearState, 0.02f, 0);
        }

        public override void AnimationEndTrigger()
        {
            if (_isDying)
            {
                _isDying = false;
                _deathUsingTrigger = false;

                RestoreAnimatorLayers();
                lockFlip = false;          // 혹시 모를 락 해제

                HideVisualOnDeath();

                SetDead();
                return;
            }

            StateMachine.CurrentState.AnimationEndTrigger();
            _bossAttackState.AnimationEndTrigger();
        }
        public void StartGlobalSkillLock()        // 스킬 쓰면 다음 스킬 쓸때까지는 기다리기
        {
            nextSkillAllowedTime = Time.time + afterSkillDelay;
        }

        public bool IsGlobalSkillReady()
        {
            bool ok = Time.time >= nextSkillAllowedTime;
            return ok;
        }

        private void SetTargetsActive(bool active)
        {
            if (phase3VanishTargets == null) return;
            foreach (var go in phase3VanishTargets)
                if (go) go.SetActive(active);
        }

        // 활성 상태의 모든 대상에 트리거 전파
        private void OnBossHit()
        {
            var hp = HealthComponent;
            if (hp != null)
            {

                // 반피 되면 3페이즈 돌입
                if (!_phase3Triggered && !_phase3Running && hp.NormalizedHealth <= 0.5f)
                {
                    StartCoroutine(Phase3EntrySmart());
                }
            }

            // UI가 이벤트로도 갱신되지만, 타이밍 이슈 방지용으로 한 번 더 강제 갱신
            if (bossHpUI != null) bossHpUI.Refresh();
        }

        private void OnBossDeath()
        {
            if (_isDying) return;
            _isDying = true;
            _deathUsingTrigger = false;
            _deathAnimStarted = false;


            // 전투 잠시 차단(스킬/공격 못 하게) — 필요 최소치만
            StateMachine?.ChangeState(EnemyBehaviourType.Idle);
            lockFlip = true;
            SetCombatEnabled(false);

            var rb = MovementComponent?.RbCompo;
            if (rb != null)
            {
                var v = rb.linearVelocity; v.x = 0f;
                rb.linearVelocity = v;
            }

            if (bossHpUI) bossHpUI.gameObject.SetActive(false);

            if (deathWaitForLanding && !IsGroundedFast())
            {
                StartCoroutine(DeathRoutineSmart());
            }
            else
            {
                PlayDeathSequence();
            }
        }

        private System.Collections.IEnumerator Phase3Routine()
        {
            _phase3Triggered = true;
            _phase3Running = true;

            // 새 스킬/상태 차단
            StateMachine?.ChangeState(EnemyBehaviourType.Idle);
            lockFlip = true;

            SetPhysicsEnabled(false);
            SetCombatEnabled(false);

            float total = phase3VanishAnimTime + phase3HiddenTime + phase3AppearAnimTime;
            if (_phase3MeteorCo != null) { StopCoroutine(_phase3MeteorCo); _phase3MeteorCo = null; }
            _phase3MeteorCo = StartCoroutine(MeteorRainRoutine(total));

            // 다른 트리거 봉쇄 + 사라짐 강제 재생
            ResetAllTriggersExcept(phase3DisappearState, phase3AppearState);

            MuteAnimatorLayersExcept(phase3StateLayer);

            if (!string.IsNullOrEmpty(phase3DisappearState))
                ForcePlayState(phase3DisappearState, phase3StateLayer);
            else
                PlayVanishAnim();

            yield return new WaitForSeconds(phase3VanishAnimTime);

            // (비주얼 숨기기 등 연출)
            SetTargetsActive(false);
            if (SpriteRendererComponent) SpriteRendererComponent.enabled = false;
            yield return new WaitForSeconds(phase3HiddenTime);

            // 커져서 등장
            phase3ScaleTarget.localScale = _origScale * phase3ScaleMultiplier;
            SetTargetsActive(true);
            if (SpriteRendererComponent) SpriteRendererComponent.enabled = true;

            ResetAllTriggersExcept(phase3DisappearState, phase3AppearState);
            if (!string.IsNullOrEmpty(phase3AppearState))
                ForcePlayState(phase3AppearState, phase3StateLayer);
            else
                PlayAppearAnim();

            yield return new WaitForSeconds(phase3AppearAnimTime);
            if (_phase3MeteorCo != null) { StopCoroutine(_phase3MeteorCo); _phase3MeteorCo = null; } // 메테오 사라지기

            RestoreAnimatorLayers();

            _phase3Running = false;

            if (!string.IsNullOrEmpty(phase3ResumeState))
                ForcePlayState(phase3ResumeState, phase3StateLayer);

            SetPhysicsEnabled(true);
            SetCombatEnabled(true);
            lockFlip = false;
            StateMachine?.ChangeState(EnemyBehaviourType.Idle);
        }
        private System.Collections.IEnumerator Phase3EntrySmart()
        {
            _phase3Triggered = true;                 // 중복 방지
            _phase3Pending = phase3WaitForLanding; // 대기 플래그

            if (phase3WaitForLanding && !IsGroundedFast())
            {
                // 점프/낙하가 끝날 때까지 기다림
                while (!IsGroundedFast())
                    yield return null;

                if (phase3LandingExtraDelay > 0f)
                    yield return new WaitForSeconds(phase3LandingExtraDelay);
            }

            _phase3Pending = false;
            yield return StartCoroutine(Phase3Routine());
        }

        private System.Collections.IEnumerator MeteorRainRoutine(float duration)
        {
            if (!phase3MeteorRain) yield break;
            if (meteorPool == null || meteorSpawnPoints == null || meteorSpawnPoints.Length == 0)
                yield break;

            float endTime = Time.time + duration;

            while (Time.time < endTime)
            {
                // 1) 스폰 포인트 랜덤
                Transform p = meteorSpawnPoints[Random.Range(0, meteorSpawnPoints.Length)];
                if (p == null) { yield return null; continue; }

                // 2) 풀에서 하나 꺼내기
                GameObject mObj = meteorPool.Get();
                if (mObj != null)
                {
                    mObj.transform.position = p.position;

                    // 3) 메테오 초기화 (내부에서 아래로 낙하/수명 관리)
                    var meteor = mObj.GetComponent<MeteorObject>();
                    if (meteor != null)
                    {
                        // dir/shotSpeed는 메테오가 무시하므로 형식 맞춰 전달
                        meteor.Initialize(p, Vector2.down, meteorDamage, meteorKnockback, 0f);
                    }
                }

                // 4) 다음 스폰까지 랜덤 대기
                float wait = Random.Range(meteorDelayRange.x, meteorDelayRange.y);
                yield return new WaitForSeconds(wait);
            }
        }
        private System.Collections.IEnumerator DeathRoutineSmart()
        {
            while (!IsGroundedFast())
                yield return null;

            if (deathLandingExtraDelay > 0f)
                yield return new WaitForSeconds(deathLandingExtraDelay);

            PlayDeathSequence();   // 착지했으니 실제 죽음 연출 시작
        }
        private void PlayDeathSequence()
        {
            _deathAnimStarted = true;

            StateMachine?.ChangeState(EnemyBehaviourType.Idle);
            lockFlip = true;
            SetCombatEnabled(false);

            // 완전 정지
            var rb = MovementComponent?.RbCompo;
            if (rb != null)
            {
                var v = rb.linearVelocity; v.x = 0f; v.y = 0f;
                rb.linearVelocity = v;
            }
            SetPhysicsEnabled(false); //

            if (bossHpUI) bossHpUI.gameObject.SetActive(false);

            // 애니 파라미터 잠금 & 죽음 상태 강제 재생
            ResetAllTriggersExcept();
            MuteAnimatorLayersExcept(deathStateLayer);

            _deathUsingTrigger = false;
            if (!TryForcePlay(deathState, deathStateLayer))
            {
                _deathUsingTrigger = true;
                if (_anim) _anim.SetTrigger(deathTrigger);
            }
        }

#if UNITY_EDITOR
        protected override void OnDrawGizmosSelected()
        {
            base.OnDrawGizmosSelected();

            Gizmos.color = skill3DetectColor;
            Gizmos.DrawWireSphere(transform.position, skill3DetectRadius);

            if (showSkill4Gizmos)
            {
                Gizmos.color = skill4DetectColor;
                Gizmos.DrawWireSphere(transform.position, skill4DetectRadius);
            }

            /*// ───────── Skill3 (Jump) Gizmos ─────────
        // Detect 원
        Gizmos.color = new Color(0.2f, 0.6f, 1f, 0.9f);
        Gizmos.DrawWireSphere(transform.position, skill3DetectRadius);

        // Slam 박스 (flipX 반영)
        float face3 = (SpriteRendererComponent && SpriteRendererComponent.flipX) ? -1f : 1f;
        Vector3 c3 = transform.position + new Vector3(skill3SlamBoxOffset.x * face3, skill3SlamBoxOffset.y, 0f);
        Vector3 s3 = new Vector3(skill3SlamBoxSize.x, skill3SlamBoxSize.y, 0.01f);

        Gizmos.color = skill3SlamBoxFill; Gizmos.DrawCube(c3, s3);
        Gizmos.color = skill3SlamBoxColor; Gizmos.DrawWireCube(c3, s3);

        // 편집 핸들(센터 이동 + 크기 스케일)
        UnityEditor.Handles.color = skill3SlamBoxColor;
        Vector3 newC3 = UnityEditor.Handles.FreeMoveHandle(c3, 0.08f, Vector3.zero, UnityEditor.Handles.SphereHandleCap);
        if (newC3 != c3)
        {
            Vector3 local = newC3 - transform.position;
            skill3SlamBoxOffset = new Vector2(local.x * (face3), local.y);
        }

        float halfX3 = skill3SlamBoxSize.x * 0.5f;
        float halfY3 = skill3SlamBoxSize.y * 0.5f;
        Vector3 hx3 = c3 + Vector3.right * halfX3 * face3;
        Vector3 hy3 = c3 + Vector3.up * halfY3;

        float newHalfX3 = UnityEditor.Handles.ScaleValueHandle(halfX3, hx3, Quaternion.identity, 0.8f, UnityEditor.Handles.CubeHandleCap, 0.1f);
        float newHalfY3 = UnityEditor.Handles.ScaleValueHandle(halfY3, hy3, Quaternion.identity, 0.8f, UnityEditor.Handles.CubeHandleCap, 0.1f);
        newHalfX3 = Mathf.Max(0.05f, newHalfX3);
        newHalfY3 = Mathf.Max(0.05f, newHalfY3);
        if (!Mathf.Approximately(newHalfX3, halfX3) || !Mathf.Approximately(newHalfY3, halfY3))
            skill3SlamBoxSize = new Vector2(newHalfX3 * 2f, newHalfY3 * 2f);

        // ───────── Skill4 (HeavySlam) Gizmos ─────────
        if (showSkill4Gizmos)
        {
            // Detect 원
            Gizmos.color = skill4DetectColor;
            Gizmos.DrawWireSphere(transform.position, skill4DetectRadius);

            // Hit 박스 (flipX 반영)
            float face4 = (SpriteRendererComponent && SpriteRendererComponent.flipX) ? -1f : 1f;
            Vector3 c4 = transform.position + new Vector3(skill4HitBoxOffset.x * face4, skill4HitBoxOffset.y, 0f);
            Vector3 s4 = new Vector3(skill4HitBoxSize.x, skill4HitBoxSize.y, 0.01f);

            Gizmos.color = skill4BoxFillColor; Gizmos.DrawCube(c4, s4);
            Gizmos.color = skill4BoxLineColor; Gizmos.DrawWireCube(c4, s4);

            // 핸들 (센터 이동)
            UnityEditor.Handles.color = skill4BoxLineColor;
            Vector3 newC4 = UnityEditor.Handles.FreeMoveHandle(c4, 0.08f, Vector3.zero, UnityEditor.Handles.SphereHandleCap);
            if (newC4 != c4)
            {
                Vector3 local = newC4 - transform.position;
                skill4HitBoxOffset = new Vector2(local.x * (face4), local.y);
            }

            // 핸들 (가로/세로 스케일)
            float halfX4 = skill4HitBoxSize.x * 0.5f;
            float halfY4 = skill4HitBoxSize.y * 0.5f;
            Vector3 hx4 = c4 + Vector3.right * halfX4 * face4;
            Vector3 hy4 = c4 + Vector3.up * halfY4;

            float newHalfX4 = UnityEditor.Handles.ScaleValueHandle(halfX4, hx4, Quaternion.identity, 0.8f, UnityEditor.Handles.CubeHandleCap, 0.1f);
            float newHalfY4 = UnityEditor.Handles.ScaleValueHandle(halfY4, hy4, Quaternion.identity, 0.8f, UnityEditor.Handles.CubeHandleCap, 0.1f);
            newHalfX4 = Mathf.Max(0.05f, newHalfX4);
            newHalfY4 = Mathf.Max(0.05f, newHalfY4);
            if (!Mathf.Approximately(newHalfX4, halfX4) || !Mathf.Approximately(newHalfY4, halfY4))
                skill4HitBoxSize = new Vector2(newHalfX4 * 2f, newHalfY4 * 2f);
        }*/

            // ───────── Skill1 원형 감지(옵션) ─────────
            if (showSkill1Gizmo)
            {
                Vector3 center = transform.position + (Vector3)skill1Offset;
                Gizmos.color = new Color(skill1GizmoColor.r, skill1GizmoColor.g, skill1GizmoColor.b, 0.1f);
                Gizmos.DrawSphere(center, 0.05f);
                Gizmos.color = skill1GizmoColor;
                Gizmos.DrawWireSphere(center, skill1Radius);
                // dd
                UnityEditor.Handles.color = skill1GizmoColor;
                Vector3 newCenter = UnityEditor.Handles.FreeMoveHandle(center, 0.1f, Vector3.zero, UnityEditor.Handles.SphereHandleCap);
                float newRadius = UnityEditor.Handles.RadiusHandle(Quaternion.identity, center, skill1Radius);
                if (newCenter != center) skill1Offset = newCenter - transform.position;
                if (!Mathf.Approximately(newRadius, skill1Radius)) skill1Radius = Mathf.Max(0f, newRadius);
            }
        }
#endif
    }
}

