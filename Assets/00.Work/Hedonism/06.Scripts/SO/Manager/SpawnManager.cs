using System.Collections;
using System.Collections.Generic;
using _00.Work.CheolYee._01.Codes.Core.Cameras;
using _00.Work.CheolYee._01.Codes.Enemys;
using _00.Work.CheolYee._01.Codes.Enemys.Portals;
using _00.Work.CheolYee._01.Codes.Managers;
using _00.Work.Hedonism._06.Scripts.ChangeMap;
using _00.Work.Nugusaeyo._Script.Cost;
using _00.Work.Nugusaeyo._Script.Tsunami;
using _00.Work.Resource.Manager;
using _00.Work.Resource.SO;
using UnityEngine;

namespace _00.Work.Hedonism._06.Scripts.SO.Manager
{
    public class SpawnManager : MonoSingleton<SpawnManager>
    {
        [SerializeField] private List<Transform> spawnPoints; // 인스펙터에 프리팹들 넣기

        [SerializeField] private PoolItem portalItemPrefab; // 풀링할 포탈
        [SerializeField] private List<PortalDataSo> portalData;    // 포탈 설정 데이터
        [SerializeField] private int maxSpawnCount;    //최대 스폰 카운트
        
        private bool _isBossSpawn;
        
        private Transform _playerTrm;


        public List<Enemy> Enemys { get; set; } = new();

        void Start()
        {
            _playerTrm = GameManager.Instance.playerTransform;
            OnFirstStart(Random.Range(0, spawnPoints.Count));
        }

        public bool CanSpawn()
        {
            return Enemys?.Count < maxSpawnCount;
        }

        private void IsBossSpawn(Vector3 position)
        {
            if (_isBossSpawn)
            {
                Enemy boss = PoolManager.Instance.Pop(portalData[GameManager.Instance.currentLevel - 1].currentBoss.poolName) as Enemy;
                if (boss != null) boss.transform.position = position;
            }
        }

        public void RemoveEnemy(Enemy enemy)
        {
            Enemys.Remove(enemy);
        }

        //  여러 개의 포탈 관리용 리스트
        private readonly List<Portal> _currentPortals = new List<Portal>();

        public void SpawnPortalInMap(MapArea map)
        {

            if (GameManager.Instance.currentLevel == 12)
            {
                SoundManager.Instance.PlayBgm("BOSS");
            }
            // 기존 포탈 전부 삭제
            DespawnCurrentPortals();
            ClearAllEnemies();
            maxSpawnCount = portalData[GameManager.Instance.currentLevel - 1].enemyCount;
            _isBossSpawn = portalData[GameManager.Instance.currentLevel - 1].isBossStage;
            
            if (map.portalSpawnPoints.Count == 0)
            {
                return;
            }

            // 랜덤하게 2개의 다른 위치 선택
            List<int> indices = new List<int>();
            while (indices.Count < map.portalSpawnCount)
            {
                int index = Random.Range(0, map.portalSpawnPoints.Count);
                if (!indices.Contains(index))
                    indices.Add(index);
            }

            // 포탈 2개 생성
           IsBossSpawn(map.portalSpawnPoints[0].position); //만약에 보스 생성할 수 있다면 보스생성
            foreach (int index in indices)
            {
                
                Vector3 spawnPos = map.portalSpawnPoints[index].position;
                bool left = map.portalSpawnPoints[index].GetComponent<PortalSpawn>().isLeft;

                Portal portal = PoolManager.Instance.Pop(portalItemPrefab.poolName) as Portal;
                if (portal != null)
                {
                    portal.transform.position = spawnPos;
                    portal.Initialize(portalData[GameManager.Instance.currentLevel -1], left);

                    _currentPortals.Add(portal); // 리스트에 저장
                }
            }
        }

        /// <summary>
        /// 현재 활성화된 모든 포탈 제거
        /// </summary>
        public void DespawnCurrentPortals()
        {
            if (_currentPortals.Count > 0)
            {
                foreach (var portal in _currentPortals)
                {
                    PoolManager.Instance?.Push(portal);
                }
                Debug.Log("기존 포탈 제거 완료");
                _currentPortals.Clear();
            }
        }

        public void ClearAllEnemies()
        {
            Enemys.Clear();
            
            Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None); // 씬에 있는 모든 Enemy 컴포넌트 검색
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
            StartCoroutine(MovePlayerTo(index));
        }

        private void OnFirstStart(int index)
        {
            ShowStageSelectUI.Instance.lastChosenIndex = index;
            FadeManager.Instance.FadeIn();
            _playerTrm.position = spawnPoints[index].transform.position;
        }

        private IEnumerator MovePlayerTo(int index)
        {
            FadeManager.Instance.FadeIn();
            yield return new WaitForSeconds(0.5f);
            _playerTrm.position = spawnPoints[index].transform.position;
            CamDampingSetting.Instance.WarpPlayer(_playerTrm.position);
            TsunamiEventManager.Instance.LadderInteracted(GameManager.Instance.currentLevel);
        }
    }
}
