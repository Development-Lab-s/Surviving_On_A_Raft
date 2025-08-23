using System.Collections.Generic;

namespace _00.Work.CheolYee._01.Codes.Enemy.FSM
{
    public class EnemyStateMachine
    {
        //Enemy의 상태를 관리하고, 상태 전환을 하는 클래스
        public EnemyState CurrentState { get; private set; } //현재 상태를 나타내는 프로퍼티 (읽기 가능)
        
        //Enemy의 상태(Enum)과 객체의 실제 상태 타입들을 묶는 딕셔너리
        private readonly Dictionary<EnemyBehaviourType, EnemyState> _stateDictionary = new Dictionary<EnemyBehaviourType, EnemyState>();

        private Enemy _enemy; // 이 클래스를 참조하는 적 객체를 알기 위한것.

        public void Initialize(EnemyBehaviourType startState, Enemy enemy) //FSM 초기화 함수
        {
            _enemy = enemy; //enemy 인스턴스 저장
            CurrentState = _stateDictionary[startState]; //시작 상태 설정 (키인 enum을 찾아 그 상태의 value 즉 EnemyState로 설정한다)
            CurrentState.Enter(); //현재 상태를 저장하고 시작
        }

        public void ChangeState(EnemyBehaviourType newState, bool force = false) //상태를 변경하는 함수 
        {
            if (_enemy.CanStateChangeable == false && force == false) return;
            //상태 변환이 불가능한 상태에서 강제 상태변환도 아니라면 무시하고 넘어간다.
            if(_enemy.isDead) return;
            //사망했다면 더이상 상태를 변경하지 않는다.
            
            CurrentState?.Exit(); //현재 상태에서 나갈 때 처리를 해줌
            CurrentState = _stateDictionary[newState]; //현재 상태를 새 상태로 전환
            CurrentState.Enter(); //새 상태의 진입 처리를 해줌
        }

        public void AddState(EnemyBehaviourType stateEnum, EnemyState state) //FSM에 상태를 등록하는 함수이다.
        {
            _stateDictionary.Add(stateEnum, state); // Enum(상태)와 EnemyState(상속받은놈 포함)
        }
    }
}