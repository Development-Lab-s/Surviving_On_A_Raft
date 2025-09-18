using _00.Work.CheolYee._01.Codes.Players;
using _00.Work.Resource.Manager;
using _00.Work.Resource.SO;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Items.PicupItem
{
    public class HealPackPickupItem : PickupItem, IPoolable
    {
        [SerializeField] private float healPer = 0.1f;
        public string ItemName => gameObject.name;
        public GameObject GameObject => gameObject;
        protected override void Pickup(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out Player p))
            {
                SoundManager.Instance.PlaySfx("HEAL");
                p.HealthComponent.HealPer(healPer);
                PoolManager.Instance.Push(this);
            }
        }
        public void ResetItem()
        {
            
        }
    }
}