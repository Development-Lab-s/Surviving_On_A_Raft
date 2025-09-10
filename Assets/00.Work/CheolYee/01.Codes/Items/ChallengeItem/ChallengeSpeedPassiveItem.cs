using _00.Work.CheolYee._01.Codes.Items.PassiveItems;

namespace _00.Work.CheolYee._01.Codes.Items.ChallengeItem
{
    public class ChallengeSpeedPassiveItem : PassiveItem
    {
        public override void ApplyBuff()
        {
            Player.MovementComponent.SpeedMultiplier += PassiveItemSo.speedMulti;
            Player.HealthComponent.HealthMulti -= PassiveItemSo.hpMulti;
        }

        public override void CancelBuff()
        {
            Player.MovementComponent.SpeedMultiplier -= PassiveItemSo.speedMulti;
            Player.HealthComponent.HealthMulti += PassiveItemSo.hpMulti;
        }
    }
}