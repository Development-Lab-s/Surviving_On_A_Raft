using System;
using System.Collections.Generic;
using _00.Work.Resource.Manager;
using _00.Work.Resource.SO;
using UnityEngine;
using Random = UnityEngine.Random;

public class CostSpawner : MonoBehaviour
{
    public void SpawnCost(Vector2 spawnPosition)
    {
        
        int rand = Random.Range(0, 100);
        int amount = 0;
        if (rand > 30)
        {
            amount = 8;
        }
        else if (rand > 80)
        {
            amount = 12;
        }
        else
        {
            amount = 15;
        }
        
        for (int i = 0; i < amount; i++)
        {
            IPoolable cost = PoolManager.Instance.Pop($"ItemCost");
            cost.GameObject.transform.position = spawnPosition;
        }
    }
}
