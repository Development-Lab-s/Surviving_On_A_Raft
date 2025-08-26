using System;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Projectiles
{
    public class FirePos : MonoBehaviour
    {
        [SerializeField] private Enemy.Enemy enemy;
        private void Update()
        {
            Vector3 target = enemy.targetTrm.position;
            Vector2 direction = transform.InverseTransformPoint(target);
            
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}