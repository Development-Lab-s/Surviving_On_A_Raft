using _00.Work.CheolYee._01.Codes.Enemys.Attacks;
using UnityEngine;

public class AirMeleeDamage : AirEnemy
{
    // 원거리 몬스터 전용 공격.
    [Header("Only Melee Attack Settings")]
    [SerializeField] public DamageCaster damageCaster; //데미지 가하는 컴포넌트

    [SerializeField] private float radius = 0.7f;          // 히트 반경(원형)
    [SerializeField] private Vector2 offset = new Vector2(0.6f, 0f); // 적 기준 오프셋(앞쪽)
    [SerializeField] private LayerMask playerMask;          // 플레이어 레이어


    private MeleeAttack _attackBehaviour;
    protected override void Awake()
    {
        base.Awake();
        _attackBehaviour = new MeleeAttack();
    }

    public override void Attack()
    {
        if (damageCaster == null) return;

        // 바라보는 방향 보정 (부모 회전으로 플립 중이면 아래, 스케일 플립이면 lossyScale.x씀)
        // 그 바라보는 방향대로 만드는거 이미 만들었었나? 기억 안나서 걍 쓸게
        float face = (transform.parent && transform.parent.eulerAngles.y > 0.1f) ? -1f : 1f;

        // 히트 중심반경
        Vector2 center = (Vector2)transform.position + new Vector2(offset.x * face, offset.y);
        float r = radius;

        _attackBehaviour?.Attack(this);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        float face = (transform.parent && transform.parent.eulerAngles.y > 0.1f) ? -1f : 1f;
        Vector2 center = (Vector2)transform.position + new Vector2(offset.x * face, offset.y);
        Gizmos.DrawWireSphere(center, radius);
    }
}
