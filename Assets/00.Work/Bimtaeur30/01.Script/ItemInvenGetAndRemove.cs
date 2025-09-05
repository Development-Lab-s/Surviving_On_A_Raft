using UnityEngine;
using UnityEngine.UI;

public class ItemInvenGetAndRemove : MonoBehaviour
{
    [SerializeField] private ExItemSO[] itemSO;
    [SerializeField] private InventorySelect IS;
    //private void Start()
    //{
    //    FindInventorySlot(itemSO[0]);
    //    FindInventorySlot(itemSO[1]);
    //}


    public bool FindInventorySlot(ExItemSO itemSO)
    {
        ItemType itemType = itemSO.ItemType;
        int startSlot = 0;
        int endSlot = 0;
        if (itemType == ItemType.AttackItem)
        {
            startSlot = 0;
            endSlot = 4;
        }
        else if (itemType == ItemType.PassiveItem)
        {
            startSlot = 5;
            endSlot = 9;
        }

        ExItemSO[] itemlist = InventoryManager.Instance.ItemSlotList;
        for (int i = startSlot; i <= endSlot; i++)
        {
            if (itemlist[i] == null)
            {
                GetItemToInventory(i, itemSO);
                return true;
            }
        }
        return false;
    }


    public void GetItemToInventory(int num, ExItemSO itemSO)
    {
        if (InventoryManager.Instance.ItemSlotList[num] != null)
            return;

        InventoryManager.Instance.ItemSlotList[num] = itemSO;
        Image visualImage = InventoryManager.Instance.SlotList[num].transform.Find("VisualImage").GetComponent<Image>();
        visualImage.sprite = itemSO.ItemImage;
        Color color = visualImage.color;
        color.a = 1f;
        visualImage.color = color;
    }

    public void RemoveItem()
    {
        if (IS.currentSlotsSelecting == -1)
            return;
        if (InventoryManager.Instance.ItemSlotList[IS.currentSlotsSelecting] == null)
            return;

        InventoryManager.Instance.ItemSlotList[IS.currentSlotsSelecting] = null;
        Image visualImage = InventoryManager.Instance.SlotList[IS.currentSlotsSelecting].transform.Find("VisualImage").GetComponent<Image>();
        visualImage.sprite = null;
        Color color = visualImage.color;
        color.a = 0f;
        visualImage.color = color;

        IS.SlotUnselectMethod();
    }
}
