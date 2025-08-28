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
        for (int i = 0; i <= InventoryManager.Instance.SlotCount * InventoryManager.Instance.InvenCount - 1; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + i))
            {
                IS.SlotSelectMethod(i);

            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            IS.ChangeInvenSelecting();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            IIGAR.RemoveItem();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (isFullscreenUIEnabled != true)
            {
                IIV.ItemInfoViewMethod(InventoryManager.Instance.ItemSlotList[IS.currentSlotsSelecting]);
                isFullscreenUIEnabled = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (isFullscreenUIEnabled != true)
            {
                ICU.ItemCreateUIView();
                isFullscreenUIEnabled = true;
            }
        }
    }
}
