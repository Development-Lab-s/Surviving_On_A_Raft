using Unity.VisualScripting;
using UnityEngine;

public interface IEvent
{
    public GameEventType eventType { get; }
    public string eventEffectText { get; }
    void StartEvent();
    void StartEventEffectText();

}
