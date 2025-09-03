using UnityEngine;
using UnityEngine.UI;

public class SmoothScroll : MonoBehaviour
{
    public ScrollRect scrollRect;

    void Start()
    {
        scrollRect.inertia = true;
        
        scrollRect.decelerationRate = 0.5f; 
    }
}