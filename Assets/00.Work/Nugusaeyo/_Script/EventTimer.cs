using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EventTimer : MonoBehaviour
{
    [SerializeField] private Image rollingRealImage;
    [SerializeField] private Image waitCircle;
    [SerializeField] private Image backGround;

    public Color reloadColor;
    public Color waitingColor;

    private float _lastTime;
    private float _loadTime;
    private float _endTime;
    private float _aaa;
    private bool _isWaiting;
    
    public float WaitingTime { get; private set; }
    public float LoadingTime { get; private set; }

    public static EventTimer Instance;

    private void Awake()
    {
        _lastTime = 0f;
        _isWaiting = true;

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        rollingRealImage.color = reloadColor;
        waitCircle.color = waitingColor;
        backGround.color = reloadColor;
    }

    public void SetLoadingTime(float time)
    {
        LoadingTime = time;
    }

    private void Update()
    {
        if (_isWaiting)
        {

            if (rollingRealImage.fillAmount <= 0)
            {
                _loadTime -= Time.deltaTime;
                waitCircle.fillAmount = Mathf.Lerp(waitCircle.fillAmount, _loadTime / _aaa, Time.deltaTime * 5);
                if (waitCircle.fillAmount <= 0)
                {
                    _isWaiting = false;
                    waitCircle.fillAmount = 1;
                    rollingRealImage.fillAmount = 1;
                }
            }
            else
            {
                _lastTime -= Time.deltaTime;
                rollingRealImage.fillAmount = Mathf.Lerp(rollingRealImage.fillAmount, _lastTime / WaitingTime, Time.deltaTime * 5);
            }
            
        }
    }

    public void StartTimer(float time)
    {
        WaitingTime = time;
        _lastTime = time;
        _isWaiting = true;
        rollingRealImage.fillAmount = 1;
        waitCircle.fillAmount = 1;
        _loadTime = LoadingTime - time;
        _aaa = _loadTime;
    }
}
