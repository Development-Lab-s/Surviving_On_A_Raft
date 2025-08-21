using System;
using _00.Work.lusalord._02.Script.SO.AttackItem.ItemType;
using UnityEngine;

namespace _00.Work.lusalord._02.Script.ItemType
{
    public abstract class ItemTypeSpin : AttackItem
    {
        private SpinItemSO _spinItemSO;
        
        public float spinSpeed => _spinItemSO.spinSpeed;
        protected virtual void Start()
        {
            _spinItemSO = (SpinItemSO)attackItemSo;
        }
    }
}
