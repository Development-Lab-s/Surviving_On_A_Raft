using _00.Work.lusalord._02.Script.SO.AttackItem.ItemType;
using UnityEngine;

namespace _00.Work.lusalord._02.Script.ItemType
{
    public abstract class ItemTypeSpin : AttackItem
    {
        private SpinItemSo _spinItemSo;


        protected float SpinSpeed => _spinItemSo.spinSpeed;
        
        protected virtual void Awake()
        {
            _spinItemSo = (SpinItemSo)attackItemSo;
            Spawn();
        }
        public void Spawn()
        {
            GameObject spawnItem = Instantiate(_spinItemSo.spinPrefab, transform);
            spawnItem.transform.position = transform.position + new Vector3(0, _spinItemSo.spinRadius, 0);
        }
    }
}
