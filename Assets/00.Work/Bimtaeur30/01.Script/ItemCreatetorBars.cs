using System.Collections.Generic;
using _00.Work.Resource.Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _00.Work.Bimtaeur30._01.Script
{
    public class ItemCreatetorBars : MonoSingleton<ItemCreatetorBars>
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
            // 공격템 전용 페이지
            CreateItemBars(TJ_ItemManager.Instance.attackItemList, "공격템");

            // 패시브템 시작할 때 무조건 새 페이지 생성
            CreateItemBars(TJ_ItemManager.Instance.passiveItemList, "패시브템", true);
            foreach (var page in pageList)
                page.SetActive(false);

            // 첫 페이지만 활성화
            if (pageList.Count > 0)
            {
                pageList[0].SetActive(true);
                currentPage = 0;
            }
        }

        private void CreateItemBars(List<ExItemSO> itemList, string category, bool forceNewPage = false)
        {
            GameObject clonedPage;

            // 기존 페이지 이어쓰기 or 무조건 새로 시작
            if (forceNewPage || pageList.Count == 0)
            {
                clonedPage = Instantiate(Page, PageParent.transform);
                clonedPage.transform.name = "Page_" + pageList.Count;
                pageList.Add(clonedPage);
            }
            else
            {
                clonedPage = pageList[pageList.Count - 1]; // 마지막 페이지 이어쓰기
            }

            for (int i = 0; i < itemList.Count; i++)
            {
                GameObject clonedBar = Instantiate(CreateBar, clonedPage.transform);
                ItemBar bar = clonedBar.GetComponent<ItemBar>();
                bar.MyItem = itemList[i];

                // UI 세팅
                clonedBar.transform.Find("ItemNameTxt").GetComponent<TextMeshProUGUI>().text = itemList[i].ItemName;
                clonedBar.transform.Find("ItemDescriptionTxt").GetComponent<TextMeshProUGUI>().text = itemList[i].ItemDescription;
                clonedBar.transform.Find("IconBaseImage/Image").GetComponent<Image>().sprite = itemList[i].ItemImage;

                GameObject MeatBallDes = clonedBar.transform.Find("MeatballDes").gameObject;
                MeatBallDes.SetActive(false);
                MeatBallDes.transform.Find("AtTxt").GetComponent<TextMeshProUGUI>().text = "분류: " + category;

                TextMeshProUGUI sourceCountTxt = clonedBar.transform.Find("SourceCountTxt").GetComponent<TextMeshProUGUI>();
                sourceCountTxt.text = "";
                foreach (var igdt in itemList[i].ItemIgdt)
                {
                    sourceCountTxt.text += $" {igdt.Name}({igdt.Amount})";
                }

                // 4개마다 새 페이지 생성
                if ((i + 1) % 4 == 0 && i != itemList.Count - 1)
                {
                    clonedPage = Instantiate(Page, PageParent.transform);
                    clonedPage.SetActive(false);
                    clonedPage.transform.name = "Page_" + pageList.Count;
                    pageList.Add(clonedPage);
                }

                barList.Add(bar);
            }
        }
        
        public void UpdateAttackItemUI(int index)
        {
            if (index < 0 || index >= TJ_ItemManager.Instance.attackItemList.Count)
            {
                Debug.LogWarning("잘못된 어택 아이템 인덱스");
                return;
            }

            ExItemSO item = TJ_ItemManager.Instance.attackItemList[index];

            // 업그레이드가 가능하다면 nextExItemSo 반영
            if (item.nextItem != null && item.ItemLevel < 5)
            {
                item = item.nextItem;
                TJ_ItemManager.Instance.attackItemList[index] = item; // 리스트에도 반영
            }

            ItemBar targetBar = barList[index];
            ApplyItemDataToBar(item, targetBar, item.itemAttributes[0]);
        }

        
        public void UpdatePassiveItemUI(int index)
        {
            if (index < 0 || index >= TJ_ItemManager.Instance.passiveItemList.Count)
            {
                Debug.LogWarning("잘못된 패시브 아이템 인덱스");
                return;
            }

            int offset = TJ_ItemManager.Instance.attackItemList.Count;
            ExItemSO item = TJ_ItemManager.Instance.passiveItemList[index];

            if (item.nextItem != null && item.ItemLevel < 5)
            {
                item = item.nextItem;
                TJ_ItemManager.Instance.passiveItemList[index] = item; // 리스트에도 반영
            }

            ItemBar targetBar = barList[offset + index];
            ApplyItemDataToBar(item, targetBar, "패시브템");
        }

        private void ApplyItemDataToBar(ExItemSO item, ItemBar bar, string categoryName)
        {
            // MyItem 업데이트
            bar.MyItem = item;

            // UI 요소 찾아오기
            TextMeshProUGUI itemNameTxt = bar.transform.Find("ItemNameTxt").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI itemDescriptionTxt = bar.transform.Find("ItemDescriptionTxt").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI sourceCountTxt = bar.transform.Find("SourceCountTxt").GetComponent<TextMeshProUGUI>();
            Image itemImage = bar.transform.Find("IconBaseImage").transform.Find("Image").GetComponent<Image>();
            GameObject MeatBallDes = bar.transform.Find("MeatballDes").gameObject;
            TextMeshProUGUI AtTxt = MeatBallDes.transform.Find("AtTxt").GetComponent<TextMeshProUGUI>();

            // 분류
            AtTxt.text = categoryName;
            MeatBallDes.SetActive(false);

            // 텍스트 갱신
            itemNameTxt.text = item.ItemName;
            itemDescriptionTxt.text = item.ItemDescription;

            // 아이콘
            itemImage.sprite = item.ItemImage;

            // 재료 텍스트 초기화 후 다시 채우기
            sourceCountTxt.text = "";
            for (int j = 0; j < item.ItemIgdt.Count; j++)
            {
                sourceCountTxt.text += " " + item.ItemIgdt[j].Name + "(" + item.ItemIgdt[j].Amount + ")";
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
}
