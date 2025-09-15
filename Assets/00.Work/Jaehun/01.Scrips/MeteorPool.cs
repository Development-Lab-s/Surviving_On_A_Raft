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

        // Ǯ �� ���� �׳� ���� ���� ���� �ְ�, null�� ������ ���� ����
        var extra = Instantiate(meteorPrefab, transform);
        return extra;
    }

    public void Return(GameObject meteor)
    {
        meteor.SetActive(false);
        pool.Enqueue(meteor);
    }
}
