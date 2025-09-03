using UnityEngine;

namespace _00.Work.lusalord._02.Script.SO.AttackItem
{
    [CreateAssetMenu(fileName = "AttackItem", menuName = "SO/ItemType/AttackItem")]
    
    public abstract class AttackItemSo : ScriptableObject
    {
        public string itemName;
        public int level;
        public float damage;
        public float knockbackPower;
        public float atkRate;
        
        
    }
}
