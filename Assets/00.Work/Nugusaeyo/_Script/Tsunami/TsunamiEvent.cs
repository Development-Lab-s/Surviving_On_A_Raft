using System;
using UnityEngine;

public class TsunamiEvent : MonoBehaviour
{
    private TsunamiTimer _tsunamiTimer;
    
    public int CurrentTsunamiLevel { get; private set; }
    public int currentLevel = 1;

    private void Awake()
    {
        _tsunamiTimer.TsunamiAction += HandleTsunamiAction;
    }

    private void HandleTsunamiAction()
    {
        
    }

    private void TsunamiUp()
    {
        CurrentTsunamiLevel++;
        if (CurrentTsunamiLevel >= currentLevel)
        {
            
        }
    }
}
