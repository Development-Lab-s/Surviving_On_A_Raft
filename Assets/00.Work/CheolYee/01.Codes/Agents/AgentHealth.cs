using UnityEngine;
using UnityEngine.Events;

namespace _00.Work.CheolYee._01.Codes.Agents
{
    public class AgentHealth : MonoBehaviour
    {
        public UnityEvent onHit;
        public UnityEvent onDeath;

        [SerializeField] private float maxHealth = 150f;
        
        private float _currentHealth;
        private Agent _owner;

        public void Initialize(Agent owner, float health)
        {
            maxHealth = health;
            _owner = owner;
            ResetHealth();
        }

        public void ResetHealth()
        {
            _currentHealth = maxHealth;
        }

        public void TakeDamage(float amount, Vector2 normal, Vector2 point, float kbPower)
        {
            _currentHealth -= amount;
            onHit?.Invoke();

            if (kbPower > 0)
            {
                //노말은 피격지점의 수직인 벡터니까 -1을 곱하면 피격 방향 벡텀가 나오게 된다
                _owner.MovementComponent.GetKnockBack(normal * -1, kbPower);
            }
            
            if (_currentHealth <= 0)
            {
                onDeath?.Invoke();
            }
        }
    }
}