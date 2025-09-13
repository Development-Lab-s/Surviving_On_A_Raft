using System;
using _00.Work.CheolYee._01.Codes.Players;
using _00.Work.Resource.Manager;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Items.PicupItem
{
    public abstract class PickupItem : MonoBehaviour
    {
        public virtual void Initialize(Transform spawnPoint)
        {
            transform.position = spawnPoint.position;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            Pickup(other);
        }

        protected abstract void Pickup(Collision2D other);
        
    }
}