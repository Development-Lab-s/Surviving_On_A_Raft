using System.Collections.Generic;
using _00.Work.CheolYee._01.Codes.Enemys.Portals;
using _00.Work.Resource.Manager;
using _00.Work.Resource.SO;
using UnityEngine;

public class SpawnManager : MonoSingleton<SpawnManager>
{
    [SerializeField] private List<Transform> spawnPoints; // 인스펙터에 프리팹들 넣기

    [SerializeField] private PoolItem _portalItemPrefab; // 풀링할 포탈
    [SerializeField] private PortalDataSo portalData;      // 포탈 설정 데이터
    [SerializeField] private Transform spawnPoint;       // 소환 위치

    private Transform currentSpawn;

    void Start()
    {
        Debug.Log(spawnPoints.Count);
    }
    public void SpawnPortal()
    {
        Portal portal = PoolManager.Instance.Pop(_portalItemPrefab.poolName) as Portal;
        Debug.Log(portal.transform.position);
        Debug.Log(spawnPoint.position);
        portal.transform.position = spawnPoint.position;
        portal.Initialize(portalData, true);
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
