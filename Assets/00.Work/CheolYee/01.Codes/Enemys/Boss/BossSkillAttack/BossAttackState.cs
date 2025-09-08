using _00.Work.CheolYee._01.Codes.Enemys.FSM;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Enemys.Boss.BossSkillAttack
{
    public class BossAttackState : State
    {
        private readonly SkillStateMachine _skillStateMachine;
        
        public BossAttackState(Enemy enemy, EnemyStateMachine stateMachine, string boolName) : base(enemy, stateMachine, boolName)
        {
            _skillStateMachine = new SkillStateMachine(enemy);
        }
        
        public void AddSkill(SkillType type, SkillState state) => _skillStateMachine.AddState(type, state);

        public override void Enter()
        {
            base.Enter(); // Animator의 Attack bool 켜짐
            Enemy.MovementComponent.StopImmediately();

            // 스킬 선택 (CanUse()를 만족하는 첫 스킬 선택)
            var selected = SelectSkill();
            if (selected.HasValue)
            {
                Debug.LogError("상태변환" + selected.Value);
                _skillStateMachine.ChangeState(selected.Value);
            }
            /*else
            {
                Debug.LogError("상태 없음");
                // 선택된 스킬이 없으면 기본 공격 스킬을 실행하도록 설계 가능 (예: Skill1)
                if (_skillStateMachine.TryGetState(SkillType.Skill1, out var _))
                    _skillStateMachine.ChangeState(SkillType.Skill1);
                else
                    Debug.LogWarning("[BossAttackState] No skill to execute.");
            }*/
        }

        public override void Update()
        {
            base.Update();

            //서브 FSM 업데이트
            _skillStateMachine.Update();

            //현재 스킬이 끝났으면 Attack 상태 종료
            if (_skillStateMachine.CurrentState != null && _skillStateMachine.CurrentState.IsCompleted)
            {
                //스킬이 끝난 시점에 마지막 공격 시간 갱신 후 Chase 로 다시 진입 못하게 함
                Enemy.lastAttackTime = Time.time;

                //Attack 상태의 Exit을 타고 Idle로 복귀
                StateMachine.ChangeState(EnemyBehaviourType.Idle);
            }
        }

        // 애니메이터의 AttackCast 이벤트가 들어왔을 때(Enemy.Attack()가 호출되면)
        public void OnAnimationCast()
        {
            _skillStateMachine.OnAnimationCast();
        }

        // 애니메이션 End 트리거가 들어오면 서브 FSM으로 전달
        public override void AnimationEndTrigger()
        {
            _skillStateMachine.OnAnimationEnd();
        }

        //스킬 선택 로직
        private SkillType? SelectSkill()
        {
            foreach (var kv in _skillStateMachine.AllStates()) //모든 스킬을 검사해 사용할 수 있는지 알아낸다.
            {
                var st = kv.Value;
                // 기본 CanUse + 거리 조건(스킬마다 오버라이드 가능)
                Debug.Log(st.CanUse());
                if (st.CanUse())
                {
                    // 스킬 스테이트에서 거리 체크를 내부에서 하도록 재설계해도 된다.
                    return kv.Key;
                }
            }

            return null;
        }
    }
}