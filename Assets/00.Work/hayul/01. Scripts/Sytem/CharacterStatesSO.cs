using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterStats", menuName = "Game/Character Stats")]
public class CharacterStatsSO : ScriptableObject
{
    public Sprite characterImage; // 사진
    public string characterName; // 이름
    
    public string maxHp; // 체력
    public string attackPower; // 데미지
    public string attackSpeed; // 공격 속도
    public string moveSpeed; // 이동 속도
    public string abilityCooldown; // 스킬 쿨다운

    [TextArea] public string description; // 메모
}