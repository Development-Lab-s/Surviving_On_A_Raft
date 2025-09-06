using UnityEngine;

[System.Serializable]
public class PlayerItem
{
    public ExItemSO Template;  // SO ����
    public int Level = 1;      // �÷��̾� ���� ����

    public void Upgrade()
    {
        if (Level < 5)
            Level++;
    }
}
