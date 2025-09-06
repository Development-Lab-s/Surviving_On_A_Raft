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
        // 1. ����Ű�� ���� ����
        for (int i = 0; i < InventoryManager.Instance.SlotCount * InventoryManager.Instance.InvenCount; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + i))
            {
                IS.SlotSelectMethod(i);
            }
        }

        // 2. RŰ �� �κ��丮 ��ȯ
        if (Input.GetKeyDown(KeyCode.R))
        {
            IS.ChangeInvenSelecting();
        }

        // 3. EŰ �� ���� ���� ������ ����
        if (Input.GetKeyDown(KeyCode.E))
        {
            IIGAR.RemoveItem();
        }

        // 4. QŰ �� ���� ���� ������ ���� ����
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

        // 5. TabŰ �� ������ ���� UI ����
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ICU.ItemCreateUIView();
        }
    }
}
