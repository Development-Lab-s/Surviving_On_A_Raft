using System;
using System.Collections.Generic;
using _00.Work.CheolYee._01.Codes.Items.PassiveItems;
using _00.Work.CheolYee._01.Codes.Items.PicupItem;
using _00.Work.lusalord._02.Script;
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
            if (attackItems[id].gameObject.activeSelf)
            {
                Debug.Log("업글");
                attackItems[id].LevelUp(id);                
            }
            else
            {
                Debug.Log("처음생성");
                attackItems[id].gameObject.SetActive(true);
            }
        }
        
        public void CreatePassiveItem(int id)
        {
            if (passiveItems[id].gameObject.activeSelf)
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

            if (index < 10)
            {
                PickupItem healthItem =  PoolManager.Instance.Pop(pickupItems[0].name) as PickupItem;
                if (healthItem != null) healthItem.Initialize(spawnPos);
            }
        }
    }
}