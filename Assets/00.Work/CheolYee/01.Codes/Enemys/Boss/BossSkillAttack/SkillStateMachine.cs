using System;
using System.Collections.Generic;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Enemys.Boss.BossSkillAttack
{
    public enum SkillType
    {
        Skill1 = 0,
        Skill2 = 1,
        Skill3 = 2,
        Skill4 = 3,
        Skill5 = 4,
        Skill6 = 5
    }

    public class SkillStateMachine
    {
        private readonly Dictionary<SkillType, SkillState> _states = new();
        public SkillState CurrentState { get; private set; }
        private readonly Enemy _enemy;

        public SkillStateMachine(Enemy enemy)
        {
            _enemy = enemy;
        }

        //상태 추가
        public void AddState(SkillType type, SkillState state)
        {
            if (_states.ContainsKey(type)) throw new ArgumentException($"Skill {type} 이 추가되지 않았습니다.");
            _states.Add(type, state);
        }

        //상태 가져오기
        public bool TryGetState(SkillType type, out SkillState state) => _states.TryGetValue(type, out state);

        //상태 변경
        public void ChangeState(SkillType type)
        {
            if (!_states.TryGetValue(type, out var next) || next == null)
            {
                Debug.LogWarning($"[SkillStateMachine] Skill {type} 이 설정되지 않았습니다.");
                return;
            }

            CurrentState?.Exit();
            CurrentState = next;
            CurrentState.Enter();
        }

        public void Update()
        {
            CurrentState?.Update();
        }

        //애니메이터의 AttackCast 이벤트가 들어오면 호출
        public void OnAnimationCast() => CurrentState?.OnAnimationCast();

        //애니메이터의 AnimationEnd 이벤트가 들어오면 호출
        public void OnAnimationEnd() => CurrentState?.AnimationEndTrigger();

        //상태들을 순회할 수 있게 (선택 로직에서 사용)
        public IEnumerable<KeyValuePair<SkillType, SkillState>> AllStates() => _states;
    }
}