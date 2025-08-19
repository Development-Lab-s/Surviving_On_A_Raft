using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SelectUIDoTween : MonoBehaviour
{
    public RectTransform[] characterSlots;
    private int _currentIndex = 0;

    public float moveDuration = 0.5f;
    public float scaleSmall = 0.7f;
    public float scaleBig = 1.0f;
    public float moveOffset = 500f;

    public void OnClickNext()
    {
        int nextIndex = (_currentIndex + 1) % characterSlots.Length;
        AnimateTransition(_currentIndex, nextIndex, true);
        _currentIndex = nextIndex;
    }

    public void OnClickPrev()
    {
        int prevIndex = (_currentIndex - 1 + characterSlots.Length) % characterSlots.Length;
        AnimateTransition(_currentIndex, prevIndex, false);
        _currentIndex = prevIndex;
    }

    private void AnimateTransition(int fromIndex, int toIndex, bool toRight)
    {
        RectTransform fromChar = characterSlots[fromIndex];
        RectTransform toChar = characterSlots[toIndex];

        float direction = toRight ? -1 : 1;
        
        fromChar.DOScale(scaleSmall, moveDuration);
        fromChar.DOAnchorPosX(direction * moveOffset, moveDuration)
            .SetEase(Ease.InBack);
        
        toChar.localScale = Vector3.one * scaleSmall;
        toChar.anchoredPosition = new Vector2(-direction * moveOffset, 0);
        
        toChar.DOScale(scaleBig, moveDuration);
        toChar.DOAnchorPosX(0, moveDuration)
            .SetEase(Ease.OutBack);
    }
}