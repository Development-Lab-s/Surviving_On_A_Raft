using _00.Work.CheolYee._01.Codes.Projectiles;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Skills
{
    public class FisherSkill : SkillBase
    {
        [SerializeField] private Transform firePos;
        
        protected override void Activate()
        {
            NetProjectile net = Instantiate(playerSkillData.skillPrefab, 
                firePos.position, firePos.rotation).GetComponentInChildren<NetProjectile>();
            net.Initialize(firePos, playerSkillData);
        }
    }
}