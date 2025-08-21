using UnityEngine;

[CreateAssetMenu(menuName = "SO/EnemyData", fileName = "EnemyData")]  // so만들기
public class EnemyData : ScriptableObject
{
    [Header("기본 스탯")]
    [SerializeField] private string EnemyName = "anyting";       // 적 이름.
    [Min(1)][SerializeField] private float maxHealth = 30f;     // 일단 값들은 전부 대충 넣은거니까 적들마다 바꾸면 됨. // 참고로 [Min(1)은 최솟값을 1이상으로 강제하는 일종의 안전장치임.
    public float moveSpeed = 2.5f; //적 이동속도. 만약 안움직이는 적이 있을지도 모르니 안전장치는 빼겠음.
    [Min(0)][SerializeField] private float attackPower = 5f;    //적 공격력.

    [Header("거리(근거리 기준]")]
    public float detectRadius = 5f; // 감지 반경
    public float attackRange = 1.2f; // 공격 사거리
    public float attackCooldown = 1.0f; // 공격 쿨타임. 적이 공격 후 일정 시간동안 아무것도 안하는 시스템.

    [Header("점프(장애물)")]
    [Min(1)] public float jumpForceY = 6f;   //점프력
    [Min(0)] public float obstacleCheckDist = 0.6f; // 전방 레이 거리
    [Min(0)] public float jumpCooldown = 0.5f; // 연속 점프 방지

    [Header("드랍 (아이템)")]
    [SerializeField] private GameObject[] dropItem;  //죽으면 떨어뜨리는 아이템
    public Vector2Int dropCountRange = new Vector2Int(1, 1); // 매번 랜덤 개수(최소~최대)

    public int RollDropCount()                    // 이건 짜피 예비로 한 메서드니까 무시해도 됨. 나중에 수정하던 
    {
        int min = Mathf.Max(0, dropCountRange.x);
        int max = Mathf.Max(min, dropCountRange.y);
        return Random.Range(min, max + 1); // [min, max] 범위에서 난수
    }
}
