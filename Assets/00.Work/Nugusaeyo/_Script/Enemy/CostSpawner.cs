using System.Collections.Generic;
using UnityEngine;

public class CostSpawner : MonoBehaviour
{
    public List<GameObject> spreadCost = new List<GameObject>();

    public void SpawnCost()
    {
        int amount = Random.Range(0, 3);
        
        Debug.Log($"Cost Spawned, Amount : {amount}");
        
        for (int i = 0; i < amount; i++)
        {
            int costType = Random.Range(1, 101);
            
            if (costType < 2) // 1% 확률로 코스트 타입 E 생성
            {
                costType = 4;
            }
            else if (costType < 7) // 5% 확률로 코스트 타입 D 생성
            {
                costType = 3;
            }
            else if (costType < 22) // 15% 확률로 코스트 타입 C 생성
            {
                costType = 2;
            }
            else if (costType < 52) // 30% 확률로 B 생성 
            {
                costType = 1;
            }
            else //  39% 확률로 A 생성
            {
                costType = 0;
            }
            Instantiate(spreadCost[costType], transform.position, Quaternion.identity);
        }
    }
}
