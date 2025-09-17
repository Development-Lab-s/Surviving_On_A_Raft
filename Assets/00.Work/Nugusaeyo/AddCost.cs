using System;
using _00.Work.Nugusaeyo._Script.Cost;
using UnityEngine;

public class AddCost : MonoBehaviour
{
    private void Start()
    {
        CostManager.Instance.PlusCost(1, 6);
        CostManager.Instance.PlusCost(3, 2);
        CostManager.Instance.PlusCost(4, 3);
    }
}
