using _00.Work.CheolYee._01.Codes.Enemys.Attacks;
using _00.Work.Resource.Manager;
using UnityEngine;
using UnityEngine.Events;

namespace _00.Work.CheolYee._01.Codes.Projectiles
{
    public class Grenade : Projectile
    {
        public UnityEvent onExplosionEvent;
        
        [SerializeField] private float lifeTime = 5f;
        [SerializeField] private float torquePower;
        [SerializeField] private DamageCaster damageCaster; //데미지를 넣을 담당 컴포넌트
        [SerializeField] private ContactFilter2D enemyLayer; //에너미 레이어

        private float _speed;
        private float _damage;
        private float _knockbackPower;
        
        private float _timer;
        public override void Initialize(Transform firePos, Vector2 dir, float damage, float knockbackPower, float shotSpeed)
        {
            _timer = 0;
            _damage = damage;
            _knockbackPower = knockbackPower;
            _speed = shotSpeed;
            transform.position = firePos.position;
            float xDir = Random.Range(-1f, 1f); // 좌우 랜덤
            float yDir = Random.Range(0.8f, 1.2f); // 항상 위쪽으로 가게
            Vector2 forceDir = new Vector2(xDir, yDir).normalized;

            RbCompo.AddForce(forceDir * _speed, ForceMode2D.Impulse);
            RbCompo.AddTorque(torquePower, ForceMode2D.Impulse);
        }
        
        private void Update()
        {
            _timer += Time.deltaTime;
            if (_timer >= lifeTime)
            {
                Explosion();
            }
        }

        private void Explosion()
        {
            onExplosionEvent?.Invoke();
            
            damageCaster.CastDamage(_damage, _knockbackPower);
            
            PoolManager.Instance.Push(this);
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            int collisionLayerMask = 1 << other.gameObject.layer;
            if ((collisionLayerMask & enemyLayer.layerMask) > 1)
            {
                Explosion();
            }
        }
    }
}