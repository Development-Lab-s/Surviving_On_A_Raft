using UnityEngine;

namespace _00.Work.lusalord._02.Script.SO
{
    [CreateAssetMenu(fileName = "AttackItem", menuName = "SO/ItemType/AttackItem")]
    
    public abstract class AttackItemSO : ScriptableObject
    {
        public string itemName;
        public int level;

        public float atkRate;
    }
}
