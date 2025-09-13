using System;
using UnityEngine;

public class TsunamiEventManager : MonoBehaviour
{
    [SerializeField] private TsunamiEvent tsunamiEvent;
    
    public static TsunamiEventManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LadderInteracted(int currentLevel)
    {
        tsunamiEvent.MiniMapStageUp.SetCurrentLevel(currentLevel);
        tsunamiEvent.MiniMapStageUp.CastleViewUp();
    }
}
