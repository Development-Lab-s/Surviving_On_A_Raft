using System;
using System.Security.Cryptography;
using _00.Work.CheolYee._01.Codes.Enemys.Attacks;
using _00.Work.CheolYee._01.Codes.Managers;
using _00.Work.CheolYee._01.Codes.Players;
using _00.Work.lusalord._02.Script;
using _00.Work.lusalord._02.Script.SO.AttackItem.ItemType;
using UnityEngine;
using DG.Tweening;

public class SpinCaster : MonoBehaviour
{
    [SerializeField] private DamageCaster damageCaster;
    [SerializeField] private float rotationSpeed;

    private SpinItemSo _spinItemSo;
    private Player _player;
    
    private void Start()
    {
        _spinItemSo = GetComponentInParent<AttackItem>().attackItemSo as SpinItemSo;
        _player = GameManager.Instance.playerTransform.GetComponent<Player>();
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        float weaponDamage = _player.CurrentDamage * _spinItemSo.atkRate;
        damageCaster.CastDamage(_spinItemSo.damage + weaponDamage, _spinItemSo.knockbackPower);
    }
}
