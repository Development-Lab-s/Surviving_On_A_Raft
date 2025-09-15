using _00.Work.CheolYee._01.Codes.Enemys.Attacks;
using _00.Work.CheolYee._01.Codes.Projectiles;
using _00.Work.Resource.Manager;
using UnityEngine;
using UnityEngine.Events;

public class InstallAnvil : Projectile
{ 
    [SerializeField] private float lifeTime = 10f;
    [SerializeField] private float torquePower;
    [SerializeField] private DamageCaster damageCaster; //데미지를 넣을 담당 컴포넌트
    [SerializeField] private ContactFilter2D enemyLayer; //에너미 레이어

    private float _damage;
    private float _knockbackPower;
        
    private float _timer;
    public override void Initialize(Transform firePos, Vector2 dir, float damage, float knockbackPower, float shotSpeed)
    {
        RbCompo.linearVelocity = Vector3.zero;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        _timer = 0;
        _damage = damage;
        _knockbackPower = knockbackPower;
        transform.position = firePos.position;
        RbCompo.AddForce(new Vector3(Random.Range(-4f, 4f), 7f, 0), ForceMode2D.Impulse);
    }
        
    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= lifeTime)
        {
            Stabbing();
        }
    }

    private void Stabbing()
    {
        damageCaster.CastDamage(_damage, _knockbackPower);
        PoolManager.Instance.Push(this);
    }
        
    private void OnCollisionEnter2D(Collision2D other)
    {
        int collisionLayerMask = 1 << other.gameObject.layer;
        if ((collisionLayerMask & enemyLayer.layerMask) > 1)
        {
            Stabbing();
        }
    }
}
