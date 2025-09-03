using UnityEngine;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;

public class ItemCreatetor : MonoBehaviour
{
    [SerializeField] private GameObject PageParent;
    [SerializeField] private GameObject Page;
    [SerializeField] private GameObject CreateBar;

    private void Start()
    {
        SetItemCreateBar();
    }

    private void SetItemCreateBar()
    {
        GameObject clonedPage = Instantiate(Page, PageParent.transform);
        clonedPage.transform.name = "Page_01";
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
                clonedPage.transform.name = "Page_0" + i + 1 / 4;
            }

        }
    }
}
