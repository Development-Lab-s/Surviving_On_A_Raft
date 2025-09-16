using _00.Work.CheolYee._01.Codes.Managers;
using UnityEngine;

public class PlayerCritChanceUpEvent : MonoBehaviour, IEvent
{
    [field: SerializeField] public GameEventType eventType { get; private set; }
    [field: SerializeField] public string eventEffectText { get; private set; }

    [field: SerializeField] public float EventDuration { get; private set; }

    [ContextMenu("나 가끔 츠요이")]
    public void StartEvent()
    {
        StatManager.Instance.PlayerBuffInTime(StatType.CritChance, 0.5f, EventDuration);
        EventTimer.Instance.StartTimer(EventDuration);
    }

    public void StartEventEffectText()
    {
        EventUIManager.Instance.SetEventTextEffect(eventEffectText);
    }
}
