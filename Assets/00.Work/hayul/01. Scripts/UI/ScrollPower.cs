using UnityEngine;
using UnityEngine.UI;

public class ScrollPower : MonoBehaviour
{
    public ScrollRect scrollRect;

    void Start()
    {
        
        scrollRect.scrollSensitivity = 13f;
    }
}