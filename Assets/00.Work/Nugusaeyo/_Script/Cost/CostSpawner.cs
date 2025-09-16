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
        int dropItemRand = Random.Range(0, 100);
        int amount = 0;

        if (dropItemRand < 20)
        {
            amount = 0;
        }
        else if (dropItemRand < 80)
        {
            amount = 6;
        }
        else
        {
            amount = 10;
        }
        
        for (int i = 0; i < amount; i++)
        {
            IPoolable cost = PoolManager.Instance.Pop($"ItemCost");
            cost.GameObject.transform.position = spawnPosition;
        }
    }
}
