using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private InventorySelect IS;
    [SerializeField] private ItemInvenGetAndRemove IIGAR;
    [SerializeField] private ItemInfoView IIV;
    [SerializeField] private ItemCreateUI ICU;

    public bool isFullscreenUIEnabled = false;

    public void ChangeUIEnabled(bool value)
    {
        isFullscreenUIEnabled = value;
    }

    private void Update()
    {
        // 1. 숫자키로 슬롯 선택
        for (int i = 0; i < InventoryManager.Instance.SlotCount * InventoryManager.Instance.InvenCount; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + i))
            {
                IS.SlotSelectMethod(i);
            }
        }

        // 2. R키 → 인벤토리 전환
        if (Input.GetKeyDown(KeyCode.R))
        {
            IS.ChangeInvenSelecting();
        }

        // 3. E키 → 선택 슬롯 아이템 삭제
        if (Input.GetKeyDown(KeyCode.E))
        {
            IIGAR.RemoveItem();
        }

        // 4. Q키 → 선택 슬롯 아이템 정보 보기
        if (Input.GetKeyDown(KeyCode.Q))
        {
            int selectedSlot = IS.currentSlotsSelecting;
            if (selectedSlot != -1)
            {
                var playerItem = InventoryManager.Instance.ItemSlotList[selectedSlot];
                if (playerItem != null)
                {
                    IIV.ItemInfoViewMethod(playerItem);
                    Debug.Log("aaaaaaaa");
                }
            }
        }

        // 5. Tab키 → 아이템 생성 UI 열기
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ICU.ItemCreateUIView();
        }
    }
}
