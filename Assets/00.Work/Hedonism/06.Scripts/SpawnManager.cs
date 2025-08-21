using System.Collections.Generic;
using _00.Work.Resource.Manager;
using UnityEngine;

public class SpawnManager : MonoSingleton<SpawnManager>
{
    [SerializeField] private List<Transform> spawnPoints; // 인스펙터에 프리팹들 넣기
    private Transform currentSpawn;

    void Start()
    {
        Debug.Log(spawnPoints.Count);
    }
    public void StartCycle()
    {
        Debug.Log(spawnPoints.Count);
        if (spawnPoints.Count == 0)
        {
            Debug.Log("모든 스폰포인트를 다 사용했습니다!");
            return;
        }

        // 랜덤 인덱스 선택
        int randomIndex = Random.Range(0, spawnPoints.Count);
        currentSpawn = spawnPoints[randomIndex];

        // 플레이어 이동
        MovePlayerTo(currentSpawn.position);
        Debug.Log($"인덱스: {randomIndex}, 포지션: {currentSpawn.position}");

        // 사용한 스폰포인트 제거
        spawnPoints.RemoveAt(randomIndex);
    }

    private void MovePlayerTo(Vector3 pos)
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            player.transform.position = pos;
        }
    }
}
