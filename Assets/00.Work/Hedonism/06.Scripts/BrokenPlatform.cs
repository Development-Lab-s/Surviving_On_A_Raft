using System.Collections;
using UnityEngine;

[DisallowMultipleComponent]
public class BrokenPlatform : MonoBehaviour
{
    [Header("Trigger")]
    [SerializeField] private string triggerTag = "Player";
    [SerializeField] private bool triggerOnCollision = true; // true: OnCollisionEnter2D, false: OnTriggerEnter2D

    [Header("Timing")]
    [SerializeField, Min(0f)] private float delayBeforeDisappear = 0.4f; // 밟은 뒤 사라지기까지 지연
    [SerializeField, Min(0f)] private float respawnAfter = 2.0f;          // 다시 나타날 시간 (0이면 복귀 안 함)

    [Header("Behavior")]
    [SerializeField] private bool deactivateGameObject = false; // true면 오브젝트 자체를 SetActive(false)
    [SerializeField] private bool oneShot = false;              // 한 번만 작동하고 더 이상 작동 안 함

    private Collider2D[] _colliders;
    private Renderer[] _renderers;
    private bool _isRunning;
    private bool _consumed; // oneShot용

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

        // 1) 대기 (밟고 잠시 후)
        if (delayBeforeDisappear > 0f)
            yield return new WaitForSeconds(delayBeforeDisappear);

        // 2) 사라짐
        SetPlatformVisible(false);
        SetPlatformCollidable(false);

        if (deactivateGameObject)
            gameObject.SetActive(false); // 코루틴은 여기서 중단됨(자기 자신 비활성). 복귀하려면 아래 방식 대신 비활성 X

        // 3) 리스폰 없는 경우
        if (respawnAfter <= 0f || deactivateGameObject || oneShot)
        {
            _consumed = true;
            _isRunning = false;
            yield break;
        }

        // 4) 리스폰 대기
        yield return new WaitForSeconds(respawnAfter);

        // 5) 복귀
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
