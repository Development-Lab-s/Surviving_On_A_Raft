using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private SpawnSO spawnSO;
    private List<SpawnSO.SpawnPointData> currentSpawnList;

    private SpawnSO.SpawnPointData currentSpawn;
    
    public static SpawnManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else return;

        // SO 데이터를 복사해서 사용 (원본 훼손 X)
        currentSpawnList = new List<SpawnSO.SpawnPointData>(spawnSO.spawnPoints);
    }

    public void StartCycle()
    {
        if (currentSpawnList.Count == 0)
        {
            Debug.Log("모든 스폰포인트를 다 사용했습니다!");
            return;
        }

        // 랜덤 선택
        int randomIndex = Random.Range(0, currentSpawnList.Count);
        currentSpawn = currentSpawnList[randomIndex];

        // 플레이어 이동
        MovePlayerTo(currentSpawn.position);

        // 사용한 위치 삭제
        currentSpawnList.RemoveAt(randomIndex);
    }

    private void MovePlayerTo(Vector2 pos)
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            player.transform.position = pos;
        }
    }
}
