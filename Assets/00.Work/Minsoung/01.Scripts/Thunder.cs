using UnityEngine;

public class Thunder : MonoBehaviour, IEvent
{
    [field: SerializeField] public GameEventType eventType { get; private set; }

    public void StartEvent()
    {
        // 이벤트 구현
    }

}
