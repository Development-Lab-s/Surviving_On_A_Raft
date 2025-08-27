using UnityEngine;

public class CostDebugger : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CostManager.instance.GetCost((Random.Range(0, 5)), Random.Range(1, 101));
        }
    }
}
