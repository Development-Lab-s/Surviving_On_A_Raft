using System;
using UnityEngine;

namespace _00.Work.lusalord._02.Script.SO.AttackItem
{
    [CreateAssetMenu(fileName = "AttackItem", menuName = "SO/ItemType/AttackItem")]
    
    public abstract class AttackItemSo : ScriptableObject
    {
        [Header("AttackItem Settings")] 
        public int id;
        public string itemName;
        [TextArea]
        public string desc;
        public Sprite icon;
        public int level;
        public float damage;
        public float knockbackPower;

        public AttackItemSo nextLevel;

        public AttackItemSo TryAttackItemLevelUp()
        {
            if (IsLevelUpPossible()) return nextLevel;
            return this;
        }
        
        public bool IsLevelUpPossible()
        {
            return nextLevel != null;
        }

        protected abstract void OnValidate();
    }
}
