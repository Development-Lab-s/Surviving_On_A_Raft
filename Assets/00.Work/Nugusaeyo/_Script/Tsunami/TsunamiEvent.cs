using System;
using UnityEngine;
using UnityEngine.UI;

public class TsunamiEvent : MonoBehaviour
{
    [SerializeField] private Image tsunamiImage;
    [SerializeField] private Image backTsunamiImage;
    private TsunamiTimer _tsunamiTimer;
    private MiniMapStageUp _miniMapStageUp;
    private float _currentWaveHeight;
    public int CurrentTsunamiLevel { get; private set; }

    private void Awake()
    {
        _tsunamiTimer = GetComponent<TsunamiTimer>();
        _miniMapStageUp = GetComponent<MiniMapStageUp>();
        CurrentTsunamiLevel = 0;
    }

    private void Start()
    {
        _tsunamiTimer.TsunamiAction += HandleTsunamiAction;
        _miniMapStageUp.FloorChanged += HandleFloorChanged;
        _currentWaveHeight = 0.3f;
    }

    private void OnDestroy()
    {
        _tsunamiTimer.TsunamiAction -= HandleTsunamiAction;
        _miniMapStageUp.FloorChanged -= HandleFloorChanged;
    }

    private void HandleTsunamiAction()
    {
        TsunamiUp();
    }

    private void HandleFloorChanged()
    {
        if (CurrentTsunamiLevel + 1 < MiniMapStageUp.Instance.CurrentFloor)
        {
            Debug.Log("아무 일도 없엇따");
            _currentWaveHeight = 0.0f;
        }
    }

    private void TsunamiUp()
    {
        CurrentTsunamiLevel++;
        if (CurrentTsunamiLevel == MiniMapStageUp.Instance.CurrentFloor)
        {
            Debug.Log("죽엇따");
            _currentWaveHeight = 1f;
        }
        else if (CurrentTsunamiLevel + 1 == MiniMapStageUp.Instance.CurrentFloor)
        {
            Debug.Log("조금 일이 있엇따");
            _currentWaveHeight = 0.3f;
        }
    }

    private void Update()
    {
        tsunamiImage.fillAmount = Mathf.Lerp(tsunamiImage.fillAmount, _currentWaveHeight, Time.deltaTime * 5f);
        backTsunamiImage.fillAmount = Mathf.Lerp(backTsunamiImage.fillAmount, _currentWaveHeight, Time.deltaTime * 5f);
    }
}
