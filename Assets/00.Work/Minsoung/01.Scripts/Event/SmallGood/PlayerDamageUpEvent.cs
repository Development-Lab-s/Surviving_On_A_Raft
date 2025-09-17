using _00.Work.CheolYee._01.Codes.Managers;
using UnityEngine;

public class PlayerDamageUpEvent : MonoBehaviour, IEvent
{
    [field: SerializeField] public GameEventType eventType { get; private set; }
    [field: SerializeField] public string eventEffectText { get; private set; }

    [field: SerializeField] public float EventDuration { get; private set; }

    [ContextMenu("나 약간 츠요이")]
    public void StartEvent()
    {
        StatManager.Instance.PlayerBuffInTime(StatType.Damage, 0.2f, EventDuration);
        EventTimer.Instance.StartTimer(EventDuration);
    }

    public void StartEventEffectText()
    {
        EventUIManager.Instance.SetEventTextEffect(eventEffectText);
    }
}
