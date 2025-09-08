using _00.Work.Resource.Manager;
using _00.Work.Resource.SO;
using UnityEngine;

public class LaserProjectile : MonoBehaviour, IPoolable
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private BoxCollider2D _collider;

    private float _damage;
    private float _duration;
    private float _spawnTime;

    public string ItemName => "BossLaser"; // PoolManager에서 이 이름으로 관리
    public GameObject GameObject { get; }
    public void ResetItem()
    {
        throw new System.NotImplementedException();
    }

    public void Initialize(Vector3 startPos, Vector2 dir, float damage, float knockback, float duration)
    {
        gameObject.SetActive(true);

        transform.position = startPos;
        transform.rotation = Quaternion.identity; // 회전은 직접 계산

        _damage = damage;
        _duration = duration;
        _spawnTime = Time.time;

        // 레이저 길이 (예: 20)
        float length = 20f;
        Vector3 endPos = startPos + (Vector3)dir.normalized * length;

        // LineRenderer 그리기
        if (_lineRenderer)
        {
            _lineRenderer.positionCount = 2;
            _lineRenderer.SetPosition(0, startPos);
            _lineRenderer.SetPosition(1, endPos);
        }

        // Collider 맞추기 (길이와 방향)
        if (_collider)
        {
            Vector2 midPoint = (startPos + endPos) / 2f;
            transform.position = midPoint;

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            _collider.size = new Vector2(length, 0.3f); // 두께 0.3
            _collider.offset = Vector2.zero;
            _collider.enabled = true;
        }
    }

    private void Update()
    {
        if (Time.time >= _spawnTime + _duration)
        {
            PoolManager.Instance.Push(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("레이저 발동!!!");
            // collision.GetComponent<Player>()?.TakeDamage(_damage);
        }
    }

    public void OnPop()
    {
        gameObject.SetActive(true);
    }

    public void OnPush()
    {
        if (_collider) _collider.enabled = false;
        if (_lineRenderer) _lineRenderer.positionCount = 0;
        gameObject.SetActive(false);
    }
}