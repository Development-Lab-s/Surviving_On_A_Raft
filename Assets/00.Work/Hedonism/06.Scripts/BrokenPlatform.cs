using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;

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
    [SerializeField] private bool oneShot = false;

    [Header("Effects")]
    [SerializeField] private float shakeDuration = 0.4f; // 흔들림 시간
    [SerializeField] private float shakeStrength = 0.1f; // 흔들림 세기
    [SerializeField] private int shakeVibrato = 20;      // 진동 횟수
    [SerializeField] private float shakeRandomness = 90; // 랜덤 정도

    // ✅ 흔들릴 자식 오브젝트(선택 사항)
    [SerializeField] private Transform shakeTarget;

    [SerializeField] private Collider2D[] colliders;
    [SerializeField] private Renderer[] renderers;
    private bool _isRunning;
    private bool _consumed;
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!triggerOnCollision || col.transform.position.y < transform.position.y) return;
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

        // 1) 발판 흔들림
        if (delayBeforeDisappear > 0f)
        {
            (shakeTarget ? shakeTarget : transform).DOShakePosition(
                shakeDuration,
                shakeStrength,
                shakeVibrato,
                shakeRandomness,
                false,
                true
            );
            yield return new WaitForSeconds(delayBeforeDisappear);
        }

        // 2) 렌더러/콜라이더 끄기
        SetPlatformVisible(false);
        SetPlatformCollidable(false);

        if (respawnAfter <= 0f || oneShot)
        {
            _consumed = true;
            _isRunning = false;
            yield break;
        }

        // 3) 리스폰 대기
        yield return new WaitForSeconds(respawnAfter);

        // 4) 다시 켜기
        SetPlatformCollidable(true);
        SetPlatformVisible(true);

        _isRunning = false;
    }

    private void SetPlatformCollidable(bool on)
    {
        if (colliders == null) return;
        foreach (var c in colliders)
            if (c) c.enabled = on;
    }

    private void SetPlatformVisible(bool on)
    {
        if (renderers == null) return;
        foreach (var r in renderers)
            if (r) r.enabled = on;
    }
}
