using System;
using _00.Work.Nugusaeyo._Script.Cost;
using UnityEngine;

public class AddCost : MonoBehaviour
{
    private void Start()
    {
        CostManager.Instance.PlusCost(1, 100);
        CostManager.Instance.PlusCost(2, 100);
        CostManager.Instance.PlusCost(3, 100);
        CostManager.Instance.PlusCost(4, 100);
        CostManager.Instance.PlusCost(5, 100);
    }
}
