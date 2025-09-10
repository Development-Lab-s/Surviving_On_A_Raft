using _00.Work.CheolYee._01.Codes.Enemys.Attacks;
using _00.Work.Resource.Manager;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Projectiles
{
    public class PlayerInductionBullet : Projectile
    {
        [Header("Induction Bullet Settings")]
        [SerializeField] protected float lifeTime = 1.5f; //총알 지속시간
        [SerializeField] private DamageCaster damageCaster; //데미지를 넣을 담당 컴포넌트
        
        private float _speed = 20f; //이동속도
        private float _damage; //데미지
        private float _knockBackPower; //넉백파워
        private Transform _target; //타겟
        
        public override void Initialize(Transform firePos, Transform targetPos, float damage, float knockbackPower, float shotSpeed)
        {
            _damage = damage; //데미지
            _knockBackPower = knockbackPower; //넉백량
            _target = targetPos;
            _speed = shotSpeed;
            
            Vector2 dir = (targetPos.position - firePos.position).normalized;
            
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.SetPositionAndRotation(firePos.position, Quaternion.Euler(0, 0, angle));
        }

        private void FixedUpdate()
        {
            if (_target == null) return;

            // 현재 위치에서 타겟 위치
            Vector2 dir = (_target.position - transform.position).normalized;
            RbCompo.linearVelocity = dir * _speed;
            
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            
            
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
            PoolManager.Instance.Push(this);
        }
    }
}