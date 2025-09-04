namespace _00.Work.CheolYee._01.Codes.Enemys.Attacks
{
    public class MeleeAttack
    {
        public void Attack(GroundMeleeAttackEnemy enemy)
        {
            if (enemy.damageCaster == null) return;

            enemy.damageCaster.CastDamage(enemy.CurrentAttackDamage, 0);
        }
        
        public void Attack(BossEnemy enemy)
        {
            if (enemy.damageCaster == null) return;

            enemy.damageCaster.CastDamage(enemy.CurrentAttackDamage, 
                enemy.knockbackPower);
        }

        internal void Attack(AirMeleeAttackEnemy airMeleeAttackEnemy)
        {
            if (airMeleeAttackEnemy.damageCaster == null) return;

            airMeleeAttackEnemy.damageCaster.CastDamage
                (airMeleeAttackEnemy.CurrentAttackDamage, 0);
        }
    }
}