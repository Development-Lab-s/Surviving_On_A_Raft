using _00.Work.CheolYee._01.Codes.Items.PassiveItems;

namespace _00.Work.CheolYee._01.Codes.Items.ChallengeItem
{
    public class ChallengeAttackSpeedPassiveItem : PassiveItem
    {
        public override void ApplyBuff()
        {
            Player.attackSpeedMulti -= PassiveItemSo.atkSpeedMulti;
            Player.damageMulti += PassiveItemSo.damageMulti;
        }

        public override void CancelBuff()
        {
            Player.attackSpeedMulti += PassiveItemSo.atkSpeedMulti;
            Player.damageMulti -= PassiveItemSo.damageMulti;
        }
    }
}