using _00.Work.lusalord._02.Script.SO.AttackItem.ItemType;
using UnityEngine;

namespace _00.Work.lusalord._02.Script.ItemType
{
    public abstract class ItemTypeForward : AttackItem
    {
        public GameObject forwardPrefab;
        public string itemName;
        public float size;
        
        private SpinItemSo _spinItemSo;

        protected virtual void Awake()
        {
            _spinItemSo = (SpinItemSo)attackItemSo;
        }
        
        
    }
}
