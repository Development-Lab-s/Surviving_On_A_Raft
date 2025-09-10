using NUnit.Framework;
using System;
using UnityEngine;
using System.Collections.Generic;

public class CostManager : MonoBehaviour
{
    public Action CostUpEvent;
    public Action CostDownEvent;

    public int[] Costs { get; private set; } = new int[5];
    public List<string> CostNames = new List<string> { "구리", "강철", "황금", "보석", "마석" };
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

    public void PlusCost(int costType, int value)
    {
        if (costType < 999)
        {
            if (costType + value > 999)
            {
                Costs[costType] = IncreaseCost(999);
            }
            else
            {
                Costs[costType] += IncreaseCost(value);
            }
            CostUpEvent?.Invoke();
        }
    }

    public void MinusCost(int costType, int value)
    {
        Costs[costType] -= IncreaseCost(value);
        if (Costs[costType] < 0)
        {
            Debug.LogError($"코스트 {costType} 음수 돌파됨, 값 {Costs[costType]} 초과. 0으로 설정됨.");
            Costs[costType] = 0;
        }
        CostDownEvent?.Invoke();
    }

    private int IncreaseCost(int value)
    {
        //여기서 여러 이벤트 알아서 실행
        return value;
    }
}
