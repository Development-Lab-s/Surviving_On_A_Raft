using System.Collections;
using _00.Work.CheolYee._01.Codes.Enemys.Attacks;
using _00.Work.Resource.Manager;
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

    private float _stormDir = 1f;
    private float _rand1 = 0;

    private Animator _animator;
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        _animator.speed = 0f;
        SoundManager.Instance.PlaySfx("STORM");
        StartCoroutine(StormSprite());
    }

    private IEnumerator StormSprite()
    {
        _sr.enabled = false;
        yield return new WaitForSeconds(0.2f);
        _sr.enabled = true;
        _animator.speed = 1f;
    }

    private void Airing()
    {
        _animator.SetBool(_airingHash, true);
        StartCoroutine(AirLifeTime());
        StartCoroutine(DotDamage());
        StartCoroutine(StormRandomMove());
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

    private void FixedUpdate()
    {
        _rb.linearVelocity = Vector2.right * (speed * _stormDir);
    }

    private IEnumerator StormRandomMove()
    {
        _rand1 = Random.Range(0.1f,1.5f);
        _stormDir *= -1;
        yield return new WaitForSeconds(_rand1);
        if(!_end)
            StartCoroutine(StormRandomMove());
    }

    private void StormDestroy()
    {
        Destroy(gameObject);
    }

}
