using _00.Work.CheolYee._01.Codes.Items.PassiveItems;

namespace _00.Work.CheolYee._01.Codes.Items.ChallengeItem
{
    public class CritChanceChallengeItem : PassiveItem
    {
        public override void ApplyBuff()
        {
            Player.critChanceMulti += PassiveItemSo.criticalChanceMulti;
            Player.MovementComponent.SpeedMultiplier += PassiveItemSo.speedMulti;
            Player.attackSpeedMulti += PassiveItemSo.atkSpeedMulti;
        }

        public override void CancelBuff()
        {
            Player.critChanceMulti -= PassiveItemSo.criticalChanceMulti;
            Player.MovementComponent.SpeedMultiplier -= PassiveItemSo.speedMulti;
            Player.attackSpeedMulti -= PassiveItemSo.atkSpeedMulti;
        }
    }
}