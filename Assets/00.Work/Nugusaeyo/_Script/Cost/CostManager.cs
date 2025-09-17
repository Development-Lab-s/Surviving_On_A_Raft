using System;
using System.Collections.Generic;
using _00.Work.Resource.Manager;
using UnityEngine;

namespace _00.Work.Nugusaeyo._Script.Cost
{
    public class CostManager : MonoSingleton<CostManager>
    {
        public Action CostUpEvent;
        public Action CostDownEvent;

        public int[] Costs { get; private set; } = new int[5];
        public List<string> costNames = new List<string> { "구리", "강철", "황금", "보석", "마석" };

        [ContextMenu("코스트복사")]
        public void ChitCost()
        {
            PlusCost(0, 200);
            PlusCost(1, 200);
            PlusCost(2, 200);
            PlusCost(3, 200);
            PlusCost(4, 200);
        }
        
        public void PlusCost(int costType, int value)
        {
            TestCostReset.Instance.ResetBarState();
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
            TestCostReset.Instance.ResetBarState();
            Costs[costType] -= IncreaseCost(value);
            if (Costs[costType] < 0)
            {
                Costs[costType] = 0;
            }
            CostDownEvent?.Invoke();
        }

        public bool IsPaid(int costType, int value)
        {
            if (Costs[costType] < value) return false;
            return true;
        }

        private int IncreaseCost(int value)
        {
            //여기서 여러 이벤트 알아서 실행
            return value;
        }
    }
}
