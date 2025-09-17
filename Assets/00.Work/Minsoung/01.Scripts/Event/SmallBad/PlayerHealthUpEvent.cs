using _00.Work.CheolYee._01.Codes.Managers;
using UnityEngine;

public class PlayerHealthUpEvent : MonoBehaviour, IEvent
{
    [field: SerializeField] public GameEventType eventType { get; private set; }
    [field: SerializeField] public string eventEffectText { get; private set; }

    [field: SerializeField] public float EventDuration { get; private set; }

    [ContextMenu("나 약간 통통")]
    public void StartEvent()
    {
        StatManager.Instance.PlayerBuffInTime(StatType.Health, 0.2f, EventDuration);
    }

    public void StartEventEffectText()
    {
        EventUIManager.Instance.SetEventTextEffect(eventEffectText);
    }
}
