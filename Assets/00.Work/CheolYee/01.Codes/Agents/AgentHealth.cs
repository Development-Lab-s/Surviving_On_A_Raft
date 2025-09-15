using _00.Work.CheolYee._01.Codes.Managers;
using _00.Work.CheolYee._01.Codes.SO;
using UnityEngine;
using UnityEngine.Events;

namespace _00.Work.CheolYee._01.Codes.Agents
{
    public class AgentHealth : MonoBehaviour
    {
        public EnemyDataSo data;
        public UnityEvent onHit;
        public UnityEvent onDeath;

        public float HealthMulti { get; set; } = 1f;

        private float _maxHealth = 150f;
        private float _currentHealth;

        public float MaxHealth => _maxHealth * HealthMulti;
        public float NormalizedHealth => Mathf.Approximately(MaxHealth, 0f) ? 0f : CurrentHealth / MaxHealth;

        public float CurrentHealth
        {
            get => _currentHealth * HealthMulti;
            set
            {
                // 가능하면 여기 코드를 안바꾸고 싶은데 내가 생각하는 방향대로라면 이 코드 수정이 불가피함. 나중에 문제 생기면 바꿔도 됨.
                _currentHealth = Mathf.Clamp(value, 0, _maxHealth * HealthMulti);
            }
            /* get => _currentHealth * HealthMulti;
             set
             {
                 _currentHealth += value;
                 _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth * HealthMulti);
             }*/
        }

        private Agent _owner;

        public void Initialize(Agent owner, float health)
        {
            HealthMulti = StatManager.Instance.GetEnemyBuff(StatType.Health);
            _maxHealth = health * HealthMulti;
            _owner = owner;
            ResetHealth();
        }

        public void ResetHealth()
        {
            _currentHealth = _maxHealth;
        }

        public void TakeDamage(float amount, Vector2 normal, float kbPower, Agent attacker = null)
        {
            Debug.Assert(_owner != null, $"{nameof(_owner)} 의 체력이 초기화되지 않았습니다.");

            CurrentHealth -= amount;
            onHit?.Invoke();

            if (kbPower > 0)
            {
                //노말은 피격지점의 수직인 벡터니까 -1을 곱하면 피격 방향 벡텀가 나오게 된다
                _owner.MovementComponent.GetKnockBack(normal * -1, kbPower);
            }

            if (CurrentHealth <= 0)
            {
                onDeath?.Invoke();
            }
        }
    }
}