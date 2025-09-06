using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Enemys.Boss.BossSkillAttack
{
    [CreateAssetMenu(fileName = "NewBossSkillData", menuName = "SO/Boss/Skill", order = 0)]
    public class BossSkillData : ScriptableObject
    {
        public string skillName;
        public float cooldown;
        public GameObject prefab;
        public float damage;
        public float preferredRange; // 선호 거리 (이 스킬은 이 거리에서 잘 맞는다)

        private float _cooldownTimer;

        public bool IsReady => _cooldownTimer <= 0f;

        public void TickCooldown(float deltaTime)
        {
            if (_cooldownTimer > 0f)
                _cooldownTimer -= deltaTime;
        }

        public void PutOnCooldown()
        {
            _cooldownTimer = cooldown;
        }

        public void Execute(BossEnemy boss, Transform target)
        {
            if (prefab != null)
            {
                GameObject go = GameObject.Instantiate(prefab, boss.transform.position, Quaternion.identity);
                // TODO: 타겟 방향 처리, 데미지 세팅 등
            }

            PutOnCooldown();
        }
    }
}