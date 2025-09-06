using UnityEngine;
using UnityEngine.UI;

public class ItemInvenGetAndRemove : MonoBehaviour
{
    [SerializeField] private InventorySelect IS;

    // ������ ���׷��̵�
    public bool UpgradeItem(int slotIndex)
    {
        var playerItem = InventoryManager.Instance.ItemSlotList[slotIndex];
        if (playerItem == null || playerItem.Level >= 5)
            return false;

        playerItem.Upgrade();
        UpdateSlotUI(slotIndex);
        Debug.Log(playerItem.Template.ItemName + " ����: " + playerItem.Level);
        return true;
    }

    // �κ��丮���� ������ ã�� + ���׷��̵�
    public bool FindInventorySlot(ExItemSO templateSO)
    {
        // �̹� �κ��丮�� �ִ��� Ȯ��
        for (int i = 0; i < InventoryManager.Instance.ItemSlotList.Length; i++)
        {
            var playerItem = InventoryManager.Instance.ItemSlotList[i];
            if (playerItem != null && playerItem.Template == templateSO)
            {
                return UpgradeItem(i);
            }
        }

        // ���� ���� ����
        int startSlot = templateSO.ItemType == ItemType.AttackItem ? 0 : 5;
        int endSlot = templateSO.ItemType == ItemType.AttackItem ? 4 : 9;

        // �� ���Կ� �߰�
        for (int i = startSlot; i <= endSlot; i++)
        {
            if (InventoryManager.Instance.ItemSlotList[i] == null)
            {
                GetItemToInventory(i, templateSO);
                return true;
            }
        }

        // ���� ����
        return false;
    }

    // ������ ȹ��
    public void GetItemToInventory(int slotIndex, ExItemSO templateSO)
    {
        if (InventoryManager.Instance.ItemSlotList[slotIndex] != null)
            return;

        InventoryManager.Instance.ItemSlotList[slotIndex] = new PlayerItem
        {
            Template = templateSO,
            Level = 1
        };
        UpdateSlotUI(slotIndex);
    }

    // ������ ����
    public void RemoveItem()
    {
        int slotIndex = IS.currentSlotsSelecting;
        if (slotIndex == -1) return;

        var playerItem = InventoryManager.Instance.ItemSlotList[slotIndex];
        if (playerItem == null) return;

        InventoryManager.Instance.ItemSlotList[slotIndex] = null;
        UpdateSlotUI(slotIndex);

        IS.SlotUnselectMethod();
    }

    // ���� UI ����
    private void UpdateSlotUI(int slotIndex)
    {
        var playerItem = InventoryManager.Instance.ItemSlotList[slotIndex];
        Image img = InventoryManager.Instance.SlotList[slotIndex].transform.Find("VisualImage").GetComponent<Image>();

        if (playerItem != null)
        {
            img.sprite = playerItem.Template.ItemImage;
            img.color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            img.sprite = null;
            img.color = new Color(1f, 1f, 1f, 0f);
        }
    }
}
