using _00.Work.CheolYee._01.Codes.Items.SO;
using _00.Work.lusalord._02.Script;
using _00.Work.Resource.SO;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Items
{
    public abstract class ItemTypeProjectile : AttackItem
    {
        protected ProjectileItemSo CurrentProjectileSo;
        protected string BullItemName;
        protected float Damage;
        protected float KnockbackPower;
        protected float Speed;
        protected float Cooldown => _coolDown / (1f + Player.CurrentAttackSpeed);

        protected float LastSpawnTime = -1;

        private float _coolDown;
        
        protected override void Awake()
        {
            base.Awake();
            CurrentProjectileSo = (ProjectileItemSo)attackItemSo;
            Damage = CurrentProjectileSo.damage;
            KnockbackPower = CurrentProjectileSo.knockbackPower;
            Speed = CurrentProjectileSo.speed;
            _coolDown = CurrentProjectileSo.cooldown;
            BullItemName = CurrentProjectileSo.projectilePrefab.GetComponent<IPoolable>().ItemName;
        }

        public override void ApplySetting()
        {
            CurrentProjectileSo = (ProjectileItemSo)attackItemSo;
            Damage = CurrentProjectileSo.damage;
            KnockbackPower = CurrentProjectileSo.knockbackPower;
            Speed = CurrentProjectileSo.speed;
            _coolDown = CurrentProjectileSo.cooldown;
        }

        protected virtual void SpawnProjectile()
        {
            LastSpawnTime = Time.time;
        }
    }
}
