using System.Collections.Generic;
using UnityEngine;

namespace _00.Work.lusalord._02.Script.SO.AttackItem.ItemType
{
    [CreateAssetMenu(fileName = "SpinItemSO", menuName = "SO/Item/SpinItemSO")]
    public class SpinItemSo : AttackItemSo
    {
        public float spinRadius;
        public GameObject spinPrefab;
        public float spinSpeed;
        public int spinAmount;
        
        public List<GameObject> spinItems = new List<GameObject>();
        public bool isRotate;

    }
}
