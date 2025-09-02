using _00.Work.CheolYee._01.Codes.Enemys.Attacks;
using UnityEngine;

public class AirMeleeDamage : AirEnemy
{
    // ���Ÿ� ���� ���� ����.
    [Header("Only Melee Attack Settings")]
    [SerializeField] public DamageCaster damageCaster; //������ ���ϴ� ������Ʈ

    [SerializeField] private float radius = 0.7f;          // ��Ʈ �ݰ�(����)
    [SerializeField] private Vector2 offset = new Vector2(0.6f, 0f); // �� ���� ������(����)
    [SerializeField] private LayerMask playerMask;          // �÷��̾� ���̾�


    private MeleeAttack _attackBehaviour;
    protected override void Awake()
    {
        base.Awake();
        _attackBehaviour = new MeleeAttack();
    }

    public override void Attack()
    {
        if (damageCaster == null) return;

        // �ٶ󺸴� ���� ���� (�θ� ȸ������ �ø� ���̸� �Ʒ�, ������ �ø��̸� lossyScale.x��)
        // �� �ٶ󺸴� ������ ����°� �̹� ���������? ��� �ȳ��� �� ����
        float face = (transform.parent && transform.parent.eulerAngles.y > 0.1f) ? -1f : 1f;

        // ��Ʈ �߽ɹݰ�
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
