using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public enum ItemType
{
    AttackItem,
    PassiveItem
}

[System.Serializable]
public class Ingredient
{
    public string Name;   // ��� �̸�
    public int Amount;    // �ʿ� ����
}

[CreateAssetMenu(fileName = "ExItemSO", menuName = "SO/ExItemSO")]
public class ExItemSO : ScriptableObject
{
    public ItemType ItemType;

    public Sprite ItemImage;
    public string ItemName;
    [TextArea] public string ItemDescription;   // �ν����Ϳ��� ���� �� �Է� ���ϰ�
    public string[] itemAttributes;
    public List<Ingredient> ItemIgdt = new List<Ingredient>(); // �ν����Ϳ� ����!
}
