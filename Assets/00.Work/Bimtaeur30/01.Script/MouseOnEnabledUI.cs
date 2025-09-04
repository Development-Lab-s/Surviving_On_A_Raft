using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MouseOnEnabledUI : MonoBehaviour
{
    [SerializeField] private GameObject ShowUI;


    public void MouseOnMethod()
    {
        ShowUI.gameObject.SetActive(true);
    }

    public void MouseOffMethod()
    {
        ShowUI.gameObject.SetActive(false);
    }
}
