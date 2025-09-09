using UnityEngine;
[DisallowMultipleComponent]

public class SmallSlimeWallProbe : MonoBehaviour
{
    [Header("Ray Settings")]
    [SerializeField] private string groundLayerName = "Ground";
    [SerializeField] private Vector2 rayOriginOffset = new Vector2(0f, 0.2f); // ��¦ ������ ���
    [SerializeField] private float rayDistance = 0.6f;

    [Header("Debug")]
    [SerializeField] private bool drawGizmos = true;

    private LayerMask _groundMask;

    private void Awake()
    {
        _groundMask = LayerMask.GetMask(groundLayerName);
    }

    /// <summary>
    /// �̵� ����(dirX)�� ���̸� ���� Ground ����
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

        // �̸������ (����/���� ���)
        Transform owner = transform;
        Vector2 origin = (Vector2)owner.position + rayOriginOffset;

        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(origin, 0.03f);

        // ����� ǥ��
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(origin, origin + Vector2.right * rayDistance);
        Gizmos.DrawLine(origin, origin + Vector2.left * rayDistance);
    }
#endif
}
