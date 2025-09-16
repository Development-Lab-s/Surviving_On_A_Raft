using System;
using _00.Work.Nugusaeyo._Script.Cost;
using UnityEngine;

public class AddCost : MonoBehaviour
{
    private void Start()
    {
        CostManager.Instance.PlusCost(0, 1000);
        CostManager.Instance.PlusCost(1, 1000);
        CostManager.Instance.PlusCost(2, 1000);
        CostManager.Instance.PlusCost(3, 1000);
        CostManager.Instance.PlusCost(4, 1000);
    }
}
