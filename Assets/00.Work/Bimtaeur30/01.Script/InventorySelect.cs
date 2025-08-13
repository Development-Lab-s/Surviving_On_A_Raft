using UnityEngine;
using DG.Tweening;

public class InventorySelect : MonoBehaviour
{
    public static InventorySelect Instance { get; private set; }
    public int currentSlotsSelecting = 0; // 0이면 아무것도 선택 안하는중
    public int currentInvenSelecting = 1; // 첨엔 1번 인벤토리부터

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void InvenSelectMethod(int num)
    {
        if (currentSlotsSelecting == num)
        {
            InvenUnselectMethod();
            currentSlotsSelecting = 0;
            return;
        }
        currentSlotsSelecting = num;

        for (int i = 1; i <= InventoryManager.Instance.SlotCount; i++)
        {
            RectTransform slotRect = InventoryManager.Instance.SlotList[i - 1]
                .GetComponent<RectTransform>();

            if (num == i) // 선택된 슬롯
            {
                Vector2 targetPos = slotRect.anchoredPosition + new Vector2(0, 40);
                slotRect.DOAnchorPos(targetPos, 0.2f);
            }
            else // 선택 안된 슬롯
            {
                if (Mathf.Abs(slotRect.anchoredPosition.y) > 0.1f)
                {
                    Vector2 originPos = new Vector2(slotRect.anchoredPosition.x, -60);
                    slotRect.DOAnchorPos(originPos, 0.2f);
                }
            }
        }
    }

    public void InvenUnselectMethod()
    {
        for (int i = 1; i <= InventoryManager.Instance.SlotCount; i++)
        {
            RectTransform slotRect = InventoryManager.Instance.SlotList[i - 1]
                .GetComponent<RectTransform>();

            if (Mathf.Abs(slotRect.anchoredPosition.y) > 0.1f)
            {
                Vector2 originPos = new Vector2(slotRect.anchoredPosition.x, -60);
                slotRect.DOAnchorPos(originPos, 0.2f);
            }
        }
    }
}
