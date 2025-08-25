using System.Collections.Generic;
using UnityEngine;

namespace _00.Work.lusalord._02.Script.SO.AttackItem.ItemType
{
    [CreateAssetMenu(fileName = "SpinItemSO", menuName = "SO/SpinItemSO")]
    public class SpinItemSo : AttackItemSo
    {
        public float spinRadius;
        public GameObject spinPrefab;
        public float spinSpeed;
        public int spinAmount;
        
        public List<GameObject> spinItems = new List<GameObject>();
        private void OnValidate()
        {
            if (spinAmount < 0)
            {
                spinAmount = 0;
            }

            if (spinItems.Count != spinAmount)
            {
                if (spinItems.Count < spinAmount)
                {
                    while (spinItems.Count < spinAmount)
                    {
                        GameObject spawnItem = Instantiate(spinPrefab);
                        spinItems.Add(spawnItem);
                    }
                }
                else
                {
                    while (spinItems.Count > spinAmount)
                        spinItems.RemoveAt(spinItems.Count - 1);
                }
            }
        }
    }
}
