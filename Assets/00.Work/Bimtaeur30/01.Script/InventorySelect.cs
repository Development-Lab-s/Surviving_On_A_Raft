using UnityEngine;
using DG.Tweening;

public class InventorySelect : MonoBehaviour
{
    public static InventorySelect Instance { get; private set; }
    public int currentSlotsSelecting = 0; // 0�̸� �ƹ��͵� ���� ���ϴ���
    public int currentInvenSelecting = 1; // ÷�� 1�� �κ��丮����

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

            if (num == i) // ���õ� ����
            {
                Vector2 targetPos = slotRect.anchoredPosition + new Vector2(0, 40);
                slotRect.DOAnchorPos(targetPos, 0.2f);
            }
            else // ���� �ȵ� ����
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
