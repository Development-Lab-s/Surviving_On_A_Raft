using System;
using _00.Work.CheolYee._01.Codes.Projectiles;
using _00.Work.lusalord._02.Script.SO.AttackItem;
using _00.Work.Resource.SO;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Items.SO
{
    [CreateAssetMenu(fileName = "NewProjectileItem", menuName = "SO/Item/ProjectileItemSO")]
    public class ProjectileItemSo : AttackItemSo
    {
        public float speed;
        public float cooldown;
        public GameObject projectilePrefab;

        protected override void OnValidate()
        {
            if (projectilePrefab != null)
            {
                if (projectilePrefab.TryGetComponent(out Projectile projectile))
                {
                    itemName = projectile.GetComponent<IPoolable>().ItemName;
                }
                else
                {
                    projectilePrefab = null;
                    Debug.Log("얘는 발사체가 아닙니다.");
                }
            }
        }
    }
}