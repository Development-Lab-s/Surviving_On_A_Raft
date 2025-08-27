using System.Collections.Generic;
using _00.Work.Resource.SO;
using UnityEngine;
using IPoolable = _00.Work.Resource.SO.IPoolable;

namespace _00.Work.Resource.Manager
{
    public class PoolManager : MonoSingleton<PoolManager>
    {
        [SerializeField] private PoolingListSo poolList;

        private Dictionary<string, Pool> _pools;

        protected override void Awake()
        {
            base.Awake();
            _pools = new Dictionary<string, Pool>();

            foreach (PoolItem item in poolList.items)
            {
                CreatePool(item.prefab, item.count);
            }
        }

        private void CreatePool(GameObject item, int count)
        {
            IPoolable poolable = item.GetComponent<IPoolable>();
            if (poolable == null)
            {
                Debug.LogError($"Item {item.name} does not implement IPoolable.");
                return;
            }
        
            Pool pool = new Pool(poolable, transform, count);
            _pools.Add(poolable.ItemName, pool); //이름을 기반으로 딕셔너리에 추가한다.
        }

        public IPoolable Pop(string itemName)
        {
            if (_pools.ContainsKey(itemName))
            {
                IPoolable item = _pools[itemName].Pop(); //해당 이름의 풀에서 꺼내서 넣어주고
                item.ResetItem(); //리셋해서 
                return item;
            }
            return null;
        }

        public void Push(IPoolable returnItem)
        {
            if (_pools.ContainsKey(returnItem.ItemName))
            {
                _pools[returnItem.ItemName].Push(returnItem); //풀에 반납한다.
            }
        }
    }
}