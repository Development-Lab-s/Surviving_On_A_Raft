using System;
using System.Collections.Generic;
using _00.Work.CheolYee._01.Codes.Items.PassiveItems;
using _00.Work.CheolYee._01.Codes.Managers;
using _00.Work.lusalord._02.Script.SO.AttackItem;
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

    public string ItemName
    {
        get
        {
            if (ItemType == ItemType.AttackItem) return attackItem.itemName;
            if (ItemType == ItemType.PassiveItem) return passiveItem.itemName;
            return null;
        }
    }

    public Sprite ItemImage
    {
        get
        {
            if (ItemType == ItemType.AttackItem) return attackItem.icon;
            if (ItemType == ItemType.PassiveItem) return passiveItem.icon;
            return null;
        }
    }

    public string ItemDescription
    {
        get
        {
            if (ItemType == ItemType.AttackItem) return attackItem.desc;
            if (ItemType == ItemType.PassiveItem) return passiveItem.desc;
            return null;
        }
    }

    public AttackItemSo attackItem;
    public PassiveItemSo passiveItem;
    public string[] itemAttributes;
    public List<Ingredient> ItemIgdt = new List<Ingredient>(); // 인스펙터에 보임!

    private void OnEnable()
    {
        ItemLevel = 1;
    }

    public void Upgrade()
    {
        if (ItemType == ItemType.AttackItem)
        {
            ItemManager.Instance.CreateAttackItem(attackItem.id);
        }

        if (ItemType == ItemType.PassiveItem) ItemManager.Instance.CreatePassiveItem(passiveItem.id);
    }
}
