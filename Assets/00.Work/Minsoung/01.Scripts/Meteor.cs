using System.Collections;
using _00.Work.CheolYee._01.Codes.Enemys.Attacks;
using UnityEngine;
using UnityEngine.UI;

public class Meteor : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float colliderTime = 1.5f;
    [SerializeField] private LayerMask whatIsgroundCheck;
    [SerializeField] private DamageCaster damageCaster;
    [SerializeField] private float damage;
    [SerializeField] private CameraEventSO _eventSO;

    private ParticleSystem explosionParticle;

    private bool _onCrash = false;
    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider;
    CameraEventData eventData = new();

    private void Awake()
    {
        explosionParticle = GetComponentInChildren<ParticleSystem>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
        _collider.enabled = false;
    }
    private void Update()
    {
        if(!_onCrash)
            transform.position += (Vector3.down + Vector3.left) * (speed * Time.deltaTime);
    }

    private void Start()
    {
        StartCoroutine(ColloiderActive());
    }

    private IEnumerator ColloiderActive()
    {
        eventData.gain = 5;
        _eventSO.RaiseEvent(eventData);

        yield return new WaitForSeconds(colliderTime);
        _collider.enabled = true;
        eventData.gain = 10;
        _eventSO.RaiseEvent(eventData);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((whatIsgroundCheck & (1 << collision.gameObject.layer)) != 0)
        {
            Debug.Log("Crash 레이어 일치! 파괴!");
            StartCoroutine(MeteorParticle());
        }
        else
        {
            Debug.Log("레이어 불일치");
        }
    }
    private IEnumerator MeteorParticle()
    {
        _spriteRenderer.enabled = false;
        _collider.enabled = false;
        _onCrash = true;
        explosionParticle.Play();
        damageCaster.CastDamage(damage, 0);

        eventData.gain = 20;
        _eventSO.RaiseEvent(eventData);

        yield return new WaitForSeconds(0.5f);
        eventData.gain = 0;
        _eventSO.RaiseEvent(eventData);
        Destroy(gameObject);
    }




}
