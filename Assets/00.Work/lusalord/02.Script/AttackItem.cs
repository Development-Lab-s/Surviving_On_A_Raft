using _00.Work.lusalord._02.Script.SO.AttackItem;
using UnityEngine;

namespace _00.Work.lusalord._02.Script
{
    public abstract class AttackItem : MonoBehaviour
    {
        public AttackItemSo attackItemSo;

        [Header("Item Settings")]
        public string ItemName => attackItemSo.itemName;
        public int Level => attackItemSo.level;
        public float ItemRate => attackItemSo.atkRate;
        
        
        
        public void LevelUp()
        {
            
        }
    }
}
