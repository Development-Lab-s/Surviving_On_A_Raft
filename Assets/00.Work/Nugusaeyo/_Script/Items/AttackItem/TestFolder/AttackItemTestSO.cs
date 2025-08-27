using UnityEngine;

[CreateAssetMenu(fileName = "AttackItemTestSO", menuName = "Test/AttackItemTestSO")]
public class AttackItemTestSO : ScriptableObject
{
    [Header("아이템 이름")]
    public string name;
    [Header("세팅한 아이템의 레벨")]
    public int level;
    [Header("아이템의 고유 번호")]
    public int id;
    [Header("데미지 계수")]
    public float damage;
}
