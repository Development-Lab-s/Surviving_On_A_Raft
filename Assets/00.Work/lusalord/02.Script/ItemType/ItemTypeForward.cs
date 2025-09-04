using System;
using _00.Work.CheolYee._01.Codes.Enemys.Attacks;
using UnityEngine;

namespace _00.Work.lusalord._02.Script.ItemType
{
    public abstract class ItemTypeForward : AttackItem
    {
        [SerializeField] private DamageCaster damageCaster;
        
        private ForwardItemSO _forwardItemSo;

        private float time = 0;

        private float _coolTime = 3;
        public Transform pos;
        protected virtual void Awake()
        {
            _forwardItemSo = (ForwardItemSO)attackItemSo;

            gameObject.name = _forwardItemSo.itemName;
            _coolTime = _forwardItemSo.coolTime;
        }

        protected virtual void Update()
        {
            time += Time.deltaTime;

            if (_coolTime <= time)
            {
                damageCaster.CastDamage(10, 4);
                time = 0;
            }
        }
    }
}
