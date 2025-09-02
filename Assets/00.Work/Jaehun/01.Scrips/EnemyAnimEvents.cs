using UnityEngine;

public class EnemyAnimEvents : MonoBehaviour
{
    private AirEnemy _enemy;
    private AirMeleeDamage _airMelee;

    void Awake()
    {
        // �θ� ��(��Ʈ)���� ��Ʈ��/���� ������ ���������� ĳ��
        _enemy = GetComponentInParent<AirEnemy>();
        _airMelee = GetComponentInParent<AirMeleeDamage>();
    }

    // �ִϸ��̼� �߰� �����ӿ��� ȣ��� ���� �̺�Ʈ
    public void Attack()
    {
        // AirMeleeDamage�� ������ Attack() ���� �� DamageCaster�� ���� ������
        if (_airMelee != null) _airMelee.Attack();
    }

    // �ִϸ��̼� �� �����ӿ��� ȣ��(���� ����/������)
    public void AnimationEndTrigger()
    {
        // AirEnemy �ʿ� �̹� ������ Ʈ���ŷ� ���¸ӽſ� �������� ��ȣ ����
        if (_enemy != null) _enemy.AnimationEndTrigger();
    }
}
