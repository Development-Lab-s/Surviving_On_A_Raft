using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Enemys.Boss.BossSkillAttack
{
    public enum SkillConditionType
    {
        Always,      //무조건 사용 가능
        InRange,     //일정 거리 이내
        OutOfRange,  //일정 거리 밖
        HpBelow,     //체력이 특정 값 이하
        TimePassed   //보스가 생성된 지 특정 시간 이상
    }

    [System.Serializable]
    public class SkillCondition
    {
        public SkillConditionType type;
        public float value;

        public bool IsMet(BossEnemy boss, Transform target, float elapsedTime)
        {
            float dist = Vector2.Distance(boss.transform.position, target.position);

            switch (type)
            {
                case SkillConditionType.Always:
                    return true;
                case SkillConditionType.InRange:
                    return dist <= value;
                case SkillConditionType.OutOfRange:
                    return dist > value;
                case SkillConditionType.HpBelow:
                    return boss.HealthComponent.CurrentHealth <= value;
                case SkillConditionType.TimePassed:
                    return elapsedTime >= value;
            }

            return false;
        }
    }
}