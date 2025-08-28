using System;
using UnityEngine;

namespace _00.Work.lusalord._02.Script.ItemType
{
    public abstract class ItemTypeForward : AttackItem
    {

        public string itemName;
        private ForwardItemSO _forwardItemSo;

        private float _coolTime = 3;
        public Transform pos;
        protected virtual void Awake()
        {
            _forwardItemSo = (ForwardItemSO)attackItemSo;

            _coolTime = _forwardItemSo.coolTime;
        }

        private void Update()
        {
            float curTime = Time.deltaTime;
            Debug.Log(curTime);
            if (curTime >= _coolTime)
            {
                Collider2D[] overab = Physics2D.OverlapBoxAll(pos.position, _forwardItemSo.size, 0);
                Debug.Log("병건이 바보");
                curTime = 0;
            }
        }
    }
}
