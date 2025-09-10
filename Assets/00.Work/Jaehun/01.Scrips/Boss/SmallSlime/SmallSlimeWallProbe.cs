using UnityEngine;
[DisallowMultipleComponent]

public class SmallSlimeWallProbe : MonoBehaviour
{
    [Header("Ray Settings")]
    [SerializeField] private string groundLayerName = "Ground";
    [SerializeField] private Vector2 rayOriginOffset = new Vector2(0f, 0.2f); // 살짝 위에서 쏘기
    [SerializeField] private float rayDistance = 0.6f;

    [Header("Debug")]
    [SerializeField] private bool drawGizmos = true;

    private LayerMask _groundMask;

    private void Awake()
    {
        _groundMask = LayerMask.GetMask(groundLayerName);
    }

    /// <summary>
    /// 이동 방향(dirX)에 레이를 쏴서 Ground 감지
    /// </summary>
    public bool CheckAhead(Transform owner, float dirX, out RaycastHit2D hit)
    {
        hit = default;
        if (Mathf.Abs(dirX) < 0.001f) return false;

        Vector2 origin = (Vector2)owner.position + rayOriginOffset;
        Vector2 dir = new Vector2(Mathf.Sign(dirX), 0f);

        hit = Physics2D.Raycast(origin, dir, rayDistance, _groundMask);

#if UNITY_EDITOR
        if (drawGizmos)
        {
            Color c = hit.collider ? Color.red : Color.green;
            Debug.DrawRay(origin, dir * rayDistance, c);
        }
#endif
        return hit.collider != null;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (!drawGizmos) return;

        // 미리보기용 (우측/좌측 모두)
        Transform owner = transform;
        Vector2 origin = (Vector2)owner.position + rayOriginOffset;

        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(origin, 0.03f);

        // 양방향 표시
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(origin, origin + Vector2.right * rayDistance);
        Gizmos.DrawLine(origin, origin + Vector2.left * rayDistance);
    }
#endif
}
