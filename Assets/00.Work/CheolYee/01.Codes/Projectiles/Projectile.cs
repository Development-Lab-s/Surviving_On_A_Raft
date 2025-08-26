using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Projectiles
{
    public abstract class Projectile : MonoBehaviour
    {
        [SerializeField] protected LayerMask targetLayer;

        protected bool IsDead;
        protected float Timer;
        
        protected Rigidbody2D RbCompo;

        protected virtual void Awake()
        {
            RbCompo = GetComponent<Rigidbody2D>();
        }
        
        public abstract void Initialize(Transform firePos, float damage, float knockbackPower);

        public void ResetItem()
        {
            IsDead = false;
            Timer = 0;
        }
    }
}