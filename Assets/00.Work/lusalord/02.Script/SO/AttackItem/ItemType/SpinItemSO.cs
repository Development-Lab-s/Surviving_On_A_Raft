using UnityEngine;

namespace _00.Work.lusalord._02.Script.SO.AttackItem.ItemType
{
    [CreateAssetMenu(fileName = "SpinItemSO", menuName = "SO/SpinItemSO")]
    public class SpinItemSO : AttackItemSO
    {
        public float spinRadius;
        public GameObject spinPrefab;
        public float spinSpeed;
        public int spinAmount;
        
    }
}
