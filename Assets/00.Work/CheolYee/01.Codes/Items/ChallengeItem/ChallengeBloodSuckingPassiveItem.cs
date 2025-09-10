using _00.Work.CheolYee._01.Codes.Agents.Healths;
using _00.Work.CheolYee._01.Codes.Items.PassiveItems;

namespace _00.Work.CheolYee._01.Codes.Items.ChallengeItem
{
    public class ChallengeBloodSuckingPassiveItem : PassiveItem
    {
        public override void ApplyBuff()
        {
        }

        public override void CancelBuff()
        {
            Player.MovementComponent.SpeedMultiplier -= PassiveItemSo.speedMulti;
            Player.HealthComponent.HealthMulti += PassiveItemSo.hpMulti;
        }
    }
}