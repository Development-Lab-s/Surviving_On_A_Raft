using System.Collections.Generic;
using System.Linq;
using _00.Work.CheolYee._01.Codes.Enemys.Attacks;
using _00.Work.CheolYee._01.Codes.Enemys.Boss.BossSkillAttack;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Enemys.Boss
{
    public class BossEnemy : GroundEnemy
    {
        [Header("Boss Skills")] [SerializeField]
        private List<BossSkillData> skills = new List<BossSkillData>();

        private float _elapsedTime;

        private void Update()
        {
            StateMachine.CurrentState.Update();

            if (!isDead)
            {
                _elapsedTime += Time.deltaTime;
                foreach (var skill in skills)
                    skill.TickCooldown(Time.deltaTime);
            }
        }

        public void TryUseSkill()
        {
            if (targetTrm == null) return;

            float dist = Vector2.Distance(transform.position, targetTrm.position);

            // 공격 반경 안이 아니면 시도하지 않음
            if (dist > attackRadius)
                return;

            // 1. 거리 순으로 정렬
            var ordered = skills.OrderBy(s => Mathf.Abs(dist - s.preferredRange));

            // 2. 사용 가능한(쿨타임 아님) 스킬 찾기
            foreach (var skill in ordered)
            {
                if (skill.IsReady)
                {
                    skill.Execute(this, targetTrm);
                    return;
                }
            }

            // 3. 모든 스킬이 쿨타임이면 아무 것도 안 함
        }
    }
}