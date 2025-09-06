using Unity.VisualScripting;
using UnityEngine;

public class NextDungeon : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SpawnManager.Instance.StartCycle();
        }
    }
}
