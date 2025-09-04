using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.SO
{
    [CreateAssetMenu(fileName = "NewCharacterData", menuName = "SO/Player/CharacterDataSo")]
    public class CharacterDataSo : ScriptableObject
    {
        [Header("CharacterData")]
        public float health = 100f;
        public float moveSpeed = 5;
        public float jumpForce = 50;
        public float attack = 10f;
        public float attackSpeed = 0.8f;
        public int criticalChance = 10;
        
        [Header("SkillData")]
        public PlayerSkillDataSo playerSkillData;
    }
}