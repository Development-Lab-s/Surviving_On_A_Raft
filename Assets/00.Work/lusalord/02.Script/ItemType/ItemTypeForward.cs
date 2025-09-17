using _00.Work.CheolYee._01.Codes.Enemys.Attacks;
using UnityEngine;

namespace _00.Work.lusalord._02.Script.ItemType
{
    public abstract class ItemTypeForward : AttackItem
    {

        [Header("Forward Settings")]
        [SerializeField] private DamageCaster damageCaster;
        
        [SerializeField] public Animator animator;
        
        private ForwardItemSO _forwardItemSo;
        private float CoolTime => _coolTime / (1f + Player.CurrentAttackSpeed);
        private float _coolTime = 3;
        private float _timer;
        
        private static readonly int ForwardAttack = Animator.StringToHash("ForwardAttack");
        protected override void Awake()
        {
            base.Awake();
            _forwardItemSo = (ForwardItemSO)attackItemSo;

            animator.runtimeAnimatorController = _forwardItemSo.animatorController;
            _coolTime = _forwardItemSo.coolTime;
        }

        public override void ApplySetting()
        {
            _forwardItemSo = (ForwardItemSO)attackItemSo;
            _coolTime = _forwardItemSo.coolTime;
        }

        protected virtual void Update()
        {
            _timer += Time.deltaTime;

            if (CoolTime <= _timer)
            {
                animator.SetTrigger(ForwardAttack);
                _timer = 0;
            }
        }

        public virtual void AnimateForwardAttack()
        {
            damageCaster.CastDamage(Player.CurrentDamage + _forwardItemSo.damage, _forwardItemSo.knockbackPower);
        }
    }
}
