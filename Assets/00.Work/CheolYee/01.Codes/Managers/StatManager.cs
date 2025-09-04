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
    }
    public class StatManager : MonoSingleton<StatManager>
    {
        public event Action<StatType, float> OnEnemyBuff;
        public event Action<StatType, float> OnPlayerBuff;
        public event Action<StatType> OnResetEnemyBuff;
        public event Action<StatType> OnResetPlayerBuff;

        private readonly Dictionary<StatType, float> _enemyBuffMultipliers = new();
        
        private void Start()
        {
            EnemyBuffInTime(StatType.MoveSpeed, 3, 5);
        }

        private void Update()
        {
            if (Keyboard.current.hKey.wasPressedThisFrame) PlayerBuffInTime(StatType.MoveSpeed, 3, 10);
        }

        ///<summary>
        ///전체 적에게 버프 적용 (타입, 배율, 지속시간)
        ///</summary>
        private void EnemyBuffInTime(StatType stats, float multi, float time)
        {
            StartCoroutine(EnemyBuffRoutine(stats, multi, time));
        }
        
        ///<summary>
        ///전체 플레이어에게 버프 적용 (타입, 배율, 지속시간)
        ///</summary>
        public void PlayerBuffInTime(StatType stats, float multi, float time)
        {
            StartCoroutine(PlayerBuffRoutine(stats, multi, time));
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
        
        public void BuffPlayer(StatType stats, float multi)
        {
            OnPlayerBuff?.Invoke(stats, multi);
        }
        private void ResetEnemies(StatType stat)
        {
            if (_enemyBuffMultipliers.ContainsKey(stat))
                _enemyBuffMultipliers.Remove(stat);
    
            OnResetEnemyBuff?.Invoke(stat);
        }

        public float GetEnemyBuff(StatType stat)
        {
            return _enemyBuffMultipliers.GetValueOrDefault(stat, 1f);
        }
        public void ResetPlayer(StatType type)
        {
            OnResetPlayerBuff?.Invoke(type);
        }
    }
}