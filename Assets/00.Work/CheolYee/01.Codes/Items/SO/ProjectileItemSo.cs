using System;
using _00.Work.CheolYee._01.Codes.Projectiles;
using _00.Work.lusalord._02.Script.SO.AttackItem;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Items.SO
{
    [CreateAssetMenu(fileName = "NewProjectileItem", menuName = "SO/Item/ProjectileItemSO")]
    public class ProjectileItemSo : AttackItemSo
    {
        public float speed;
        public GameObject projectilePrefab;

        private void OnValidate()
        {
            if (projectilePrefab != null && projectilePrefab.TryGetComponent(out Projectile projectile))
            {
                itemName = projectile.ItemName;
            }
        }
    }
}