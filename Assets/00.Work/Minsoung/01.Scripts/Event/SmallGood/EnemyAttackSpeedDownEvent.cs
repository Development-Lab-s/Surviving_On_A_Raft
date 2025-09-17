using _00.Work.CheolYee._01.Codes.Managers;
using UnityEngine;

public class EnemyAttackSpeedDownEvent : MonoBehaviour, IEvent
{
    [field: SerializeField] public GameEventType eventType { get; private set; }
    [field: SerializeField] public string eventEffectText { get; private set; }

    [field: SerializeField] public float EventDuration { get; private set; }

    [ContextMenu("나 공격 오소이")]
    public void StartEvent()
    {
        StatManager.Instance.EnemyBuffInTime(StatType.AttackSpeed, 0.8f, EventDuration);
    }

    public void StartEventEffectText()
    {
        EventUIManager.Instance.SetEventTextEffect(eventEffectText);
    }
}