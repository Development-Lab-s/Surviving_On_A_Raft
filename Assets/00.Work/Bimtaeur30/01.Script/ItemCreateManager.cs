using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Unity.VisualScripting.Dependencies.Sqlite.SQLite3;
using static UnityEngine.Rendering.DebugUI;

public class ItemCreateManager : MonoBehaviour
{
    public static ItemCreateManager Instance { get; private set; }

    // Cost[0~4] = ����, ��ö, Ȳ��, ����, ����
    public List<string> ItemNames = new List<string>() { "����", "��ö", "Ȳ��", "����", "����" };
    [SerializeField] private ExItemSO ExItem;
    [SerializeField] private ItemInvenGetAndRemove IIGAR;
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
        CostManager.instance.PlusCost(2, 100);
        CostManager.instance.PlusCost(0, 100);
        CostManager.instance.PlusCost(1, 100);
        CostManager.instance.PlusCost(3, 100);
        CostManager.instance.PlusCost(4, 100);
        //StartCoroutine(waitaa());
    }

    //public void ItemCreate(ExItemSO createItem)
    //{
    //    List<Ingredient> ItemIgdt = new List<Ingredient>();
    //}


    //IEnumerator waitaa()
    //{
    //    yield return new WaitForSeconds(5f);
    //    CreateItem(ExItem);
    //}

    public void CreateItem(ExItemSO Item)
    {
            Dictionary<string, int> itemNeed = CheckCanCreateItem(Item);

            if (itemNeed == null)
                return;

        bool isInvenEmpty = IIGAR.FindInventorySlot(Item);
        for (int i = 0; i < itemNeed.Count; i++)
            {
                if (isInvenEmpty)
                {
                    int ind = ItemNames.IndexOf(itemNeed.Keys.ElementAt(i));
                    CostManager.instance.MinusCost(ind, itemNeed.Values.ElementAt(i));
                    //return;
                }
            }
            //IIGAR.FindInventorySlot(Item);
    }

    public Dictionary<string, int> CheckCanCreateItem(ExItemSO Item)
    {
        if (Item == null)
            return null;

        int[] costList = CostManager.instance.Costs;

        List<Ingredient> igdtList = Item.ItemIgdt;
        Dictionary<string, int> itemNeed = new Dictionary<string, int>();
        for (int i = 0; i < igdtList.Count; i++)
        {
            string iname = igdtList[i].Name;
            int iAmount = igdtList[i].Amount;
            if (ItemNames.Contains(iname))
            {
                int ind = ItemNames.IndexOf(iname);
                if (costList[ind] >= iAmount)
                {
                    itemNeed.Add(iname, iAmount);
                }
                else
                {
                    Debug.Log(iname + " �ڿ� ���� ����, "+ costList[ind] + " " + iAmount);
                    return null;
                }
            }
            else
            {
                Debug.LogAssertion("������ ExSO ��� �̸� �����ϼ���");
                return null;
            }
        }
        return itemNeed;
    }
}
