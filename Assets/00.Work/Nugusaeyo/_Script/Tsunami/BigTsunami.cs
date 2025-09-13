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

    private void Awake()
    {
        bigTsunami = GetComponent<Image>();
        gameObject.SetActive(false);
    }

    public void TsunamiUp()
    {
        gameObject.SetActive(true);
        bigTsunami.rectTransform.DOAnchorPos(new Vector3(0, bigTsunami.rectTransform.sizeDelta.y, 0), 1.2f).SetEase(Ease.InOutQuint);
        _bubble = PoolManager.Instance.Pop("BubbleParticle") as Bubble;
        _bubble.StartBubble();
    }

    public void TsunamiEnd()
    {
        gameObject.SetActive(false);
        bigTsunami.rectTransform.anchoredPosition = new Vector2(0, 0);
        if (_bubble != null)
        {
            _bubble.StopAllCoroutines();
            PoolManager.Instance.Push(_bubble);
            _bubble = null;
        }
    }
}
