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

    private Portal currentPortal; // 현재 활성화된 포탈 참조

    /// <summary>
    /// 특정 맵에 포탈 소환
    /// </summary>
    public void SpawnPortalInMap(MapArea map)
    {
        // 기존 포탈 삭제
        DespawnCurrentPortal();

        if (map.portalSpawnPoints.Count == 0)
        {
            Debug.LogWarning($"{map.mapName}에는 포탈 스폰포인트가 없습니다!");
            return;
        }

        // 랜덤 위치 선택
        int index = Random.Range(0, map.portalSpawnPoints.Count);
        Vector3 spawnPos = map.portalSpawnPoints[index].position;

        // 풀에서 포탈 꺼내기
        Portal portal = PoolManager.Instance.Pop(_portalItemPrefab.poolName) as Portal;
        portal.transform.position = spawnPos;
        portal.Initialize(portalData, false);

        currentPortal = portal; // 현재 포탈 저장

        Debug.Log($"{map.mapName} 에 포탈 생성! 위치: {spawnPos}");
    }

    /// <summary>
    /// 현재 포탈 제거
    /// </summary>
    public void DespawnCurrentPortal()
    {
        if (currentPortal != null)
        {
            PoolManager.Instance.Push(currentPortal);
            Debug.Log("기존 포탈 제거 완료");
            currentPortal = null;
        }
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

    // public void SpawnPortal()
    // {
    //     //나는 할거야 for문을 포탈이 랜덤으로 몇개 열리게에~ 그리고오 맵마다 배치할거야아
    //     // ~ 들어간 맵에 있는 풀만 활성화 시킬 수 있어야데👍
    //     //for()
    //     // {

    //     // }
    //     // Portal portal = PoolManager.Instance.Pop(_portalItemPrefab.poolName) as Portal;
    //     // portal.transform.position = spawnPoint.position;
    //     // portal.Initialize(portalData, true);
    // }


    // public void SpawnPortalInMap(MapArea map)
    // {
    //     if (map.portalSpawnPoints.Count == 0)
    //     {
    //         Debug.LogWarning($"{map.mapName} 에 스폰포인트 없음");
    //         return;
    //     }

    //     // 랜덤 위치 선택
    //     int index = Random.Range(0, map.portalSpawnPoints.Count);
    //     Vector3 spawnPos = map.portalSpawnPoints[index].position;

    //     // 풀에서 포탈 꺼내기
    //     Portal portal = PoolManager.Instance.Pop(_portalItemPrefab.poolName) as Portal;
    //     portal.transform.position = spawnPos;
    //     portal.Initialize(portalData, false);
    // }