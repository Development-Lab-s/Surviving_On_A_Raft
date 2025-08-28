using Unity.VisualScripting;
using UnityEngine;

public interface IEvent
{
    public GameEventType eventType { get; }
    void StartEvent();
}
