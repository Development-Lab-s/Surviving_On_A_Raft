using System;
using UnityEngine;

namespace _00.Work.lusalord._02.Script.ItemType
{
    public abstract class ItemTypeForward : AttackItem
    {
        private ForwardItemSO _forwardItemSo;

        private float _coolTime = 3;
        public Transform pos;
        protected virtual void Awake()
        {
            gameObject.name = _forwardItemSo.itemName;
            _forwardItemSo = (ForwardItemSO)attackItemSo;

            _coolTime = _forwardItemSo.coolTime;
        }

        private void Update()
        {
            float curTime = Time.deltaTime;
            Debug.Log(curTime);
            
        }
    }
}
