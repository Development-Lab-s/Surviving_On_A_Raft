using _00.Work.CheolYee._01.Codes.Managers;
using UnityEngine;

public class PlayerCritChanceZeroEvent : MonoBehaviour, IEvent
{
    [field: SerializeField] public GameEventType eventType { get; private set; }
    [field: SerializeField] public string eventEffectText { get; private set; }

    [field: SerializeField] public float EventDuration { get; private set; }

    [ContextMenu("나 운이 메이요")]
    public void StartEvent()
    {
        StatManager.Instance.PlayerBuffInTime(StatType.CritChance, -100, EventDuration);
    }

    public void StartEventEffectText()
    {
        EventUIManager.Instance.SetEventTextEffect(eventEffectText);
    }
}