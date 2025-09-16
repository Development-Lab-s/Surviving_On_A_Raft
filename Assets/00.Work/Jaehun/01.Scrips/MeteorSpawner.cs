using System.Collections;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    [SerializeField] private MeteorPool pool;
    [SerializeField] private Transform[] spawnPoints;

    [Header("Spawn Settings")]
    [SerializeField] private float minDelay = 1f;
    [SerializeField] private float maxDelay = 3f;
    [SerializeField] private int minCount = 1;
    [SerializeField] private int maxCount = 5;

    [SerializeField] private float meteorDamage = 20f;
    [SerializeField] private float meteorKnockback = 5f;

    private bool _running;

    private void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        _running = true;
        while (_running)
        {
            int count = Random.Range(minCount, maxCount + 1);

            for (int i = 0; i < count; i++)
            {
                Transform p = spawnPoints[Random.Range(0, spawnPoints.Length)];
                GameObject mObj = pool.Get();
                mObj.transform.position = p.position;

                var meteor = mObj.GetComponent<MeteorObject>();
                meteor.Initialize(p, Vector2.zero, meteorDamage, meteorKnockback, 0);

                yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
            }
        }
    }
}
