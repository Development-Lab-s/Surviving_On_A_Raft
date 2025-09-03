using UnityEngine;
using UnityEngine.Serialization;

namespace _00.Work.lusalord._02.Script.SO
{
    [CreateAssetMenu(fileName = "PassiveItem", menuName = "SO/Item/PassiveItem")]
    public class PassiveItem : ScriptableObject
    {
        
        [Header("Item info")]
        public string itemName; // 아이템 이름
        public Sprite icon; // 아이콘
        public int level; // 현재 레벨
        
        public int id;
        
        [Header("Stats")]
        public float criticalChance;
        public float hp;
        public float speed;
        public float atk;
        public float atkSpeed;
        
        public PassiveItem nextLevelPassiveItem;
        
    }
}
