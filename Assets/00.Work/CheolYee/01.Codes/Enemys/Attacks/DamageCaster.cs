using _00.Work.CheolYee._01.Codes.Agents;
using _00.Work.hayul._01._Scripts.Sytem;
using _00.Work.Resource.Manager;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Enemys.Attacks
{
    public enum CasterType
    {
        Circle,
        Box
    }
    public class DamageCaster : MonoBehaviour
    {
        [Header("Damage Caster")]
        public CasterType casterType = CasterType.Circle;
        public ContactFilter2D whatIsTarget;
        public int detectCount = 1;

        [Header("Damage Text")] [SerializeField]
        private DamageText damageText;
        
        [Header("Circle Type")]
        public float damageRadius;
        
        [Header("Box Type")]
        public Vector2 boxSize;

        private Collider2D[] _resultArray;

        private void Awake()
        {
            _resultArray = new Collider2D[detectCount];
        }

        public bool CastDamage(float damage, float kbPower, Agent attacker = null)
        {
            int cnt;
            bool result = false;
            
            switch (casterType)
            {
                case CasterType.Circle:
                    cnt = Physics2D.OverlapCircle(transform.position, damageRadius, whatIsTarget, _resultArray);

                    for (int i = 0; i < cnt; i++)
                    {
                        if (_resultArray[i].TryGetComponent(out Agent agent))
                        {
                            DamageText text = PoolManager.Instance.Pop(damageText.ItemName) as DamageText;
                            if (text != null) text.SetText(damage, agent.transform);
                            
                            Vector2 direction = _resultArray[i].transform.position - transform.position;
                            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction.normalized, 
                                direction.magnitude, whatIsTarget.layerMask);
                    
                            agent.HealthComponent.TakeDamage(damage, hit.normal, kbPower, attacker);
                            result = true;
                        }
                    }

                    return result;
                case CasterType.Box:
                    cnt = Physics2D.OverlapBox(transform.position, boxSize, 0, whatIsTarget, _resultArray);

                    for (int i = 0; i < cnt; i++)
                    {
                        if (_resultArray[i].TryGetComponent(out Agent agent))
                        {
                            DamageText text = PoolManager.Instance.Pop(damageText.ItemName) as DamageText;
                            if (text != null) text.SetText(damage, agent.transform);
                            
                            Vector2 direction = _resultArray[i].transform.position - transform.position;
                            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction.normalized, 
                                direction.magnitude, whatIsTarget.layerMask);

                            agent.HealthComponent.TakeDamage(damage, hit.normal, kbPower, attacker);
                            result = true;
                        }
                    }

                    return result;

                default:
                    return false;
            }
        }
        
#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.magenta;
            if (casterType == CasterType.Box)
            {
                Gizmos.DrawWireCube(transform.position, boxSize);
            }
            else
            {
                Gizmos.DrawWireSphere(transform.position, damageRadius);
            }
        }
#endif
    }
}