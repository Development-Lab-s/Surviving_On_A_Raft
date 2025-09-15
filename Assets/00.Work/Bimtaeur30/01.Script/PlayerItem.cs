using UnityEngine;

[System.Serializable]
public class PlayerItem
{
    public ExItemSO Template;  // SO 참조
    public int Level = 1;      // 플레이어 전용 레벨

    public void Upgrade()
    {
        if (Level < 5)
        {
            Template.Upgrade();
        }
    }
}
