using UnityEngine;

[CreateAssetMenu(menuName = "SO/EnemyData", fileName = "EnemyData")]  // so�����
public class EnemyData : ScriptableObject
{
    [Header("�⺻ ����")]
    [SerializeField] private string EnemyName = "anyting";       // �� �̸�.
    [Min(1)][SerializeField] private float maxHealth = 30f;     // �ϴ� ������ ���� ���� �����Ŵϱ� ���鸶�� �ٲٸ� ��. // ����� [Min(1)�� �ּڰ��� 1�̻����� �����ϴ� ������ ������ġ��.
    public float moveSpeed = 2.5f; //�� �̵��ӵ�. ���� �ȿ����̴� ���� �������� �𸣴� ������ġ�� ������.
    [Min(0)][SerializeField] private float attackPower = 5f;    //�� ���ݷ�.

    [Header("�Ÿ�(�ٰŸ� ����]")]
    public float detectRadius = 5f; // ���� �ݰ�
    public float attackRange = 1.2f; // ���� ��Ÿ�
    public float attackCooldown = 1.0f; // ���� ��Ÿ��. ���� ���� �� ���� �ð����� �ƹ��͵� ���ϴ� �ý���.

    [Header("����(��ֹ�)")]
    [Min(1)] public float jumpForceY = 6f;   //������
    [Min(0)] public float obstacleCheckDist = 0.6f; // ���� ���� �Ÿ�
    [Min(0)] public float jumpCooldown = 0.5f; // ���� ���� ����

    [Header("��� (������)")]
    [SerializeField] private GameObject[] dropItem;  //������ ����߸��� ������
    public Vector2Int dropCountRange = new Vector2Int(1, 1); // �Ź� ���� ����(�ּ�~�ִ�)

    public int RollDropCount()                    // �̰� ¥�� ����� �� �޼���ϱ� �����ص� ��. ���߿� �����ϴ� 
    {
        int min = Mathf.Max(0, dropCountRange.x);
        int max = Mathf.Max(min, dropCountRange.y);
        return Random.Range(min, max + 1); // [min, max] �������� ����
    }
}
