using UnityEngine;

public class EnemyAnimEvents : MonoBehaviour
{
    private AirEnemy _enemy;
    private AirMeleeDamage _airMelee;

    void Awake()
    {
        // 부모 쪽(루트)에서 컨트롤/공격 로직을 가져오도록 캐시
        _enemy = GetComponentInParent<AirEnemy>();
        _airMelee = GetComponentInParent<AirMeleeDamage>();
    }

    // 애니메이션 중간 프레임에서 호출될 공격 이벤트
    public void Attack()
    {
        // AirMeleeDamage에 구현된 Attack() 실행 → DamageCaster로 실제 데미지
        if (_airMelee != null) _airMelee.Attack();
    }

    // 애니메이션 끝 프레임에서 호출(상태 복귀/정리용)
    public void AnimationEndTrigger()
    {
        // AirEnemy 쪽에 이미 구현된 트리거로 상태머신에 “끝남” 신호 전달
        if (_enemy != null) _enemy.AnimationEndTrigger();
    }
}
