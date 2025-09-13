using System.Collections;
using UnityEngine;
using DG.Tweening; // 두트윈 네임스페이스 추가

[DisallowMultipleComponent]
public class BrokenPlatform : MonoBehaviour
{
    [Header("Trigger")]
    [SerializeField] private string triggerTag = "Player";
    [SerializeField] private bool triggerOnCollision = true;

    [Header("Timing")]
    [SerializeField, Min(0f)] private float delayBeforeDisappear = 0.4f;
    [SerializeField, Min(0f)] private float respawnAfter = 2.0f;

    [Header("Behavior")]
    [SerializeField] private bool deactivateGameObject = false;
    [SerializeField] private bool oneShot = false;

    [Header("Effects")]
    [SerializeField] private float shakeDuration = 0.4f; // 흔들림 시간
    [SerializeField] private float shakeStrength = 0.1f; // 흔들림 세기
    [SerializeField] private int shakeVibrato = 20;      // 진동 횟수
    [SerializeField] private float shakeRandomness = 90; // 랜덤 정도

    private Collider2D[] _colliders;
    private Renderer[] _renderers;
    private bool _isRunning;
    private bool _consumed;

    private void Awake()
    {
        _colliders = GetComponentsInChildren<Collider2D>(includeInactive: true);
        _renderers = GetComponentsInChildren<Renderer>(includeInactive: true);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!triggerOnCollision) return;
        TryTrigger(col.collider);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggerOnCollision) return;
        TryTrigger(other);
    }

    private void TryTrigger(Collider2D col)
    {
        if (_isRunning || _consumed) return;
        if (!col || !col.CompareTag(triggerTag)) return;
        StartCoroutine(RunSequence());
    }

    private IEnumerator RunSequence()
    {
        _isRunning = true;

        // 1) 발판 흔들림 시작 (밟으면 즉시)
        if (delayBeforeDisappear > 0f)
        {
            transform.DOShakePosition(
                shakeDuration,
                shakeStrength,
                shakeVibrato,
                shakeRandomness,
                false,   // 스냅 여부
                true     // localPosition 기준
            );

            // delay만큼 대기
            yield return new WaitForSeconds(delayBeforeDisappear);
        }

        // 2) 사라짐
        SetPlatformVisible(false);
        SetPlatformCollidable(false);

        if (deactivateGameObject)
            gameObject.SetActive(false);

        if (respawnAfter <= 0f || deactivateGameObject || oneShot)
        {
            _consumed = true;
            _isRunning = false;
            yield break;
        }

        // 3) 리스폰 대기
        yield return new WaitForSeconds(respawnAfter);

        // 4) 복귀
        SetPlatformCollidable(true);
        SetPlatformVisible(true);

        _isRunning = false;
    }

    private void SetPlatformCollidable(bool on)
    {
        if (_colliders == null) return;
        foreach (var c in _colliders)
            if (c) c.enabled = on;
    }

    private void SetPlatformVisible(bool on)
    {
        if (_renderers == null) return;
        foreach (var r in _renderers)
            if (r) r.enabled = on;
    }
}
