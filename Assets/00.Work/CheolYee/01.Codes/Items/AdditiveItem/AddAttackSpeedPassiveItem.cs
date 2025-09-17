using _00.Work.CheolYee._01.Codes.Items.PassiveItems;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Items.AdditiveItem
{
    public class AddAttackSpeedPassiveItem : PassiveItem
    {
        public override void ApplyBuff()
        {
            Player.attackSpeedMulti += PassiveItemSo.atkSpeedMulti;
        }

        public override void CancelBuff()
        {
            Player.attackSpeedMulti -= PassiveItemSo.atkSpeedMulti;
        }
    }
}