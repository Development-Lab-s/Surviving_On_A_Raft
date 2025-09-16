using System;
using System.Collections.Generic;
using _00.Work.CheolYee._01.Codes.Items.PassiveItems;
using _00.Work.CheolYee._01.Codes.Items.PicupItem;
using _00.Work.lusalord._02.Script;
using _00.Work.lusalord._02.Script.SO.AttackItem;
using _00.Work.Resource.Manager;
using _00.Work.Resource.SO;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _00.Work.CheolYee._01.Codes.Managers
{
    public class ItemManager : MonoSingleton<ItemManager>
    {
        [Header("AttackItems")] 
        public List<AttackItem> attackItems;
        
        [Header("PassiveItems")] 
        public List<PassiveItem> passiveItems;
        
        [Header("PickupItems")]
        public List<PickupItem> pickupItems;

        public void CreateAttackItem(int id)
        {
            if (attackItems[id].gameObject.activeSelf || attackItems[id].attackItemSo.level > 1)
            {
                attackItems[id].gameObject.SetActive(true);
                attackItems[id].LevelUp(id);
            }
            else
            {
                attackItems[id].gameObject.SetActive(true);
            }
        }
        
        public void DeleteAttackItem(int id) => attackItems[id].gameObject.SetActive(false);

        public void DeletePassiveItem(int id)
        {
            passiveItems[id].CancelBuff();
            passiveItems[id].gameObject.SetActive(false);
        }

        public AttackItemSo GetAttackItem(int id) => attackItems[id].attackItemSo;
        public PassiveItemSo GetPassiveItem(int id) => passiveItems[id].PassiveItemSo;

        public void CreatePassiveItem(int id)
        {
            if (passiveItems[id].gameObject.activeSelf || passiveItems[id].PassiveItemSo.level > 1)
            {
                passiveItems[id].CancelBuff();
                passiveItems[id].PassiveItemLevelUp(id);
                passiveItems[id].ApplyBuff();
            }
            else
            {
                passiveItems[id].gameObject.SetActive(true);
                passiveItems[id].ApplyBuff();
            }
        }


        public void CreateRandomPickupItem(Transform spawnPos)
        {
            int index = UnityEngine.Random.Range(0, 100);

            if (index < 2)
            {
                PickupItem healthItem =  PoolManager.Instance.Pop(pickupItems[0].name) as PickupItem;
                if (healthItem != null) healthItem.Initialize(spawnPos);
            }
        }
    }
}