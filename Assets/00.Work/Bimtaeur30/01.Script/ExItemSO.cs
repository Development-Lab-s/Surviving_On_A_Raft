using System;
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
    public string Name;   // 재료 이름
    public int Amount;    // 필요 수량
}

[CreateAssetMenu(fileName = "ExItemSO", menuName = "SO/ExItemSO")]
public class ExItemSO : ScriptableObject
{
    public ItemType ItemType;

    [SerializeField, Range(1, 5)]
    private int _itemLevel = 1;

    public int ItemLevel
    {
        get => _itemLevel;
        set => _itemLevel = Mathf.Clamp(value, 1, 5);
    }


    public Sprite ItemImage;
    public string ItemName;
    [TextArea] public string ItemDescription;   // 인스펙터에서 여러 줄 입력 편하게
    public string[] itemAttributes;
    public List<Ingredient> ItemIgdt = new List<Ingredient>(); // 인스펙터에 보임!

    private void OnEnable()
    {
        ItemLevel = 1;
    }
}
