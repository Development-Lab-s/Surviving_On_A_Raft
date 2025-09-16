using UnityEngine;
using UnityEngine.Serialization;

namespace _00.Work.CheolYee._01.Codes.Items.PassiveItems
{
    [CreateAssetMenu(fileName = "PassiveItem", menuName = "SO/Item/PassiveItem")]
    public class PassiveItemSo : ScriptableObject
    {
        [Header("Item info")]
        public int id;
        public int level; // 현재 레벨
        public string itemName; // 아이템 이름
        public Sprite icon; // 아이콘
        [TextArea]
        public string desc;
        
        
        [Header("Stats")]
        public float criticalChanceMulti;
        public float hpMulti;
        public float speedMulti;
        public float damageMulti;
        public float atkSpeedMulti;
        
        public PassiveItemSo nextLevelPassiveItem;
        
        public PassiveItemSo TryPassiveItemLevelUp()
        {
            if (IsLevelUpPossible()) return nextLevelPassiveItem;
            return this;
        }
        
        public bool IsLevelUpPossible()
        {
            return nextLevelPassiveItem != null;
        }
        
    }
}
