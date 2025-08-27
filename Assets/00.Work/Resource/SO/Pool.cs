using System.Collections.Generic;
using UnityEngine;

namespace _00.Work.Resource.SO
{
    public class Pool
    {
        private readonly Stack<IPoolable> _pool;
        private readonly Transform _parent;
        private readonly IPoolable _poolable;
        private readonly GameObject _prefab;

        public Pool(IPoolable poolable, Transform parent, int initCount)
        {
            _pool = new Stack<IPoolable>();
            _parent = parent;
            _poolable = poolable;
            _prefab = poolable.GameObject;

            for (int i = 0; i < initCount; i++)
            {
                GameObject item = Object.Instantiate(_prefab, _parent);
                item.SetActive(false); //처음 생성되는 아이템은 꺼준다.
                item.name = _poolable.ItemName; //아이템 이름을 초기화시켜준다.
                IPoolable poolableItem = item.GetComponent<IPoolable>();
                _pool.Push(poolableItem);
            }
        }

        public IPoolable Pop()
        {
            IPoolable item;
            if (_pool.Count == 0)
            {
                Debug.Log($"새로 생성 {_prefab.name}");
                GameObject gameObj = Object.Instantiate(_prefab, _parent);
                gameObj.name = _poolable.ItemName;
                item = gameObj.GetComponent<IPoolable>();
            }
            else
            {
                item = _pool.Pop();
            }

            item.GameObject.SetActive(true);
            return item;
        }

        public void Push(IPoolable item)
        {
            item.GameObject.SetActive(false);
            _pool.Push(item);
        }
    }
}