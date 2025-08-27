using System;
using UnityEngine;

public class TsunamiTimer : MonoBehaviour
{
    public static TsunamiTimer instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
}
