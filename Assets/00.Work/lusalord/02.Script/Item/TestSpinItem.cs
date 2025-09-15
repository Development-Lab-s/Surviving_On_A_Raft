using System;
using _00.Work.lusalord._02.Script.ItemType;
using _00.Work.lusalord._02.Script.SO.AttackItem.ItemType;
using UnityEngine;

namespace _00.Work.lusalord._02.Script.SO
{
    public class TestSpinItem : ItemTypeSpin
    {
        
        protected override void Awake()
        {
            base.Awake();
            
        }

        private void OnValidate()
        {
            if (_spinItemSo.flip)
            {
                transform.Rotate(0, 180, 0);
            }
        }
    }
}
