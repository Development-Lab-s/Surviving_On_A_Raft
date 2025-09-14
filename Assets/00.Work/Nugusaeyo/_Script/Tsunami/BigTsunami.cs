using System;
using _00.Work.Resource.Manager;
using _00.Work.Resource.SO;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BigTsunami : MonoBehaviour
{
    private Image bigTsunami;
    private Bubble _bubble;

    private Vector3 _startPosition;
    
    private void Awake()
    {
        bigTsunami = GetComponent<Image>();
        _startPosition = bigTsunami.rectTransform.anchoredPosition;
        gameObject.SetActive(false);
    }

    public void TsunamiUp()
    {
        gameObject.SetActive(true);
        bigTsunami.rectTransform.DOAnchorPos(Vector3.zero, 1.2f).SetEase(Ease.InOutQuint);
        _bubble = PoolManager.Instance.Pop("BubbleParticle") as Bubble;
        _bubble.StartBubble();
    }

    public void TsunamiEnd()
    {
        gameObject.SetActive(false);
        bigTsunami.rectTransform.anchoredPosition = _startPosition;
        if (_bubble != null)
        {
            _bubble.StopAllCoroutines();
            PoolManager.Instance.Push(_bubble);
            _bubble = null;
        }
    }
}
