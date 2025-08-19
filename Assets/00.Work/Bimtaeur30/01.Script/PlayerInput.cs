using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private InventorySelect IS;
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
    }
}
