using System;
using UnityEngine;

public class CostManager : MonoBehaviour
{
    public Action CostUpEvent;

    public int[] Costs { get; private set; } = new int[5];
    static public CostManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GetCost(int costType, int value)
    {
        Costs[costType] += IncreaseCost(value);
        CostUpEvent?.Invoke();
    }

    public void MinusCost(int costType, int value)
    {
        Costs[costType] -= IncreaseCost(value);
        CostUpEvent?.Invoke();
    }

    private int IncreaseCost(int value)
    {
        //여기서 여러 이벤트 알아서 실행
        return value;
    }
}
