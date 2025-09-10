using System.Collections.Generic;
using _00.Work.CheolYee._01.Codes.Managers;

namespace _00.Work.CheolYee._01.Codes.Core.Buffs
{
    public interface IBuffable
    {
        void ApplyBuff(StatType stat, float buff);
        void ResetBuff(StatType statType, float buff);
    }
}