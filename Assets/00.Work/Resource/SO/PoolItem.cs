using UnityEngine;

namespace _00.Work.Resource.SO
{
    [CreateAssetMenu(fileName = "PoolItem", menuName = "SO/Pool/Item", order = 0)]
    public class PoolItem : ScriptableObject
    {
        public string poolName;
        public GameObject prefab;
        public int count;

        private void OnValidate()
        {
            if (prefab == null) return;
            
            IPoolable item = prefab.GetComponent<IPoolable>();
            if (item == null)
            {
                prefab = null;
                return;
            }
            
            poolName = item.ItemName;
        }
    }
}