using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestCostDebugger : MonoBehaviour
{
    private CostSpawner _costSpawner;

    private void Awake()
    {
        _costSpawner = GetComponentInChildren<CostSpawner>();
    }

    void Update()
    {
        if (Keyboard.current.jKey.wasPressedThisFrame)
        {
            _costSpawner.SpawnCost(transform.position);
        }
    }
}
