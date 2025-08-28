using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MouseOnSize : MonoBehaviour
{
    [SerializeField] private Vector3 BigSize;
    [SerializeField] private Vector3 OriginSize;
    [SerializeField] private RectTransform ImageSize;
    [SerializeField] private Image ImageObject;
    [SerializeField] private Color ImageColor;
    [SerializeField] private Color OriginColor;


    public void MouseOnMethod()
    {
        ImageSize.DOScale(BigSize, 0.1f);
        ImageObject.DOColor(ImageColor, 0.1f);
    }

    public void MouseOffMethod()
    {
        ImageSize.DOScale(OriginSize, 0.1f);
        ImageObject.DOColor(OriginColor, 0.1f);
    }
}
