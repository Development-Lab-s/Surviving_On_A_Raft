using UnityEngine;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using System;

public class ItemCreatetorBars : MonoBehaviour
{
    [SerializeField] private GameObject PageParent;
    [SerializeField] private GameObject Page;
    [SerializeField] private GameObject CreateBar;
    private int currentPage = 0;

    public List<GameObject> pageList = new List<GameObject>();

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
            TextMeshProUGUI itemNameTxt = clonedBar.transform.Find("ItemNameTxt").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI itemDescriptionTxt = clonedBar.transform.Find("ItemDescriptionTxt").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI sourceCountTxt = clonedBar.transform.Find("SourceCountTxt").GetComponent<TextMeshProUGUI>();
            itemNameTxt.text = items[i].ItemName;
            itemDescriptionTxt.text = items[i].ItemDescription;
            int sourceCount = items[i].ItemIgdt.Count;
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
