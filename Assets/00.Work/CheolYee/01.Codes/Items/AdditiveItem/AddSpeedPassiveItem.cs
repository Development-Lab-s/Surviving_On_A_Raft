using _00.Work.CheolYee._01.Codes.Items.PassiveItems;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Items.AdditiveItem
{
    public class AddSpeedPassiveItem : PassiveItem
    {
        public override void ApplyBuff()
        {
            Player.MovementComponent.SpeedMultiplier += PassiveItemSo.speedMulti;
        }

        public override void CancelBuff()
        {
            Player.MovementComponent.SpeedMultiplier -= PassiveItemSo.speedMulti;
        }
    }
}