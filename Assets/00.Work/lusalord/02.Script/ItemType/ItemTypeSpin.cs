using System;
using System.Collections.Generic;
using _00.Work.lusalord._02.Script.SO.AttackItem.ItemType;
using UnityEngine;

namespace _00.Work.lusalord._02.Script.ItemType
{
    public abstract class ItemTypeSpin : AttackItem
    {
        private SpinItemSO _spinItemSO;
        
        
        public List<GameObject> spinItems;
        public float spinSpeed => _spinItemSO.spinSpeed;
        protected virtual void Awake()
        {
            _spinItemSO = (SpinItemSO)attackItemSo;
            Spawn();
        }

        public void Spawn()
        {
            GameObject spawnItem = Instantiate(_spinItemSO.spinPrefab, transform);

            spawnItem.transform.position = transform.position + new Vector3(0, _spinItemSO.spinRadius, 0);
            
            spinItems.Add(spawnItem);
        }
    }
}
