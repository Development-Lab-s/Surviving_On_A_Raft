using _00.Work.CheolYee._01.Codes.Enemys;
using _00.Work.CheolYee._01.Codes.Enemys.Boss.BossSkillAttack;
using _00.Work.Resource.Manager;
using _00.Work.Resource.SO;
using UnityEngine;

public class BossLaserSkill : SkillState
{
    private readonly GameObject _laserPrefab;
    private readonly Transform _firePos;
    private readonly float _range;

    public BossLaserSkill(Enemy enemy, 
        string animBoolName, 
        float coolDown, 
        GameObject laserPrefab, 
        Transform firePos,
        float range) 
        : base(enemy, animBoolName, coolDown)
    {
        _laserPrefab = laserPrefab;
        _firePos = firePos;
        _range = range;
    }

    public override void OnAnimationCast()
    {
        Debug.Log("보스 레이저 발사!!!");

        string poolName = _laserPrefab.GetComponent<IPoolable>().ItemName;
        LaserProjectile laser = PoolManager.Instance.Pop(poolName) as LaserProjectile;

        if (laser)
        {
            Vector2 dir = Enemy.targetTrm.position - _firePos.position;
            laser.Initialize(_firePos.position, dir, Enemy.CurrentAttackDamage, 0, 2f); 
        }

        LastAttackTime = Time.time;
    }

    public override bool CanUse()
    {
        if (Time.time < LastAttackTime + CoolDown) return false;

        float dist = Vector3.Distance(Enemy.transform.position, Enemy.targetTrm.position);
        if (dist > _range) return false;

        return true;
    }
}