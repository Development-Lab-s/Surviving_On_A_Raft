using _00.Work.CheolYee._01.Codes.Enemy;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Enemys.Attacks
{
    public class MeleeAttack
    {
        public void Attack(GroundMeleeAttackEnemy enemy)
        {
            if (enemy.damageCaster == null) return;

            bool hit = enemy.damageCaster.CastDamage(enemy.attackDamage, enemy.knockbackPower);
            Debug.Log(hit ? "근거리 공격 성공!!!!!!!!!" : "근거리 공격 실패......");
        }

        public void Attack(Players.Player player)
        {
            if (player.DamageCaster == null) return;

            bool hit = player.DamageCaster.CastDamage(player.damage, player.knockbackPower);
            Debug.Log(hit ? "근거리 공격 성공!!!!!!!!!" : "근거리 공격 실패......");
        }

        internal void Attack(AirMeleeDamage airMeleeDamage)
        {
            if (airMeleeDamage.damageCaster == null) return;

            bool hit = airMeleeDamage.damageCaster.CastDamage(airMeleeDamage.attackDamage, airMeleeDamage.knockbackPower);
            Debug.Log(hit ? "공중 몬스터 공격 성공!!!!!!!!!" : "공중 몬스터 공격 실패......");
        }
    }
}