using NUnit.Framework;
using System.CodeDom.Compiler;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    [SerializeField] private GameObject InvenSlotPrefab;

    public ExItemSO[] ItemSlotList;

    public GameObject[] InventoryFrameList;
    public List<GameObject> SlotList = new List<GameObject>(); // �κ��丮 UI�� ��Ƶδ� ����Ʈ

    public int SlotCount = 5; // �κ��丮 ĭ ���� ����
    public int InvenCount = 2; // �κ��丮 (��ų) ���� ����

    private int currentInvenCount = -1; // 0���� ���� ��ȣ ����, -1�� �ƹ��͵� �ƴ�
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
        ItemSlotList = new ExItemSO[SlotCount * InvenCount];
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
                Image visualImage = clonedInvenSlot.transform.Find("VisualImage").GetComponent<Image>();
                Color color = visualImage.color;
                color.a = 0f;
                visualImage.color = color;
                SlotList.Add(clonedInvenSlot);
            }
        }
    }
}
