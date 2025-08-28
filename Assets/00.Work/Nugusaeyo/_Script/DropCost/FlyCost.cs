using System;
using _00.Work.Resource.Manager;
using UnityEngine;
using DG.Tweening;

public class FlyCost : MonoBehaviour
{
    [SerializeField] private float time;
    private Vector2 _target = new Vector2(-6, 2.25f);

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(gameObject.transform.DOMove(_target, time).SetEase(Ease.InCubic));
            sequence.AppendCallback(() =>
            {
                PoolManager.Instance.Pop(gameObject.name);
            });
        }
    }
}
