using System.Collections.Generic;
using _00.Work.Nugusaeyo._Script.Enemy;
using _00.Work.Resource.Manager;
using UnityEngine;
using DG.Tweening;

public class FlyCost : MonoBehaviour
{
    [SerializeField] private float time;
    private Vector2 _target = new Vector2(-6, 2.25f);
    private Collider2D _collider2D;
    
    
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private int costType;
    public List<Sprite> spriteList = new List<Sprite>();

    private Costs _cost;
    private void Awake()
    {
        _cost = GetComponent<Costs>();
        _collider2D = GetComponent<Collider2D>();
        
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        int rand = Random.Range(0, 101);
        if (rand < 27)
        {
            costType = 0;
        }
        else if (rand < 54)
        {
            costType = 1;
        }
        else if (rand < 81)
        {
            costType = 2;
        }
        else if (rand < 92)
        {
            costType = 3;
        }
        else
        {
            costType = 4;
        }
        _spriteRenderer.sprite = spriteList[costType];
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("닿았따");
            _collider2D.enabled = false;
            Sequence sequence = DOTween.Sequence();
            sequence.Append(gameObject.transform.DOMove(_target, time).SetEase(Ease.InQuint));
            sequence.AppendCallback(() =>
            {
                CostManager.instance.PlusCost(costType, 1);
                PoolManager.Instance.Push(_cost);
                _collider2D.enabled = true;
                Debug.Log("비행 끝");
            });
        }
    }
}
