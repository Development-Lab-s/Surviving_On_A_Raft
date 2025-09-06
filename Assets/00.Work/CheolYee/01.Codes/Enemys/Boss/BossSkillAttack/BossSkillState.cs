using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Enemys.Boss.BossSkillAttack
{
    public abstract class BossSkillState
    {
        //에너미 상태가 가질 기본 요소를 담은 추상 클래스
        
        protected readonly BossEnemy BossEnemy; //이 상태를 가질 적
        protected readonly BossSkillStateMachine SkillStateMachine; //상태머신

        private readonly int _skillAnimBoolHash; //이 상태의 애니메이션 불값 해쉬
        protected bool IsEndTriggerCall; //상태가 끝났는지 알려주는 불값
        
        protected float Cooldown;     //쿨타임
        private float _lastUseTime;   //마지막 발동 시간

        //상태 생성을 할 때 정의되어야 할 것들을 생성자에 담기
        protected BossSkillState(BossEnemy bossEnemy, BossSkillStateMachine stateMachine, string skillName, int skillcooldown)
        {
            BossEnemy = bossEnemy;
            SkillStateMachine = stateMachine;
            _skillAnimBoolHash = Animator.StringToHash(skillName);
            Cooldown = skillcooldown;
            _lastUseTime = -Cooldown; //시작 시 바로 사용가능
        }

        public virtual void Update() {} //업데이트에서 실행할거를 한번에 담기 위해 만들어둔 메서드
        
        //상태에 들어왔을 떄
        public virtual void Enter()
        {
            BossEnemy.AnimatorComponent.SetBool(_skillAnimBoolHash, true); //애니메이션 활성
            IsEndTriggerCall = false; //시작이니 끝나지 않았다고 표시
            _lastUseTime = Time.time;
            BossEnemy.MovementComponent.canMove = false;
        }

        //상태가 끝나서 나갔을 때
        public virtual void Exit()
        {
            BossEnemy.AnimatorComponent.SetBool(_skillAnimBoolHash, false); //끝났으니 애니메이션 끄기
            BossEnemy.MovementComponent.canMove = true;
        }
        public bool IsAvailable() => Time.time >= _lastUseTime + Cooldown;
        
        public void SkillAnimationEndTrigger() => IsEndTriggerCall = true; //애니메이션이 끝났을 때 끝났으니 트리거 콜을 트루로 만들기
    }
}