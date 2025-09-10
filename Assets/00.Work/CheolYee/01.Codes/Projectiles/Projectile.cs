using _00.Work.Resource.SO;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Projectiles
{
    public abstract class Projectile : MonoBehaviour, IPoolable
    {
        [SerializeField] protected string itemName;
        public string ItemName => itemName;
        public GameObject GameObject => gameObject;
        
        protected bool IsDead;
        protected float Timer;
        
        protected Rigidbody2D RbCompo;

        protected virtual void Awake()
        {
            RbCompo = GetComponent<Rigidbody2D>();
        }
        
        public virtual void Initialize(Transform firePos, Vector2 dir, float damage, float knockbackPower, float shotSpeed) {}
        public virtual void Initialize(Transform firePos, Transform target, float damage, float knockbackPower, float shotSpeed) {}


        public void ResetItem()
        {
            IsDead = false;
            Timer = 0;
        }
    }
}