using UnityEngine;
using DG.Tweening;
using System.Linq;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class InventorySelect : MonoBehaviour
{
    [SerializeField] private Color SlotSelectColor;
    public int currentSlotsSelecting = -1; // -1�̸� �ƹ��͵� ���� ���ϴ���
    public int currentInvenSelecting = 1; // ÷�� 1�� �κ��丮����
    private int invenChangeWay = 1; // 1 == ��������, -1 == ����������. �� ���� �κ��丮 ��ü ��ȣ�� Ŀ������(1), �۾��������̴�(2).
    private bool invenChanging = false;

    public void SlotSelectMethod(int num)
    {
        if (InventoryManager.Instance.ItemSlotList[num] == null)
            return;
        if (currentSlotsSelecting == num)
        {
            SlotUnselectMethod();
            return;
        }
        else if (currentInvenSelecting * 5 - 1 >= num && currentInvenSelecting * 5 - 5 <= num)
        {
            currentSlotsSelecting = num;

            for (int i = 0; i <= InventoryManager.Instance.SlotCount * InventoryManager.Instance.InvenCount - 1; i++)
            {
                RectTransform slotRect = InventoryManager.Instance.SlotList[i]
                    .GetComponent<RectTransform>();

                Image slotImage = InventoryManager.Instance.SlotList[i].GetComponent<Image>();
                CanvasGroup SelectImage = InventoryManager.Instance.SlotList[i].transform.Find("SelectImage").gameObject.GetComponent<CanvasGroup>();

                if (num == i) // ���õ� ����
                {
                    Vector2 targetPos = new Vector2(slotRect.anchoredPosition.x, 10);
                    slotRect.DOAnchorPos(targetPos, 0.2f);
                    slotImage.DOColor(SlotSelectColor, 0.2f);
                    SelectImage.DOFade(1f, 0.2f);
                    SelectImage.DOFade(1f, 0.2f);
                }
                else // ���� �ȵ� ����
                {
                    if (Mathf.Abs(slotRect.anchoredPosition.y) > 0.1f)
                    {
                        Vector2 originPos = new Vector2(slotRect.anchoredPosition.x, -60);
                        slotRect.DOAnchorPos(originPos, 0.2f);
                        slotImage.DOColor(new Color(49f / 255f, 49f / 255f, 49f / 255f), 0.2f);
                        SelectImage.DOFade(0f, 0.2f);
                        SelectImage.DOFade(0f, 0.2f);
                    }
                }
            }
        }
    }

    public void SlotUnselectMethod()
    {
        currentSlotsSelecting = -1;
        for (int i = 0; i <= InventoryManager.Instance.SlotCount * InventoryManager.Instance.InvenCount - 1; i++)
        {
            RectTransform slotRect = InventoryManager.Instance.SlotList[i]
                .GetComponent<RectTransform>();

            Image slotImage = InventoryManager.Instance.SlotList[i].GetComponent<Image>();
            CanvasGroup SelectImage = InventoryManager.Instance.SlotList[i].transform.Find("SelectImage").gameObject.GetComponent<CanvasGroup>();

            if (Mathf.Abs(slotRect.anchoredPosition.y) > 0.1f)
            {
                Vector2 originPos = new Vector2(slotRect.anchoredPosition.x, -60);
                slotRect.DOAnchorPos(originPos, 0.2f);
                slotImage.DOColor(new Color(49f / 255f, 49f / 255f, 49f / 255f), 0.2f);
                SelectImage.DOFade(0f, 0.2f);
                SelectImage.DOFade(0f, 0.2f);
            }
        }
    }

    public void ChangeInvenSelecting()
    {
        if (invenChanging) return;
        invenChanging = true;

        var inventoryList = InventoryManager.Instance.InventoryFrameList;

        // �� �� ���� �� ���� ����
        if (currentInvenSelecting <= 1)
            invenChangeWay = 1;
        else if (currentInvenSelecting >= inventoryList.Length)
            invenChangeWay = -1;

        float xOffset = 160 * invenChangeWay;

        SlotUnselectMethod();

        GameObject currentInven = inventoryList[currentInvenSelecting - 1];
        GameObject nextInven = inventoryList[currentInvenSelecting - 1 + invenChangeWay];

        RectTransform recCurrent = currentInven.GetComponent<RectTransform>();
        RectTransform recNext = nextInven.GetComponent<RectTransform>();
        CanvasGroup cgCurrent = currentInven.GetComponent<CanvasGroup>();
        CanvasGroup cgNext = nextInven.GetComponent<CanvasGroup>();

        nextInven.SetActive(true);
        cgNext.alpha = 0f;
        recNext.anchoredPosition = new Vector2(xOffset, recNext.anchoredPosition.y);

        Sequence seq = DOTween.Sequence();

        seq.Join(recCurrent.DOAnchorPos(new Vector2(-xOffset, recCurrent.anchoredPosition.y), 0.2f).SetEase(Ease.OutQuad));
        seq.Join(cgCurrent.DOFade(0f, 0.2f));
        seq.Join(recNext.DOAnchorPosX(0, 0.2f).SetEase(Ease.OutQuad));
        seq.Join(cgNext.DOFade(1f, 0.2f));

        seq.OnComplete(() =>
        {
            currentInven.SetActive(false);
            currentInvenSelecting += invenChangeWay;
            invenChanging = false;
        });
    }


}
