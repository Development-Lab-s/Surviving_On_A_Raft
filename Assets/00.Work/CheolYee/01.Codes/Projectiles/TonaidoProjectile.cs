using System.Collections;
using _00.Work.CheolYee._01.Codes.Enemys.Attacks;
using _00.Work.Resource.Manager;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Projectiles
{
    public class TonaidoProjectile : Projectile
    {
        private static readonly int Start = Animator.StringToHash("Start");

        [Header("PlayerPenetrationBullet Settings")]
        [SerializeField] private DamageCaster damageCaster; //데미지를 넣을 담당 컴포넌트
        [SerializeField] private Animator animator; //애니메이터
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private bool continuousAttack;
        
        private float _speed = 20f; //이동속도
        private float _damage; //데미지
        private float _knockBackPower; //넉백파워
        private Vector2 _direction; //방향
        
        public override void Initialize(Transform firePos, Vector2 dir, float damage, float knockbackPower, float shotSpeed)
        {
            _damage = damage; //데미지
            _knockBackPower = knockbackPower; //넉백량

            _direction = new Vector2(dir.x, 0);
            _speed = shotSpeed;
            transform.position = firePos.position;
            
            var scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * (_direction.x < 0 ? -1 : 1);
            transform.localScale = scale;
            animator.SetTrigger(Start);
            
        }

        public void Forward()
        {
            SoundManager.Instance.PlaySfx("TONAIDO");
            rb.AddForce(_direction * _speed, ForceMode2D.Impulse);   
        }

        public void DamageCast()
        {
            damageCaster.CastDamage(_damage, _knockBackPower);
        }

        private void DestroyBullet() // 풀에 반납
        {
            PoolManager.Instance.Push(this);
        }
    }
}