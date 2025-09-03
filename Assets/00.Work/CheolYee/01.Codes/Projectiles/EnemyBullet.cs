using _00.Work.CheolYee._01.Codes.Enemys.Attacks;
using _00.Work.CheolYee._01.Codes.SO;
using _00.Work.Resource.Manager;
using UnityEngine;
using IPoolable = _00.Work.Resource.SO.IPoolable;

namespace _00.Work.CheolYee._01.Codes.Projectiles
{
    public class EnemyBullet : Projectile
    {
        [Header("Bullet Settings")]
        [SerializeField] protected float lifeTime = 1.5f; //총알 지속시간
        [SerializeField] private DamageCaster damageCaster; //데미지를 넣을 담당 컴포넌트
        
        private float _shotSpeed = 20f; //이동속도
        private float _damage; //데미지
        private float _knockBackPower; //넉백파워
        private Vector2 _direction; //방향
        
        public override void Initialize(Transform firePos, Vector2 dir,  float damage, float knockbackPower, float shotSpeed)
        {
            _damage = damage; //데미지
            _knockBackPower = knockbackPower; //넉백량
            _direction = dir.normalized;
            _shotSpeed = shotSpeed;
            
            float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
            transform.SetPositionAndRotation(firePos.position, Quaternion.Euler(0, 0, angle));
        }

        private void FixedUpdate()
        {
            RbCompo.linearVelocity = _direction * _shotSpeed; //방향으로 날아가기
            Timer += Time.fixedDeltaTime; //타이머 초세기

            if (Timer > lifeTime)
            {
                IsDead = true;
                DestroyBullet(); //만약 날아가다가 라이프 타임 초과시 죽게 만들기
            }
        }

        private void OnTriggerEnter2D(Collider2D other) // 무언가 충돌했을 때
        {
            if (IsDead) return; //이미 죽어있다면 취소
            
            IsDead = true; //죽지 않았다면 죽었다고 설정
            
            damageCaster.CastDamage(_damage, _knockBackPower); //데미지 주기

            DestroyBullet(); //풀에 반납
        }

        private void DestroyBullet() // 풀에 반납
        {
            Debug.Log("총알 반납");
            PoolManager.Instance.Push(this);
        }
    }
}