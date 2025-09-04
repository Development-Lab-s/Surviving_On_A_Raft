using UnityEngine;
using UnityEngine.Events;

namespace _00.Work.CheolYee._01.Codes.Agents
{
    public class AgentHealth : MonoBehaviour
    {
        public UnityEvent onHit;
        public UnityEvent onDeath;

        [SerializeField] protected float healthMulti = 1f;
        
        private float _maxHealth = 150f;
        private float _currentHealth;
        
        private float CurrentHealth { get => _currentHealth * healthMulti; 
            set => _currentHealth = value * healthMulti; }
        
        private Agent _owner;

        public void Initialize(Agent owner, float health)
        {
            _maxHealth = health;
            _owner = owner;
            ResetHealth();
        }

        public void ResetHealth()
        {
            _currentHealth = _maxHealth;
        }

        public void TakeDamage(float amount, Vector2 normal, Vector2 point, float kbPower)
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