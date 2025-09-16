using UnityEngine;
using System.Collections.Generic;
using _00.Work.Resource.Manager;
using UnityEngine.Serialization;

public class TJ_ItemManager : MonoSingleton<TJ_ItemManager>
{
    public List<ExItemSO> attackItemList = new List<ExItemSO>();
    public List<ExItemSO> passiveItemList = new List<ExItemSO>();
}
