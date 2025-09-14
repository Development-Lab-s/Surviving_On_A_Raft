using _00.Work.CheolYee._01.Codes.Items.PassiveItems;

namespace _00.Work.CheolYee._01.Codes.Items.ChallengeItem
{
    public class ChallengeBloodSuckingPassiveItem : PassiveItem
    {
        public override void ApplyBuff()
        {
            Player.HaveBloodSuckingItem = true;
            Player.BloodSuckingHealMultiplier = PassiveItemSo.hpMulti;
        }

        public override void CancelBuff()
        {
            Player.HaveBloodSuckingItem = false;
        }
    }
}