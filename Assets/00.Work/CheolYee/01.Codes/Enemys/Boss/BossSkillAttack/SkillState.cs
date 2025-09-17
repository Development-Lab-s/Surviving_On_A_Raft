using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Enemys.Boss.BossSkillAttack
{
    public class SkillState
    {
        protected readonly Enemy Enemy; //보스 에너미 (보스는 에너미를 상속받고있음)
        protected float CoolDown; //쿨타임
        protected bool IsEndTriggerCall; //애니메이션이 끝났는가?
        protected float LastAttackTime; //마지막 스킬 발동 시간

        private readonly int _animBoolHash; //애니메이션 전환용 불헤쉬값
        private bool _isFinished; //스킬이 끝났는지 확인

        //생성자임, 상속받은 후 다른 값을 더 받을 수 있음 (스킬이 가질 수 있는 공통된 기본값 미리 받기)
        //스킬은 발동이 끝나는 즉시 Attack에서 빠져나가므로 상태머신을 받아 전환할 필요 없음
        protected SkillState(Enemy enemy, string animBoolName, float coolDown)
        {
            Enemy = enemy;
            CoolDown = coolDown;
            _animBoolHash = Animator.StringToHash(animBoolName);
            LastAttackTime = -coolDown;
        }

        //스킬 애니메이션, 움직임 정지 등 기본처리
        public virtual void Enter()
        {
            Enemy.AnimatorComponent.SetBool(_animBoolHash, true);
            IsEndTriggerCall = false;
            _isFinished = false;

            //스킬 시전 중에는 기본 이동 중지.
            Enemy.MovementComponent.StopImmediately(true);
        }

        public virtual void Update() { }

        //스킬 애니메이션 bool 끔
        public virtual void Exit()
        {
            Enemy.AnimatorComponent.SetBool(_animBoolHash, false);
        }

        //애니메이션 이벤트(AttackCast) 시 호출되는 지점
        public virtual void OnAnimationCast() { }

        public virtual void ComboFlip() { }  // 뒤집는 애니메이션

        //애니메이션 종료 이벤트가 들어오면 호출
        public virtual void AnimationEndTrigger() => IsEndTriggerCall = true;

        //외부(프리팹 등)에서 스킬을 완료할 때 호출
        public void MarkFinished() => _isFinished = true;

        //스킬 완료 여부 체크 (애니메이션 End 또는 외부 표식)
        public bool IsCompleted => _isFinished || IsEndTriggerCall;

        //스킬 사용 가능 여부(쿨타임, 사거리 등)
        public virtual bool CanUse() => true;
    }
}