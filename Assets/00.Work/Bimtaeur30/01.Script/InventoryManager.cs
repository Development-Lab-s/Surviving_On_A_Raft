using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    [SerializeField] private GameObject InvenSlotPrefab;
    public PlayerItem[] ItemSlotList;            // 이제 PlayerItem 배열
    public GameObject[] InventoryFrameList;
    public List<GameObject> SlotList = new List<GameObject>();

    public int SlotCount = 5; // 슬롯 개수
    public int InvenCount = 2;

    private int currentInvenCount = 0;

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
        ItemSlotList = new PlayerItem[SlotCount * InvenCount];
        Generate();
    }

    private void Generate()
    {
        for (int i = 0; i < InventoryFrameList.Length; i++)
        {
            for (int x = 0; x < SlotCount; x++)
            {
                currentInvenCount++;
                GameObject clonedSlot = Instantiate(InvenSlotPrefab, InventoryFrameList[i].transform);
                clonedSlot.transform.Find("NumTxt").GetComponent<TextMeshProUGUI>().text = currentInvenCount.ToString();

                Image visualImage = clonedSlot.transform.Find("VisualImage").GetComponent<Image>();
                visualImage.color = new Color(1f, 1f, 1f, 0f);

                SlotList.Add(clonedSlot);

                // 초기 슬롯은 비어있음
                ItemSlotList[currentInvenCount] = null;
            }
            currentInvenCount = 0;
        }
    }
}
