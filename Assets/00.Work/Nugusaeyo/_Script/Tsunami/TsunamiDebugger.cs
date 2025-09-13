using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TsunamiDebugger : MonoBehaviour
{
    private int testFloor = 1;
    private void Update()
    {
        if (Keyboard.current.mKey.wasPressedThisFrame)
        {
            testFloor++;
            TsunamiEventManager.Instance.LadderInteracted(testFloor);
            Debug.Log("< Debugging > Floor Up");
        }
    }
}
