using NUnit.Framework;
using System.CodeDom.Compiler;
using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    [SerializeField] private GameObject InvenSlotPrefab;
    public GameObject[] InventoryFrameList;
    public int SlotCount = 5; // 인벤토리 칸 개수 변수
    public int InvenCount = 2; // 인벤토리 (스킬) 개수 변수
    public List<GameObject> SlotList = new List<GameObject>(); // 인벤토리 UI를 담아두는 리스트

    private int currentInvenCount = -1; // 0부터 슬롯 번호 시작, -1은 아무것도 아님
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        Generate();
    }
    private void Generate()
    {
        for (int i = 0; i < InventoryFrameList.Length; i++)
        {
            for (int x = 0; x < SlotCount; x++)
            {
                currentInvenCount++;
                GameObject clonedInvenSlot = Instantiate(InvenSlotPrefab, InventoryFrameList[i].transform);
                clonedInvenSlot.transform.Find("NumTxt").GetComponent<TextMeshProUGUI>().text = (currentInvenCount).ToString();
                SlotList.Add(clonedInvenSlot);
            }
        }
    }
}
