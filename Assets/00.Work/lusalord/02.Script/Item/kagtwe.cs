using System;
using UnityEngine;
using _00.Work.lusalord._02.Script.ItemType;

namespace _00.Work.lusalord._02.Script.Item
{
    public class kagtwe : ItemTypeProjectiile
    {
        protected override void SpawnProjectile(Vector3 spawnPosition)
        {
            base.SpawnProjectile(spawnPosition);
        }

        private void Start()
        {
            SpawnProjectile(transform.position);
        }
    }
}
