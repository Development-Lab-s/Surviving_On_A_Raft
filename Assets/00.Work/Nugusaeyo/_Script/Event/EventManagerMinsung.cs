using System;
using UnityEngine;

public class EventManagerMinsung : MonoBehaviour
{
    [SerializeField] private GameObject objEvent;
    [SerializeField] private GameObject objEvent2;
    private void Start()
    {
        objEvent.TryGetComponent<IEventMinsung>(out IEventMinsung eventMinsung);
        eventMinsung.StartEvent();
        objEvent2.TryGetComponent<IEventMinsung>(out IEventMinsung event2Minsung);
        event2Minsung.StartEvent();
    }
}
