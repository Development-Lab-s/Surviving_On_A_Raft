using System.Collections.Generic;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Enemys.Boss.BossSkillAttack
{
    public enum SkillType
    {
        None = 0,
        Skill1 = 1,
        Skill2 = 2,
        Skill3 = 3,
        Skill4 = 4,
        Skill5 = 5
    }
    public class BossSkillStateMachine
    {
        //Enemy의 상태를 관리하고, 상태 전환을 하는 클래스
        public BossSkillState CurrentState { get; private set; } //현재 상태를 나타내는 프로퍼티 (읽기 가능)

        //Enemy의 상태(Enum)과 객체의 실제 상태 타입들을 묶는 딕셔너리
        private readonly Dictionary<SkillType, BossSkillState> _stateDictionary = new Dictionary<SkillType, BossSkillState>();

        private BossEnemy _enemy; // 이 클래스를 참조하는 적 객체를 알기 위한것.

        public void Initialize(SkillType startState, BossEnemy enemy) //FSM 초기화 함수
        {
            _enemy = enemy;
            if (!_stateDictionary.TryGetValue(startState, out var st) || st == null)
            {
                Debug.LogError($"[BossSkillState] 초기 상태 '{startState}'가 등록되지 않았거나 null입니다. 오브젝트: {_enemy.name}");
                return;
            }
            CurrentState = st;
            CurrentState.Enter();
        }

        public void ChangeState(SkillType newState, bool force = false) //상태를 변경하는 함수 
        {
            if (_enemy.isDead && newState != SkillType.None) return;
            if (!_stateDictionary.TryGetValue(newState, out var st) || st == null)
            {
                Debug.LogError($"[BossSkillState] '{newState}' 상태가 등록되지 않았거나 null입니다. 오브젝트: {_enemy.name}");
                return;
            }
            CurrentState?.Exit();
            CurrentState = st;
            CurrentState.Enter();
        }

        public void AddState(SkillType stateEnum, BossSkillState state) //FSM에 상태를 등록하는 함수이다.
        {
            _stateDictionary.Add(stateEnum, state); // Enum(상태)와 EnemyState(상속받은놈 포함)
        }
    }
}