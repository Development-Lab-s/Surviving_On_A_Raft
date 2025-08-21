using _00.Work.CheolYee._01.Codes.Agents;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Enemy.Attacks
{
    public class DamageCaster : MonoBehaviour
    {
        public ContactFilter2D whatIsTarget;
        public float damageRadius;
        public int detectCount = 1;

        private Collider2D[] _resultArray;

        private void Awake()
        {
            _resultArray = new Collider2D[detectCount];
        }

        public bool CastDamage(float damage, float kbPower)
        {
            int cnt = Physics2D.OverlapCircle(transform.position, damageRadius, whatIsTarget, _resultArray);

            for (int i = 0; i < cnt; i++)
            {
                if (_resultArray[i].TryGetComponent(out Agent agent))
                {
                    Vector2 direction = _resultArray[i].transform.position - transform.position;
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, direction.normalized, 
                        direction.magnitude, whatIsTarget.layerMask);
                    
                    agent.HealthComponent.TakeDamage(damage, hit.normal, hit.point, kbPower);
                }
            }

            return cnt > 0;
        }
        
#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, damageRadius);
        }
#endif
    }
}