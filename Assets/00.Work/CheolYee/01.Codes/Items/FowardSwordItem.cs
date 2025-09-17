using _00.Work.lusalord._02.Script.Item;
using _00.Work.lusalord._02.Script.ItemType;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Items
{
    public class FowardSwordItem : ItemTypeForward
    {
        [SerializeField] private MousePosCaster mouseCaster;

        public void OnLock()
        {
            // 공격 시작할 때 방향 고정
            mouseCaster.LockDirection();
        }

        // 애니메이션 끝에서 이벤트 호출
        public void OnAttackEnd()
        {
            mouseCaster.UnlockDirection();
        }
    }
}