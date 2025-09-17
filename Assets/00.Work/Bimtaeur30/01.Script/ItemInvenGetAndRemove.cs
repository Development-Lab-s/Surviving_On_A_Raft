using _00.Work.Bimtaeur30._01.Script;
using _00.Work.CheolYee._01.Codes.Managers;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInvenGetAndRemove : MonoBehaviour
{
    [SerializeField] private InventorySelect IS;
    [SerializeField] private RectTransform UpgradeUI;
    [SerializeField] private TextMeshProUGUI UpgradeTxt;

    // 아이템 업그레이드
    private Sequence upgradeSeq;

    public bool UpgradeItem(int slotIndex)
    {
        var playerItem = InventoryManager.Instance.ItemSlotList[slotIndex];
        
        if (playerItem == null) return false;
        
        
        if (playerItem.Level >= 5)
            return false;

        playerItem.Upgrade();
        UpdateSlotUI(slotIndex);

        if (playerItem.Template.ItemType == ItemType.AttackItem)
        {
            ItemCreatetorBars.Instance.UpdateAttackItemUI(playerItem.Template.attackItem.id);
            playerItem.Level = ItemManager.Instance.GetAttackItem(playerItem.Template.attackItem.id).level;
        }
        else
        {
            ItemCreatetorBars.Instance.UpdatePassiveItemUI(playerItem.Template.passiveItem.id);
            playerItem.Level = ItemManager.Instance.GetPassiveItem(playerItem.Template.passiveItem.id).level;
        }

        Debug.Log(playerItem.Template.ItemName + " 레벨: " + playerItem.Level);

        UpgradeUI.anchoredPosition = new Vector2(1200, -450);
        UpgradeTxt.text = $"{playerItem.Template.ItemName}(이)가 {playerItem.Level}레벨로 업그레이드되었습니다!";

        // 기존 시퀀스 있으면 제거
        upgradeSeq?.Kill();

        upgradeSeq = DOTween.Sequence();
        upgradeSeq.Append(UpgradeUI.DOAnchorPosX(713, 1f))
                  .AppendInterval(5f)
                  .Append(UpgradeUI.DOAnchorPosX(1200, 1f));

        return true;
    }



    // 인벤토리에서 아이템 찾기 + 업그레이드
    public bool FindInventorySlot(ExItemSO templateSO)
    {
        // 이미 인벤토리에 있는지 확인
        for (int i = 0; i < InventoryManager.Instance.ItemSlotList.Length; i++)
        {
            var playerItem = InventoryManager.Instance.ItemSlotList[i];
            if (playerItem == null) continue;
            if (playerItem != null && playerItem.Template.ItemName == templateSO.ItemName)
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
            Level = 0
        };
        UpgradeItem(slotIndex);
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
        if (playerItem.Template.ItemType == ItemType.AttackItem) 
            ItemManager.Instance.DeleteAttackItem(playerItem.Template.attackItem.id);
        else
            ItemManager.Instance.DeletePassiveItem(playerItem.Template.passiveItem.id);
            
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
