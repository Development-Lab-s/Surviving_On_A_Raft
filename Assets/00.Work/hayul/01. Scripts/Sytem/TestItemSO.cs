using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Item/ItemData")]
public class ItemSO : ScriptableObject
{
    [Header("기본 정보")]
    public string itemName;       // 아이템 이름
    public Sprite itemIcon;       // 아이템 이미지
    [TextArea] public string ability; // 기본 능력 설명

    [Header("강화 단계별 정보 (1강 ~ 5강)")]
    [TextArea] public string[] upgradeInfos = new string[5]; 
}