using UnityEngine;

namespace _00.Work.lusalord._02.Script.ItemType
{
    public abstract class ItemTypeProjectiile : AttackItem
    {
        public GameObject projectilePrefab;
        public string itemName;
        public float itemSpeed;
    
        private GameObject _projectile;

        protected virtual void SpawnProjectile(Vector3 spawnPosition)
        {
            _projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
            _projectile.name = itemName;
        }
    }
}
