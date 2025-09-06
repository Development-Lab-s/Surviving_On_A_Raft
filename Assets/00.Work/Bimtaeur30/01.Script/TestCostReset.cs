using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class TestCostReset : MonoBehaviour
{
    [SerializeField] private ItemCreatetorBars ItemCreatetorBars;


    [ContextMenu("테스트 실행")]
    public void ResetBarState()
    {
        int barCount = ItemCreatetorBars.barList.Count;
        for (int i = 0; i < barCount; i++)
        {
            ItemBar bar = ItemCreatetorBars.barList[i];
            bool isCostPerfact = true;
            List<Ingredient> igdtList = bar.MyItem.ItemIgdt;
            for (int j = 0; j < igdtList.Count; j++)
            {
                string iname = igdtList[j].Name;
                int iamount = igdtList[j].Amount;
                int ind = CostManager.instance.CostNames.IndexOf(iname);
                if (CostManager.instance.Costs[ind] < iamount)
                {
                    isCostPerfact = false;
                    break;
                }
            }
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
