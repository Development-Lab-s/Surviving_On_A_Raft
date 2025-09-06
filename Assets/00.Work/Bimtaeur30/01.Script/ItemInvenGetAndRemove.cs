using UnityEngine;
using UnityEngine.UI;

public class ItemInvenGetAndRemove : MonoBehaviour
{
    [SerializeField] private InventorySelect IS;

    // 아이템 업그레이드
    public bool UpgradeItem(int slotIndex)
    {
        var playerItem = InventoryManager.Instance.ItemSlotList[slotIndex];
        if (playerItem == null || playerItem.Level >= 5)
            return false;

        playerItem.Upgrade();
        UpdateSlotUI(slotIndex);
        Debug.Log(playerItem.Template.ItemName + " 레벨: " + playerItem.Level);
        return true;
    }

    // 인벤토리에서 아이템 찾기 + 업그레이드
    public bool FindInventorySlot(ExItemSO templateSO)
    {
        // 이미 인벤토리에 있는지 확인
        for (int i = 0; i < InventoryManager.Instance.ItemSlotList.Length; i++)
        {
            var playerItem = InventoryManager.Instance.ItemSlotList[i];
            if (playerItem != null && playerItem.Template == templateSO)
            {
                return UpgradeItem(i);
            }
        }

        // 슬롯 범위 설정
        int startSlot = templateSO.ItemType == ItemType.AttackItem ? 0 : 5;
        int endSlot = templateSO.ItemType == ItemType.AttackItem ? 4 : 9;

        // 빈 슬롯에 추가
        for (int i = startSlot; i <= endSlot; i++)
        {
            if (InventoryManager.Instance.ItemSlotList[i] == null)
            {
                GetItemToInventory(i, templateSO);
                return true;
            }
        }

        // 슬롯 가득
        return false;
    }

    // 아이템 획득
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

    // 아이템 삭제
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

    // 슬롯 UI 갱신
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
