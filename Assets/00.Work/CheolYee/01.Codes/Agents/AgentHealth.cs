using System.Collections.Generic;
using _00.Work.Resource.Manager;
using UnityEngine;
using UnityEngine.Events;

namespace _00.Work.CheolYee._01.Codes.Agents
{
    public class AgentHealth : MonoBehaviour
    {
        public UnityEvent onHit;
        public UnityEvent onDeath;

        private float _maxHealth = 150f;
        private float _currentHealth;

        private Dictionary<string, float> _healthMultipliers = new();

        public float CurrentHealth => _currentHealth;

        public float MaxHealth => _maxHealth * TotalMultiplier;

        public float NormalizedHealth => _currentHealth / MaxHealth;


        private Agent _owner;

        public void Initialize(Agent owner, float health)
        {
            _maxHealth = health;
            _owner = owner;
            ResetHealth();
        }
        private float TotalMultiplier
        {
            get
            {
                float result = 1f;
                foreach (var kv in _healthMultipliers)
                    result += kv.Value;
                return result;
            }
        }

        public void AddMultiplier(string source, float multiplier)
        {
            _healthMultipliers[source] = multiplier;
            RecalculateHealth();
        }

        public void RemoveMultiplier(string source)
        {
            if (_healthMultipliers.ContainsKey(source))
                _healthMultipliers.Remove(source);
            RecalculateHealth();
        }

        private void RecalculateHealth()
        {
            float ratio = _currentHealth / MaxHealth; // 현재 비율 유지
            _currentHealth = MaxHealth * ratio;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, MaxHealth);
        }


        public void ResetHealth()
        {
            _currentHealth = MaxHealth;
        }

        public void Heal(float healAmount)
        {
            _currentHealth = Mathf.Clamp(_currentHealth + healAmount, 0, MaxHealth);
        }
        public void HealPer(float percent)
        {
            float healAmount = MaxHealth * percent;
            Heal(healAmount);
        }

        public void TakeDamage(float amount, Vector2 normal, float kbPower, Agent attacker = null)
        {
            Debug.Assert(_owner != null, $"{nameof(_owner)} 의 체력이 초기화되지 않았습니다.");

            SoundManager.Instance.PlaySfx("HIT");
            
            _currentHealth -= amount;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, MaxHealth);
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