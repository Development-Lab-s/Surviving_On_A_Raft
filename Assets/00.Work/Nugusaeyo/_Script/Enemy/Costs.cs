using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Costs : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    [SerializeField] private int costType;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        gameObject.name = "Cost " + costType.ToString();
        _rigidbody2D.AddForce(new Vector2(Random.Range(-100, 100), Random.Range(125, 200)));
    }
}
