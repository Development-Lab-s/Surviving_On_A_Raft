using System.Collections.Generic;
using UnityEngine;

public class MeteorPool : MonoBehaviour
{
    [Header("Pooling Settings")]
    [SerializeField] private GameObject meteorPrefab;
    [SerializeField] private int poolSize = 20;

    private readonly Queue<GameObject> pool = new Queue<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < poolSize; i++)
        {
            var obj = Instantiate(meteorPrefab, transform);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public GameObject Get()
    {
        if (pool.Count > 0)
        {
            var obj = pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }

        // 풀 다 쓰면 그냥 새로 만들 수도 있고, null을 리턴할 수도 있음
        var extra = Instantiate(meteorPrefab, transform);
        return extra;
    }

    public void Return(GameObject meteor)
    {
        meteor.SetActive(false);
        pool.Enqueue(meteor);
    }
}
