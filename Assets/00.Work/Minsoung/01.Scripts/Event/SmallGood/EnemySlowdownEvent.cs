using _00.Work.CheolYee._01.Codes.Managers;
using UnityEngine;

public class EnemySlowdownEvent : MonoBehaviour, IEvent
{
    [field: SerializeField] public GameEventType eventType { get; private set; }
    [field: SerializeField] public string eventEffectText { get; private set; }

    [field:SerializeField] public float EventDuration { get;private set; }

    [ContextMenu("적 오소이..")]
    public void StartEvent()
    {
        StatManager.Instance.EnemyBuffInTime(StatType.MoveSpeed, 0.8f,EventDuration);
        EventTimer.Instance.StartTimer(EventDuration);
    }

    public void StartEventEffectText()
    {
        EventUIManager.Instance.SetEventTextEffect(eventEffectText);
    }
}
