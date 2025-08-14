using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.SO
{
    [CreateAssetMenu(fileName = "NewCharacterData", menuName = "SO/CharacterDataSo")]
    public class CharacterDataSo : ScriptableObject
    {
        [Header("CharacterData")]
        public float moveSpeed = 5;
        public float jumpForce = 50;
    }
}