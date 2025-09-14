using System.Collections.Generic;
using _00.Work.Nugusaeyo._Script.Cost;
using _00.Work.Nugusaeyo._Script.Enemy;
using _00.Work.Resource.Manager;
using UnityEngine;
using Random = UnityEngine.Random;

public class FlyCost : MonoBehaviour
{
    [SerializeField] private float time;
    private Vector2 _target;
    private Collider2D _collider2D;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private int costType; 
    public List<Sprite> spriteList = new List<Sprite>();
    private Rigidbody2D _rigidbody2D;
    
    private bool _isFollow;
    private Costs _cost;
    private float _itemSpeed = 5f;
    
    private void Awake() {
        _cost = GetComponent<Costs>();
        _collider2D = GetComponent<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        
        _isFollow = false; 
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
        _isFollow = false;
    }

    private void OnCollisionEnter2D()
    {
        _collider2D.enabled = false;
        _isFollow = true;
        _rigidbody2D.gravityScale = 0;
    }

    private void Update()
    {
        if (_isFollow)
        {
            if (Camera.main != null)
            {
                Vector3 target = Camera.main.ScreenToWorldPoint(CostBoarder.Instance.costGoalPos.transform.position);
                transform.position = Vector3.Lerp(transform.position, target, _itemSpeed * Time.deltaTime);
                if (Vector3.Distance(transform.position, target) < 2f)
                {
                    if (CostManager.Instance != null)
                    {
                        CostManager.Instance.PlusCost(costType, 1);
                        if (PoolManager.Instance != null) PoolManager.Instance.Push(_cost);
                    }

                    _collider2D.enabled = true;
                    _isFollow = false;
                    _rigidbody2D.gravityScale = 1f;
                }
            }
        }
    }
}