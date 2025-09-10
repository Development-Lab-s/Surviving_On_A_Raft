using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class PlayerInput : MonoBehaviour
{
    [SerializeField] private InventorySelect IS;
    [SerializeField] private ItemInvenGetAndRemove IIGAR;
    [SerializeField] private ItemInfoView IIV;
    [SerializeField] private ItemCreateUI ICU;

    public float maxHoldTime = 1.0f;
    private float heldTime = 0f;
    private bool isHolding = false;

    public bool isFullscreenUIEnabled = false;

    public void ChangeUIEnabled(bool value)
    {
        isFullscreenUIEnabled = value;
    }
    private void Update()
    {
        for (int i = 0; i < InventoryManager.Instance.SlotCount * InventoryManager.Instance.InvenCount; i++)
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
            if (!isHolding)
            {
                int selectedSlot = IS.currentSlotsSelecting;

                // 슬롯 유효성 검사
                if (selectedSlot >= 0 && selectedSlot < InventoryManager.Instance.SlotList.Count)
                {
                    Transform sliderTransform = InventoryManager.Instance.SlotList[selectedSlot].gameObject.transform.Find("Slider");
                    if (sliderTransform != null && sliderTransform.TryGetComponent<Slider>(out Slider delectSlider))
                    {
                        StartCoroutine(HoldKey(delectSlider));
                    }
                    else
                    {
                        Debug.LogWarning("선택된 슬롯에서 Slider 컴포넌트를 찾을 수 없습니다.");
                    }
                }
                else
                {
                    Debug.LogWarning("선택된 슬롯 인덱스가 유효하지 않습니다.");
                }
            }
        }


        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ICU.ItemCreateUIView();
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
        IEnumerator HoldKey(Slider slider)
        {
            heldTime = 0f;
            isHolding = true;

            // 시작 시 슬라이더 0으로 초기화
            slider.value = 0f;

            while (Input.GetKey(KeyCode.E))
            {
                heldTime += Time.deltaTime;
                Debug.Log($"꾹 누른 시간: {heldTime:F2}초");

                // 슬라이더 값 = 현재 누른 시간 / 최대 시간
                slider.value = Mathf.Clamp01(heldTime / maxHoldTime);

                if (heldTime >= maxHoldTime)
                {
                    IIGAR.RemoveItem();
                    break;
                }
                yield return null;
            }

            // 키를 뗀 경우, 슬라이더 값 초기화
            slider.value = 0f;
            isHolding = false;
            Debug.Log($"키를 떼거나 멈췄습니다. 총 {heldTime:F2}초 눌렸습니다.");
        }

    }
}