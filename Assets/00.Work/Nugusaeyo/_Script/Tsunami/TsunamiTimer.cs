using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TsunamiTimer : MonoBehaviour
{
    public Action TsunamiAction;
    
    [SerializeField] private Image fill;
    [SerializeField] private Gradient gradient;
    [SerializeField] private TextMeshProUGUI timerText;

    [SerializeField] private float timerTime;
    private float _currentTime;

    private void Awake()
    {
        _currentTime = 0f;
    }

    private void Update()
    {
        if (Keyboard.current.iKey.wasPressedThisFrame)
        {
            timerTime--;
        }
        if (Keyboard.current.oKey.wasPressedThisFrame)
        {
            timerTime++;
        }
        
        _currentTime += Time.deltaTime;
        
        timerText.text = $"{(int)(_currentTime / 60):D1}:{(int)(_currentTime % 60):D2}";
            
        fill.color = gradient.Evaluate(_currentTime / timerTime);
        fill.fillAmount = Mathf.Lerp(fill.fillAmount, _currentTime / timerTime, Time.deltaTime * 5f);

        if (fill.fillAmount >= 1)
        {
            TsunamiAction?.Invoke();
            _currentTime = 0;
        }
    }
}
