using System;
using UnityEngine;

public class TestCostDebugger : MonoBehaviour
{
    private CostSpawner _costSpawner;

    private void Awake()
    {
        _costSpawner = GetComponentInChildren<CostSpawner>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            _costSpawner.SpawnCost();
        }
    }
}
