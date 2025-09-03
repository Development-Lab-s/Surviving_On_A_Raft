using System;
using UnityEngine;

public struct CameraEventData
{
    public float gain;
}

[CreateAssetMenu(fileName = "CameraEvent", menuName = "SO/CameraEvent")]
public class CameraEventSO : ScriptableObject
{
    public Action<CameraEventData> OnCameraShaked;
    public void RaiseEvent(CameraEventData eventData)
    {
        OnCameraShaked?.Invoke(eventData);
    }
}
