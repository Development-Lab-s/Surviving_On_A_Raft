using UnityEngine;
using UnityEngine.Serialization;

namespace _00.Work.lusalord._02.Script.ItemType
{
    public abstract class ItemTypeForward : AttackItem
    {
        public GameObject forwardPrefab;
        public string itemName;
        public float itemSpeed;
    
        private GameObject _forward;

        protected virtual void SpawnProjectile(Vector3 spawnPosition)
        {
            _forward = Instantiate(forwardPrefab, spawnPosition, Quaternion.identity);
            _forward.name = itemName;
        }
    }
}
