using _00.Work.CheolYee._01.Codes.Items.PassiveItems;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Items.AdditiveItem
{
    public class AddHpPassiveItem : PassiveItem
    {
        public override void ApplyBuff()
        {
            Player.HealthComponent.HealthMulti += PassiveItemSo.hpMulti;
        }

        public override void CancelBuff()
        {
            Player.HealthComponent.HealthMulti -= PassiveItemSo.hpMulti;
        }
    }
}