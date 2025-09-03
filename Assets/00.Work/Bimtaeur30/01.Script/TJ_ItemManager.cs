using UnityEngine;
using System.Collections.Generic;

public class TJ_ItemManager : MonoBehaviour
{
    public static TJ_ItemManager Instance { get; private set; }
    public List<ExItemSO> ItemList = new List<ExItemSO>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
}
