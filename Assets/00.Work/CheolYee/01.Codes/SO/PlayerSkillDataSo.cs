using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.SO
{
    [CreateAssetMenu(fileName = "NewPlayerSkillData", menuName = "SO/Player/PlayerSkillData", order = 0)]
    public class PlayerSkillDataSo : ScriptableObject
    {
        [Header("Skill Data")]
        public Sprite skillIcon;
        public GameObject skillPrefab;
        public float skillDamage;
        public float skillCooldown;
    }
}