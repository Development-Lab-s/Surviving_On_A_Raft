using _00.Work.Nugusaeyo._Script.Cost;
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
            CostManager.Instance.MinusCost(Random.Range(0, 5), 1);
        }
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            CostManager.Instance.PlusCost(Random.Range(0, 5), 100);
        }
    }
}
