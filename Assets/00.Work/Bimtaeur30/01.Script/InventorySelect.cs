using UnityEngine;
using DG.Tweening;
using System.Linq;
using DG.Tweening;

public class InventorySelect : MonoBehaviour
{
    public int currentSlotsSelecting = -1; // -1이면 아무것도 선택 안하는중
    public int currentInvenSelecting = 1; // 첨엔 1번 인벤토리부터
    private int invenChangeWay = 1; // 1 == 왼쪽으로, -1 == 오른쪽으로. 즉 각각 인벤토리 교체 번호가 커지는중(1), 작아지는중이다(2).
    private bool invenChanging = false;

    public void SlotSelectMethod(int num)
    {
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

                if (num == i) // 선택된 슬롯
                {
                    Vector2 targetPos = new Vector2(slotRect.anchoredPosition.x, 10);
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
    }

    public void SlotUnselectMethod()
    {
        currentSlotsSelecting = -1;
        for (int i = 0; i <= InventoryManager.Instance.SlotCount * InventoryManager.Instance.InvenCount - 1; i++)
        {
            RectTransform slotRect = InventoryManager.Instance.SlotList[i]
                .GetComponent<RectTransform>();

            if (Mathf.Abs(slotRect.anchoredPosition.y) > 0.1f)
            {
                Vector2 originPos = new Vector2(slotRect.anchoredPosition.x, -60);
                slotRect.DOAnchorPos(originPos, 0.2f);
            }
        }
    }

    public void ChangeInvenSelecting()
    {
        if (invenChanging) return;
        invenChanging = true;

        var inventoryList = InventoryManager.Instance.InventoryFrameList;

        // 양 끝 도달 시 방향 반전
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

        seq.Join(recCurrent.DOAnchorPos(new Vector2(-xOffset, recCurrent.anchoredPosition.y), 0.5f).SetEase(Ease.OutQuad));
        seq.Join(cgCurrent.DOFade(0f, 0.5f));
        seq.Join(recNext.DOAnchorPosX(0, 0.5f).SetEase(Ease.OutQuad));
        seq.Join(cgNext.DOFade(1f, 0.5f));

        seq.OnComplete(() =>
        {
            currentInven.SetActive(false);
            currentInvenSelecting += invenChangeWay;
            invenChanging = false;
        });
    }


}
