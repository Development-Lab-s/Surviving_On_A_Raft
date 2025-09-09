using _00.Work.CheolYee._01.Codes.Items.SO;
using _00.Work.lusalord._02.Script;
using _00.Work.lusalord._02.Script.SO.AttackItem.ItemType;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Items
{
    public abstract class ItemTypeInstall : AttackItem
    {
        protected InstallItemSo InstallItemSo;
        protected float Damage;
        protected float KnockbackPower;
        protected float Speed;
        protected float Cooldown => _cooldown / (1f+ Player.CurrentAttackSpeed);
        protected int GrenadeCount;
        
        protected float LastSpawnTime;

        private float _cooldown;
        
        protected override void Awake()
        {
            base.Awake();
            InstallItemSo = (InstallItemSo)attackItemSo;
            Damage = InstallItemSo.damage;
            KnockbackPower = InstallItemSo.knockbackPower;
            Speed = InstallItemSo.speed;
            GrenadeCount = InstallItemSo.grenadeCount;
            _cooldown = InstallItemSo.cooldown;
        }
        
        protected virtual void SpawnProjectile()
        {
            LastSpawnTime = Time.time;
        }
    }
}
