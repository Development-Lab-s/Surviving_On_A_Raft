using System.Collections.Generic;
using _00.Work.lusalord._02.Script.Item;
using UnityEngine;

namespace _00.Work.lusalord._02.Script.SO.AttackItem.ItemType
{
    [CreateAssetMenu(fileName = "SpinItemSO", menuName = "SO/Item/SpinItemSO")]
    public class SpinItemSo : AttackItemSo
    {
        [Header("Spin Item")]
        public float spinRadius;
        public GameObject spinPrefab;
        public float spinSpeed;
        public int spinAmount;
        public float rotateSpeed;
        
        [Header("Spin Setting")]
        public bool isRotate;
        public bool rickRolling;
        public bool flip;

        protected override void OnValidate()
        {
            if (spinPrefab != null)
            {
                if (spinPrefab.TryGetComponent(out SpinCaster _))
                {
                 }
                else
                {
                    spinPrefab = null;
                    Debug.Log("얘는 스핀 캐스터가 아닙니다.");
                }
            }
        }
    }
}
