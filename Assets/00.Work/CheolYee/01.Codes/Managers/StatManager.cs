using System;
using System.Collections;
using System.Collections.Generic;
using _00.Work.Resource.Manager;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _00.Work.CheolYee._01.Codes.Managers
{
    public enum StatType
    {
        Health,
        Damage,
        MoveSpeed,
        CritChance,
        AttackSpeed
    }
    public class StatManager : MonoSingleton<StatManager>
    {
        [SerializeField] private float enemyGrowthPerSecondAmount = 0.01f; //1퍼
        [SerializeField] private float enemyGrowthPerSecond = 1; //1초
        public event Action<StatType, float> OnEnemyBuff;
        public event Action<StatType, float> OnPlayerBuff;
        public event Action<StatType> OnResetEnemyBuff;
        public event Action<StatType> OnResetPlayerBuff;

        private readonly Dictionary<StatType, float> _enemyBuffMultipliers = new();
        
        private readonly Dictionary<StatType, float> _enemyGrowthMultipliers = new();

        private Coroutine _enemyGrowthRoutine;
        private WaitForSeconds _enemyGrowthTime;
        protected override void Awake()
        {
            base.Awake();
            _enemyGrowthTime = new WaitForSeconds(enemyGrowthPerSecond);
            ResetGrowthMultipliers();
        }
        private void Start()
        {
            StartGrowthRoutine();
        }

        ///<summary>
        ///전체 적 성장 배율 초기화 (다음 층으로 갈 때 사용 하면 될듯)
        ///</summary>
        public void ResetGrowthMultipliers()
        {
            foreach (StatType stat in Enum.GetValues(typeof(StatType)))
            {
                _enemyGrowthMultipliers[stat] = 0;
            }
        }

        ///<summary>
        ///전체 적 성장 루틴 시작
        ///</summary>
        public void StartGrowthRoutine()
        {
            _enemyGrowthRoutine = StartCoroutine(GrowthRoutine());
        }
        
        ///<summary>
        ///전체 적 성장 루틴 멈추기 (초기화가 아님 멈추는거임)
        ///</summary>
        public void StopGrowthRoutine()
        {
            StopCoroutine(_enemyGrowthRoutine);
        }
        ///<summary>
        ///일정 시간동안 전체 적 버프 (스텟, 배율, 시간)
        ///</summary>
        public void EnemyBuffInTime(StatType stats, float multi, float time)
        {
            StartCoroutine(EnemyBuffRoutine(stats, multi, time));
        }
        ///<summary>
        ///일정 시간동안 플레이어 버프 (스텟, 배율, 시간)
        ///</summary>
        public void PlayerBuffInTime(StatType stats, float multi, float time)
        {
            StartCoroutine(PlayerBuffRoutine(stats, multi, time));
        }
        ///<summary>
        ///적의 현재 버프 상황 얻어오기 (스텟 타입)
        ///</summary>
        public float GetEnemyBuff(StatType stat)
        {
            float buffMulti = _enemyBuffMultipliers.GetValueOrDefault(stat, 1f);
            float growthMulti = _enemyGrowthMultipliers.GetValueOrDefault(stat, 0f);
            return buffMulti + growthMulti;
        }
        ///<summary>
        ///플레이어 버프 초기화 (스텟 타입)
        ///</summary>
        public void ResetPlayer(StatType type)
        {
            OnResetPlayerBuff?.Invoke(type);
        }
        ///<summary>
        ///적 버프 초기화 (스텟 타입)
        ///</summary>
        public void ResetEnemies(StatType stat)
        {
            if (_enemyBuffMultipliers.ContainsKey(stat))
                _enemyBuffMultipliers.Remove(stat);
    
            OnResetEnemyBuff?.Invoke(stat);
        }
        
        private IEnumerator GrowthRoutine()
        {
            yield return _enemyGrowthTime;
            _enemyGrowthMultipliers[StatType.Health] += enemyGrowthPerSecondAmount;
            _enemyGrowthMultipliers[StatType.Damage] += enemyGrowthPerSecondAmount;
        }
        private IEnumerator EnemyBuffRoutine(StatType stats, float multi, float time)
        {
            BuffEnemies(stats, multi);
            yield return new WaitForSeconds(time);
            ResetEnemies(stats);
        }
        private IEnumerator PlayerBuffRoutine(StatType stats, float multi, float time)
        {
            BuffPlayer(stats, multi);
            yield return new WaitForSeconds(time);
            ResetPlayer(stats);
        }
        private void BuffEnemies(StatType stats, float multi)
        {
            _enemyBuffMultipliers[stats] = multi;            
            OnEnemyBuff?.Invoke(stats, multi);
        }
        private void BuffPlayer(StatType stats, float multi)
        {
            OnPlayerBuff?.Invoke(stats, multi);
        }
    }
}