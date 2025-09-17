using _00.Work.CheolYee._01.Codes.Items.PassiveItems;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Items.AdditiveItem
{
    public class Healing : PassiveItem
    {
        public override void ApplyBuff()
        {
            Player.HaveHealing = true;
            Player.HealingMultiplier = PassiveItemSo.hpMulti;
        }

        public override void CancelBuff()
        {
            Player.HaveHealing = false;
            Player.HealingMultiplier = 0;
        }
    }
}