using UnityEngine;
using UnityEngine.Events;

namespace _00.Work.CheolYee._01.Codes.Agent
{
    public class AgentHealth : MonoBehaviour
    {
        public UnityEvent onHit;
        public UnityEvent onDeath;

        [SerializeField] private float maxHealth = 150f;
        
        private float _currentHealth;

        public void Initialize(float health)
        {
            maxHealth = health;
        }

        public void ResetHealth()
        {
            _currentHealth = maxHealth;
        }

        public void TakeDamage(float damage)
        {
            _currentHealth -= damage;
            onHit?.Invoke();
            
            if (_currentHealth <= 0)
            {
                onDeath?.Invoke();
            }
        }
    }
}