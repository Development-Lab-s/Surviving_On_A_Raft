using _00.Work.CheolYee._01.Codes.Players;
using _00.Work.lusalord._02.Script.SO.AttackItem;
using UnityEngine;

namespace _00.Work.lusalord._02.Script
{
    public abstract class AttackItem : MonoBehaviour
    {
        public AttackItemSo attackItemSo;
        
        protected Player Player;

        [Header("Item Settings")]
        public string ItemName => attackItemSo.itemName;
        public int Level => attackItemSo.level;

        protected virtual void Awake()
        {
            Player = GetComponentInParent<Player>();
        }

        public void LevelUp()
        {
            
        }
    }
}
