using _00.Work.CheolYee._01.Codes.Items.PassiveItems;

namespace _00.Work.CheolYee._01.Codes.Items.ChallengeItem
{
    public class ChallengeDamagePassiveItem : PassiveItem
    {
        public override void ApplyBuff()
        {
            Player.damageMulti += PassiveItemSo.damageMulti;
            Player.attackSpeedMulti -= PassiveItemSo.atkSpeedMulti;
        }

        public override void CancelBuff()
        {
            Player.damageMulti -= PassiveItemSo.damageMulti;
            Player.attackSpeedMulti += PassiveItemSo.atkSpeedMulti;
        }
    }
}