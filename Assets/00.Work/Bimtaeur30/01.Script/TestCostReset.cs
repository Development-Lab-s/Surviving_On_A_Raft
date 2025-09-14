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
                int ind = CostManager.Instance.costNames.IndexOf(iname);
                if (CostManager.Instance.Costs[ind] < iamount)
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
