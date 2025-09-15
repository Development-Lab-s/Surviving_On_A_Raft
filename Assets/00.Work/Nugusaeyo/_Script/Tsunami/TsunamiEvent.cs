using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TsunamiEvent : MonoBehaviour
{
    public UnityEvent tsunamiEndingEvent;
    public UnityEvent tsunamiSafe;
    
    [SerializeField] private Image tsunamiImage;
    [SerializeField] private Image backTsunamiImage;
    public TsunamiTimer TsunamiTimer { get; private set; }
    public MiniMapStageUp MiniMapStageUp { get; private set; }
    private float _currentWaveHeight;
    public int CurrentTsunamiLevel { get; private set; }

    private void Awake()
    {
        TsunamiTimer = GetComponent<TsunamiTimer>();
        MiniMapStageUp = GetComponent<MiniMapStageUp>();
        CurrentTsunamiLevel = 1;
    }

    private void Start()
    {
        TsunamiTimer.TsunamiAction += HandleTsunamiAction;
        MiniMapStageUp.FloorChanged += HandleFloorChanged;
        _currentWaveHeight = 0.3f;
    }

    private void OnDestroy()
    {
        TsunamiTimer.TsunamiAction -= HandleTsunamiAction;
        MiniMapStageUp.FloorChanged -= HandleFloorChanged;
    }

    private void HandleTsunamiAction()
    {
        TsunamiUp();
    }

    private void HandleFloorChanged()
    {
        if (CurrentTsunamiLevel + 1 < MiniMapStageUp.Instance.CurrentFloor)
        {
            _currentWaveHeight = 0f;
        }
        else if (CurrentTsunamiLevel + 1 == MiniMapStageUp.Instance.CurrentFloor)
        {
            _currentWaveHeight = 0.3f;
        }
        else
        {
            _currentWaveHeight = 1f;
            return;
        }
        tsunamiSafe?.Invoke();
    }

    private void TsunamiUp()
    {
        CurrentTsunamiLevel++;
        if (CurrentTsunamiLevel >= MiniMapStageUp.Instance.CurrentFloor)
        {
            _currentWaveHeight = 1f;
            tsunamiEndingEvent?.Invoke();
        }
        else if (CurrentTsunamiLevel + 1 == MiniMapStageUp.Instance.CurrentFloor)
        {
            _currentWaveHeight = 0.3f;
        }
        else
        {
            _currentWaveHeight = 0f;
        }
    }

    private void Update()
    {
        tsunamiImage.fillAmount = Mathf.Lerp(tsunamiImage.fillAmount, _currentWaveHeight, Time.deltaTime * 5f);
        backTsunamiImage.fillAmount = Mathf.Lerp(backTsunamiImage.fillAmount, _currentWaveHeight, Time.deltaTime * 5f);
    }
}
