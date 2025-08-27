using UnityEngine;

public abstract class TestAttackItem : MonoBehaviour
{
    [Header("AttackItemSO Setting")]
    public AttackItemTestSO attackItemSO;
    public string AttackItemName => attackItemSO.name;
    public int ItemID => attackItemSO.id;
    public float Damage => attackItemSO.damage;
}
