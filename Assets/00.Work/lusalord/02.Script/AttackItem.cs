using _00.Work.lusalord._02.Script.SO;
using _00.Work.lusalord._02.Script.SO.AttackItem.ItemType;
using UnityEngine;
using UnityEngine.Serialization;

namespace _00.Work.lusalord._02.Script
{
    public abstract class AttackItem : MonoBehaviour
    {
        public AttackItemSO attackItemSo;

        [Header("Item Settings")]
        public string ItemName => attackItemSo.itemName;
        public int Level => attackItemSo.level;
        public float ItemRate => attackItemSo.atkRate;
        
    }
}
