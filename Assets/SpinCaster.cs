using System;
using _00.Work.CheolYee._01.Codes.Enemys.Attacks;
using UnityEngine;

public class SpinCaster : MonoBehaviour
{
    [SerializeField] private DamageCaster damageCaster;

    private void OnTriggerEnter2D(Collider2D other)
    {
        damageCaster.CastDamage(10, 5);
    }
}
