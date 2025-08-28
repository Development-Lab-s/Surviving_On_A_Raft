using System;
using _00.Work.Nugusaeyo._Script.Enemy;
using _00.Work.Resource.Manager;
using UnityEngine;
using DG.Tweening;

public class FlyCost : MonoBehaviour
{
    [SerializeField] private float time;
    private Vector2 _target = new Vector2(-6, 2.25f);
    private Collider2D _collider2D;

    private Costs _cost;
    private void Awake()
    {
        _cost = GetComponent<Costs>();
        _collider2D = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("닿았따");
            _collider2D.enabled = false;
            Sequence sequence = DOTween.Sequence();
            sequence.Append(gameObject.transform.DOMove(_target, time).SetEase(Ease.InCubic));
            sequence.AppendCallback(() =>
            {
                PoolManager.Instance.Push(_cost);
                _collider2D.enabled = true;
                Debug.Log("비행 끝");
            });
        }
    }
}
