using System;
using UnityEngine;

namespace _00.Work.lusalord._02.Script.ItemType
{
    public abstract class ItemTypeInstall : AttackItem
    {
        private InstallItemSO _installItemSo;

        protected virtual void Awake()
        {
            _installItemSo = (InstallItemSO)attackItemSo;
        }
    }
}
