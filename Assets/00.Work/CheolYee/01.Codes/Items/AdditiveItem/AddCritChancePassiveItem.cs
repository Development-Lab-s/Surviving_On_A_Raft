using _00.Work.CheolYee._01.Codes.Items.PassiveItems;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Items.AdditiveItem
{
    public class AddCritChancePassiveItem : PassiveItem
    {
        public override void ApplyBuff()
        {
            Player.critChanceMulti += PassiveItemSo.criticalChanceMulti;
        }

        public override void CancelBuff()
        {
            Player.critChanceMulti -= PassiveItemSo.criticalChanceMulti;
        }
    }
}