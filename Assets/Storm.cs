using System;
using System.Collections;
using _00.Work.CheolYee._01.Codes.Enemys.Attacks;
using UnityEngine;

public class Storm : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private DamageCaster damageCaster;
    [SerializeField] private float damage;
    [SerializeField] private CameraEventSO eventSO;
    [SerializeField] private float dotDamageTime = 0.2f;
    [SerializeField] private float lifeTime = 10f; 

    CameraEventData eventData = new();

    private readonly int _airingHash = Animator.StringToHash("Airing");
    private readonly int _endAirHash = Animator.StringToHash("EndAir");

    private bool _end = false;




    private Animator _animator;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Airing()
    {
        _animator.SetBool(_airingHash, true);
        StartCoroutine(AirLifeTime());
        StartCoroutine(DotDamage());
    }

    private IEnumerator AirLifeTime()
    {
        yield return new WaitForSeconds(lifeTime);
        EndAir();
    }

    public void EndAir()
    {
        _animator.SetBool(_endAirHash, true);
        eventData.gain = 0;
        eventSO.RaiseEvent(eventData);
        _end = true;
    }

    private IEnumerator DotDamage()
    {
        eventData.gain = 1;
        eventSO.RaiseEvent(eventData);
        while (true)
        {
            yield return new WaitForSeconds(dotDamageTime);
            damageCaster.CastDamage(damage, 0);
            if (_end) break;
        }
    }

    /*private IEnumerator StormRandomMove()
    {
        while (true)
        {
            _rb.linearVelocityX =
        }
    }*/

    private void StormDestroy()
    {
        Destroy(gameObject);
    }

}
