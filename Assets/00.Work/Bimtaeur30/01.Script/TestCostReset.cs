using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using _00.Work.Bimtaeur30._01.Script;
using _00.Work.Nugusaeyo._Script.Cost;

public class TestCostReset : MonoBehaviour
{
    [SerializeField] private ItemCreatetorBars ItemCreatetorBars;


    [ContextMenu("�׽�Ʈ ����")]
    public void ResetBarState()
    {
        PlayerItem[] itemlist = InventoryManager.Instance.ItemSlotList;
        int barCount = ItemCreatetorBars.barList.Count;
        for (int i = 0; i < barCount; i++)
        {
            //bool _isItemBeing = InventoryManager.Instance.ItemSlotList.
            ItemBar bar = ItemCreatetorBars.barList[i];

            bool isCostPerfact = true;
            List<Ingredient> igdtList = bar.MyItem.ItemIgdt;
            for (int j = 0; j < igdtList.Count; j++)
            {
                string iname = igdtList[j].Name;
                int iamount = igdtList[j].Amount;
                int ind = CostManager.Instance.costNames.IndexOf(iname);
                if (CostManager.Instance.Costs[ind] < iamount)
                {
                    isCostPerfact = false;
                    break;
                }
            }



            {
                bool found = false;

                foreach (PlayerItem item in itemlist)
                {
                    if (item == null) continue;

                    if (bar.MyItem.ItemName == item.Template.ItemName)
                    {
                        Debug.Log(item.Template.name + ": " + item);

                        if (item.Level == 5)
                        {
                            ItemCreatetorBars.barList[i].SetStateMaxUpgrade();
                            Debug.Log("최대치");
                        }
                        else
                        {
                            if (isCostPerfact == false)
                            {
                                ItemCreatetorBars.barList[i].SetStateCreateDisabled();
                            }
                            else
                            {
                                ItemCreatetorBars.barList[i].SetStateUpgrade();
                                Debug.Log("업글");
                            }
                        }

                        found = true;
                        break; // 상태 정했으면 더 안 봐도 됨
                    }
                }

                // 아이템이 하나도 안 맞았다면 새로 생성 상태
                if (!found)
                {
                    if (isCostPerfact == false)
                    {
                        ItemCreatetorBars.barList[i].SetStateCreateDisabled();
                    }
                    else
                    {
                        ItemCreatetorBars.barList[i].SetStateCreate();

                    }
                }
            }
        
        }
    }
}
