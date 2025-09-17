using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class EventTimerDebugger : MonoBehaviour
{
    private void Start()
    {
        EventTimer.Instance.SetLoadingTime(7f); //다음 이벤트까지 걸리는 시간
    }

    private void Update()
    {
        if (Keyboard.current.gKey.wasPressedThisFrame)
        {
            EventTimer.Instance.StartTimer(5f); //이벤트 실행 시간
            //따라서 7-5인 2초동안 빨간색 원이 돌아가게 됨.
        }
    }
}
