using UnityEngine;
using UnityEngine.UI;

public class ItemInvenGetAndRemove : MonoBehaviour
{
    [SerializeField] private ExItemSO[] itemSO;
    [SerializeField] private InventorySelect IS;
    private void Start()
    {
        GetItemToInventory(0, itemSO[0]);
        GetItemToInventory(1, itemSO[1]);
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
