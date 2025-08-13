using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private void Update()
    {
        for (int i = 1; i <= InventoryManager.Instance.SlotCount; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + i))
            {
                InventorySelect.Instance.InvenSelectMethod(i);

            }
        }
    }
}
