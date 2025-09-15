using _00.Work.CheolYee._01.Codes.Enemys.Attacks;
using _00.Work.CheolYee._01.Codes.Projectiles;
using UnityEngine;

public class MeteorObject : Projectile         // 민철이가 만든 총알과 유사하게 만드려 하는중
{
    [Header("Meteor Setting")]
    [SerializeField] protected float lifeTime = 2.5f;
    [SerializeField] protected float fallSpeed = 12f;
    [SerializeField] protected DamageCaster damageCaster;

    private float _damage;
    private float _knockBackPower;

    private MeteorPool _ownerPool;
    public void SetPool(MeteorPool pool) => _ownerPool = pool;

    public override void Initialize(Transform firePos, Vector2 _, float damage, float knockbackPower, float __)
    {
        InitializeAt(firePos.position, damage);
    }

    public void InitializeAt(Vector3 position, float damage)
    {
        _damage = damage;
        _knockBackPower = 0;

        transform.SetPositionAndRotation(position, Quaternion.identity);

        Timer = 0f;
        IsDead = false;
    }

    private void FixedUpdate()
    {
        // 아래로만 낙하
        transform.position += Vector3.down * (fallSpeed * Time.fixedDeltaTime);  // 리지드 바디로 하다가 오류떠서 transform으로 바꿈.

        // 수명 관리
        Timer += Time.fixedDeltaTime;
        if (Timer > lifeTime)
        {
            IsDead = true; // 시간 지나면 사라지게
            ReturnToPool();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("메테오 맞음");
        if (IsDead) return;
        

        // 총알과 같은 방식: DamageCaster로 데미지 처리
        if (damageCaster != null)
        {
            damageCaster.CastDamage(_damage, _knockBackPower);
        }


        ReturnToPool();
    }

    private void ReturnToPool()
    {
        IsDead = true;
        // PoolManager로 보내지 말고, 우리가 사용 중인 MeteorPool로 반환
        if (_ownerPool != null)
        {
            _ownerPool.Return(gameObject);
        }
        else
        {
            // 최후의 안전장치
            gameObject.SetActive(false);
        }
    }
}
