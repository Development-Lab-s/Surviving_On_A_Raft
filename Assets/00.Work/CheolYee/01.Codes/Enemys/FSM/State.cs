using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Enemys.FSM
{
    public abstract class State
    {
        //에너미 상태가 가질 기본 요소를 담은 추상 클래스
        
        protected readonly Enemy Enemy; //이 상태를 가질 적
        protected readonly EnemyStateMachine StateMachine; //상태머신

        private readonly int _animBoolHash; //이 상태의 애니메이션 불값 해쉬
        protected bool IsEndTriggerCall; //상태가 끝났는지 알려주는 불값

        //상태 생성을 할 때 정의되어야 할 것들을 생성자에 담기
        protected State(Enemy enemy, EnemyStateMachine stateMachine, string boolName)
        {
            Enemy = enemy;
            StateMachine = stateMachine;
            _animBoolHash = Animator.StringToHash(boolName);
        }

        public virtual void Update() {} //업데이트에서 실행할거를 한번에 담기 위해 만들어둔 메서드
        
        //상태에 들어왔을 떄
        public virtual void Enter()
        {
            Enemy.AnimatorComponent.SetBool(_animBoolHash, true); //애니메이션 활성
            IsEndTriggerCall = false; //시작이니 끝나지 않았다고 표시
        }

        //상태가 끝나서 나갔을 때
        public virtual void Exit()
        {
            Enemy.AnimatorComponent.SetBool(_animBoolHash, false); //끝났으니 애니메이션 끄기
        }
        
        public virtual void AnimationEndTrigger() => IsEndTriggerCall = true; //애니메이션이 끝났을 때 끝났으니 트리거 콜을 트루로 만들기
    }
}