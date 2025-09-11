using System.Collections.Generic;
using _00.Work.CheolYee._01.Codes.Enemys;
using _00.Work.CheolYee._01.Codes.Enemys.Portals;
using _00.Work.CheolYee._01.Codes.Managers;
using _00.Work.Resource.Manager;
using _00.Work.Resource.SO;
using UnityEngine;

public class SpawnManager : MonoSingleton<SpawnManager>
{
    [SerializeField] private List<Transform> spawnPoints; // 인스펙터에 프리팹들 넣기

    [SerializeField] private PoolItem _portalItemPrefab; // 풀링할 포탈
    [SerializeField] private PortalDataSo portalData;    // 포탈 설정 데이터
    private Transform _playerTrm;

    void Start()
    {
        _playerTrm = GameManager.Instance.playerTransform;
    }

    //  여러 개의 포탈 관리용 리스트
    private List<Portal> currentPortals = new List<Portal>();

    public void SpawnPortalInMap(MapArea map)
    {
        // 기존 포탈 전부 삭제
        DespawnCurrentPortals();
        ClearAllEnemies();

        if (map.portalSpawnPoints.Count == 0)
        {
            return;
        }

        // 랜덤하게 2개의 다른 위치 선택
        List<int> indices = new List<int>();
        while (indices.Count < 2)
        {
            int index = Random.Range(0, map.portalSpawnPoints.Count);
            if (!indices.Contains(index))
                indices.Add(index);
        }

        // 포탈 2개 생성
        foreach (int index in indices)
        {
            Vector3 spawnPos = map.portalSpawnPoints[index].position;

            Portal portal = PoolManager.Instance.Pop(_portalItemPrefab.poolName) as Portal;
            portal.transform.position = spawnPos;
            portal.Initialize(portalData, false);

            currentPortals.Add(portal); // 리스트에 저장
        }
    }

    /// <summary>
    /// 현재 활성화된 모든 포탈 제거
    /// </summary>
    public void DespawnCurrentPortals()
    {
        if (currentPortals.Count > 0)
        {
            foreach (var portal in currentPortals)
            {
                PoolManager.Instance.Push(portal);
            }
            Debug.Log("기존 포탈 제거 완료");
            currentPortals.Clear();
        }
    }

    public void ClearAllEnemies()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>(); // 씬에 있는 모든 Enemy 컴포넌트 검색
        foreach (Enemy enemy in enemies)
        {
            if (enemy.TryGetComponent(out IPoolable pool))
            {
                PoolManager.Instance.Push(pool);
            }
        }
    }


    public void StartCycle(int index)
    {
        // 플레이어 이동
        MovePlayerTo(index);
    }

    private void MovePlayerTo(int index)
    {
        _playerTrm.position = spawnPoints[index].transform.position;
    }
}
