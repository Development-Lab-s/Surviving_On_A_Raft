using _00.Work.CheolYee._01.Codes.Enemys.Attacks;
using _00.Work.CheolYee._01.Codes.SO;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Projectiles
{
    public class NetProjectile : MonoBehaviour
    {
        [Header("Bullet Settings")]
        [SerializeField] protected float speed = 20f; //이동속도
        [SerializeField] protected float size = 1; //사이즈 배율
        [SerializeField] private DamageCaster damageCaster; //데미지를 넣을 담당 컴포넌트
        [SerializeField] private SpriteRenderer visual;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private float wallCheckDistance;
        [SerializeField] private Transform wallCheckTransform;
         
        private BoxCollider2D _collider2D;
        private Rigidbody2D _rigidbody;
        
        private float _damage; //데미지
        private float _knockBackPower; //넉백파워
        private Vector2 _direction; //방향

        private void Awake()
        {
            _collider2D = GetComponent<BoxCollider2D>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Initialize(Transform firePos, PlayerSkillDataSo playerSkillData)
        {
            _damage = playerSkillData.skillDamage;
            _knockBackPower = playerSkillData.skillKnockBackPower;
            size = playerSkillData.skillRange;
            _direction = firePos.right;
            
            transform.position = firePos.position;
            
            _collider2D.size = new Vector2(_collider2D.size.x, _collider2D.size.y * size);
            visual.size = new Vector2(_collider2D.size.x, _collider2D.size.y);
        }
        
        private void FixedUpdate()
        {
            _rigidbody.linearVelocity = _direction * speed; //방향으로 날아가기

            if (IsWallDetected())
            {
                damageCaster.CastDamage(_damage, _knockBackPower);
                Destroy(gameObject);
            }
        }

        private bool IsWallDetected() //전방에 레이를 쏴 감지되었는지 판별
        {
            return Physics2D.Raycast(wallCheckTransform.position, wallCheckTransform.right, wallCheckDistance, groundLayer);
        }

#if  UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(wallCheckTransform.position, wallCheckTransform.position + wallCheckTransform.right * wallCheckDistance);
        }
#endif
    }
}