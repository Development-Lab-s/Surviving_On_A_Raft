using System.Collections.Generic;
using _00.Work.Resource.SO;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Enemys.Portals
{
    [CreateAssetMenu(fileName = "NewPortalData", menuName = "SO/PortalData")]
    public class PortalDataSo : ScriptableObject
    {
        [Header("Portal Data")]
        public List<PoolItem> enemies; //포탈에서 나올 적들을 정의함

        public int enemyCount = 10; //에너미 최대 개수

        public float launchForce, minTime, maxTime; // 튀나가는 속도
        
        public Color portalColor; //포탈 색

        public bool isBossStage;
        public PoolItem currentBoss;
        public int GetRandomListIndex() => Random.Range(0, enemies.Count); //리스트 랜덤 인덱스 가져오기
        public float GetRandomSpawnTime() => Random.Range(minTime, maxTime); //랜덤 시간초 가져오기
        
        #if UNITY_EDITOR
        private void OnValidate()
        {
            if (enemies != null)
            {
                foreach (PoolItem item in enemies)
                {
                    if (!item.prefab.TryGetComponent(out Enemy _))
                    {
                        Debug.LogWarning($"{item.poolName} : 지금 넣은 풀아이템은 에너미가 아닙니다.");
                    }
                }
            }
        }
        #endif
    }
}