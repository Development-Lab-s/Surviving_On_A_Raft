using Unity.Cinemachine;
using UnityEngine;

public class CameraEventListener : MonoBehaviour
{
    [SerializeField] private CameraEventSO _cameraEvent;
    private CinemachineBasicMultiChannelPerlin _camShake;
    private bool _zeroGain = false;

    private void Awake()
    {
        _camShake = GetComponent<CinemachineBasicMultiChannelPerlin>();
        if (_cameraEvent == null) return;
        _cameraEvent.OnCameraShaked += HandleCameraEvent;
    }

    private void OnDestroy()
    {
        _cameraEvent.OnCameraShaked -= HandleCameraEvent;
    }

    private void HandleCameraEvent(CameraEventData eventData)
    {
        if (eventData.gain == 0)
        {
            _zeroGain = true;
        }
        else
        {
            _camShake.AmplitudeGain = eventData.gain;
            _zeroGain = false;
        }
    }

    private void Update()
    {
        if (_zeroGain && _camShake.AmplitudeGain > 0)
        {
            _camShake.AmplitudeGain -= Time.deltaTime * 50;

            if (_camShake.AmplitudeGain <= 0)
            {
                _camShake.AmplitudeGain = 0;
                _zeroGain = false;
            }
        }
    }
}