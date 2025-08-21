using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private InventorySelect IS;
    [SerializeField] private ItemInvenGetAndRemove IIGAR;
    [SerializeField] private ItemInfoView IIV;
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
            IIV.ItemInfoViewMethod(InventoryManager.Instance.ItemSlotList[IS.currentSlotsSelecting]);
        }
    }
}
