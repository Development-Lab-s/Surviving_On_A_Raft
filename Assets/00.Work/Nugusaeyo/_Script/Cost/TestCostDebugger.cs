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
        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            CostManager.instance.MinusCost(Random.Range(0, 5), 1);
        }
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            CostManager.instance.PlusCost(Random.Range(0, 5), 100);
        }
    }
}
