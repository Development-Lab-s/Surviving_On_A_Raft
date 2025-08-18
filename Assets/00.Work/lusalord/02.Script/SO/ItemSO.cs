using UnityEngine;

namespace _00.Work.lusalord._02.Script.SO
{
    public enum ItemType
    {
        Passive,
        Attack
    }
    
    public abstract class ItemSO : ScriptableObject
    {
        [SerializeField] private string itemName;   // 아이템 이름
        [SerializeField] private Sprite icon;       // 아이콘
        [SerializeField] private int level = 1;     // 현재 레벨
        [SerializeField] private int maxLevel = 10; // 최대 레벨

        public string ItemName => itemName;
        public Sprite Icon => icon;
        public int Level => level;
        public int MaxLevel => maxLevel;

        public abstract ItemType Type { get; }

        // 아이템 레벨업
        public virtual void LevelUp()
        {
            if (level < maxLevel)
            {
                level++;
            }
        }
    }
}
