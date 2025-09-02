using _00.Work.CheolYee._01.Codes.Items.SO;
using _00.Work.lusalord._02.Script;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Items
{
    public abstract class ItemTypeProjectile : AttackItem
    {
        protected ProjectileItemSo CurrentProjectileSo;
        protected GameObject ProjectilePrefab;
        protected float Damage;
        protected float KnockbackPower;
        protected float Speed;
        protected float Cooldown;

        protected float LastSpawnTime;
        
        private void Awake()
        {
            CurrentProjectileSo = (ProjectileItemSo)attackItemSo;
            Damage = CurrentProjectileSo.damage;
            KnockbackPower = CurrentProjectileSo.knockbackPower;
            Speed = CurrentProjectileSo.speed;
            Cooldown = CurrentProjectileSo.atkRate;
        }

        protected virtual void SpawnProjectile()
        {
            LastSpawnTime = Time.time;
        }
    }
}
