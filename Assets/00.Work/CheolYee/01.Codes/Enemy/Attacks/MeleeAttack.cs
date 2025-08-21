using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Enemy.Attacks
{
    public class MeleeAttack : IAttackBehaviour
    {
        public void Attack(Enemy enemy)
        {
            if (enemy.DamageCaster == null) return;
            
            bool hit = enemy.DamageCaster.CastDamage(enemy.attackDamage, enemy.knockbackPower);
            if (hit)
            {
                Debug.Log("근거리 공격 성공!!!!!!!!!");
            }
            else
            {
                Debug.Log("근거리 공격 실패......");
            }
        }
    }
}