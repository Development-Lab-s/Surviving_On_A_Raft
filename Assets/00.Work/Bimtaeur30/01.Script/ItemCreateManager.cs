using System.Collections.Generic;
using System.Linq;
using _00.Work.Nugusaeyo._Script.Cost;
using _00.Work.Resource.Manager;
using UnityEngine;

namespace _00.Work.Bimtaeur30._01.Script
{
    public class ItemCreateManager : MonoSingleton<ItemCreateManager>
    {
        // Cost[0~4] = { "구리", "강철", "황금", "보석", "마석" }
        public List<string> itemNames = new() { "구리", "강철", "황금", "보석", "마석" };
        [SerializeField] private ItemInvenGetAndRemove iigar;

        //public void ItemCreate(ExItemSO createItem)
        //{
        //    List<Ingredient> ItemIgdt = new List<Ingredient>();
        //}


        //IEnumerator waitaa()
        //{
        //    yield return new WaitForSeconds(5f);
        //    CreateItem(ExItem);
        //}

        public void CreateItem(ExItemSO item)
        {
            TestCostReset.Instance.ResetBarState();
            Dictionary<string, int> itemNeed = CheckCanCreateItem(item);

            if (itemNeed == null)
                return;

            bool isInvenEmpty = iigar.FindInventorySlot(item);
            for (int i = 0; i < itemNeed.Count; i++)
            {
                if (isInvenEmpty)
                {
                    int ind = itemNames.IndexOf(itemNeed.Keys.ElementAt(i));
                    CostManager.Instance.MinusCost(ind, itemNeed.Values.ElementAt(i));
                    //return;
                }
            }
            if (isInvenEmpty)
            {
                SoundManager.Instance.PlaySfx("UPGRADE");
            }
            //IIGAR.FindInventorySlot(Item);
        }
        
        public void CreateStartItem(ExItemSO item)
        {
            iigar.FindInventorySlot(item);
        }

        private Dictionary<string, int> CheckCanCreateItem(ExItemSO item)
        {
            if (item == null)
                return null;

            int[] costList = CostManager.Instance.Costs;

            List<Ingredient> igdtList = item.ItemIgdt;
            Dictionary<string, int> itemNeed = new Dictionary<string, int>();
            foreach (var t in igdtList)
            {
                string iname = t.Name;
                int iAmount = t.Amount;
                if (itemNames.Contains(iname))
                {
                    int ind = itemNames.IndexOf(iname);
                    if (costList[ind] >= iAmount)
                    {
                        itemNeed.Add(iname, iAmount);
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            return itemNeed;
        }
    }
}
