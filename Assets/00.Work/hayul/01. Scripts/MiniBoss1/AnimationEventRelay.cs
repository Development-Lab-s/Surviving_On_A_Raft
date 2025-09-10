using _00.Work.CheolYee._01.Codes.Enemys.Boss;
using UnityEngine;

public class AnimationEventRelay : MonoBehaviour
{
    [SerializeField] private MiniBossEnemy boss; // 본체 참조

    public void Attack()
    {
        boss.Attack();
    }

    public void AnimationEndTrigger()
    {
        boss.AnimationEndTrigger();
    }
}