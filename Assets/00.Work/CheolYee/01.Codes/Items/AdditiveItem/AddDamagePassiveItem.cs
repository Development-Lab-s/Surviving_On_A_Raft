using _00.Work.CheolYee._01.Codes.Items.PassiveItems;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Items.AdditiveItem
{
    public class AddDamagePassiveItem : PassiveItem
    {
        public override void ApplyBuff()
        {
            Player.damageMulti += PassiveItemSo.damageMulti;
        }

        public override void CancelBuff()
        {
            Player.damageMulti -= PassiveItemSo.damageMulti;
        }
    }
}