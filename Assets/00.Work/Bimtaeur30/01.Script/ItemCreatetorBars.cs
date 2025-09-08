using UnityEngine;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using System;
using UnityEngine.UI;

public class ItemCreatetorBars : MonoBehaviour
{
    [SerializeField] private GameObject PageParent;
    [SerializeField] private GameObject Page;
    [SerializeField] private GameObject CreateBar;
    private int currentPage = 0;

    public List<GameObject> pageList = new List<GameObject>();
    public List<ItemBar> barList = new List<ItemBar>();

    private void Start()
    {
        SetItemCreateBar();
    }
    private void SetItemCreateBar()
    {
        GameObject clonedPage = Instantiate(Page, PageParent.transform);
        clonedPage.transform.name = "Page_01";
        pageList.Add(clonedPage);
        List<ExItemSO> items = TJ_ItemManager.Instance.ItemList;
        for (int i = 0; i < items.Count; i++)
        {
            GameObject clonedBar = Instantiate(CreateBar, clonedPage.transform);
            clonedBar.GetComponent<ItemBar>().MyItem = items[i];
            TextMeshProUGUI itemNameTxt = clonedBar.transform.Find("ItemNameTxt").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI itemDescriptionTxt = clonedBar.transform.Find("ItemDescriptionTxt").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI sourceCountTxt = clonedBar.transform.Find("SourceCountTxt").GetComponent<TextMeshProUGUI>();
            Image itemImage = clonedBar.transform.Find("IconBaseImage").transform.Find("Image").GetComponent<Image>();
            GameObject MeatBallDes = clonedBar.transform.Find("MeatballDes").gameObject;
            TextMeshProUGUI AtTxt = MeatBallDes.transform.Find("AtTxt").GetComponent<TextMeshProUGUI>();
            if (items[i].ItemType == ItemType.AttackItem)
            {
                AtTxt.text = "분류: 공격템";
            }
            else if (items[i].ItemType == ItemType.PassiveItem)
            {
                AtTxt.text = "분류: 패시브템";
            }
            MeatBallDes.SetActive(false);
            itemNameTxt.text = items[i].ItemName;
            itemDescriptionTxt.text = items[i].ItemDescription;
            int sourceCount = items[i].ItemIgdt.Count;
            itemImage.sprite = items[i].ItemImage;
            for (int j = 0; j < sourceCount; j++)
            {
                sourceCountTxt.text = sourceCountTxt.text + " " + items[i].ItemIgdt[j].Name + "(" + items[i].ItemIgdt[j].Amount + ")";
            }
            if ((i + 1) % 4 == 0)
            {
                clonedPage = Instantiate(Page, PageParent.transform);
                clonedPage.SetActive(false);
                clonedPage.transform.name = "Page_0" + pageList.Count;
                pageList.Add(clonedPage);
            }

            barList.Add(clonedBar.GetComponent<ItemBar>());
        }
    }
    public void FlipPage(int direction)
    {
        if (direction != 1 && direction != -1)
        {
            Debug.LogWarning("direction must be +1 or -1");
            return;
        }

        int next = currentPage + direction;
        if (next < 0 || next >= pageList.Count) return;

        pageList[currentPage].SetActive(false);
        pageList[next].SetActive(true);
        currentPage = next; // ← 현재 페이지 업데이트 필요!
    }
}
