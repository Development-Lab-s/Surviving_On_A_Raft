using System;
using System.Collections;
using _00.Work.CheolYee._01.Codes.Managers;
using _00.Work.CheolYee._01.Codes.Players;
using _00.Work.Resource.SO;
using UnityEngine;

public class Bubble : MonoBehaviour, IPoolable
{
    private ParticleSystem _particleSys;
    public string ItemName => "BubbleParticle";
    public GameObject GameObject => gameObject;
    
    private Player _player;

    private void Awake()
    {
        _particleSys = GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        _player = GameManager.Instance.playerTransform.GetComponent<Player>();
    }

    public void ResetItem()
    {
        _particleSys.Pause();
        StopAllCoroutines();
    }

    public void StartBubble()
    {
        StartCoroutine(BubbleBubble());
    }

    public IEnumerator BubbleBubble()
    {
        _particleSys.Play();
        yield return new WaitForSeconds(1f);
        transform.position = GameManager.Instance.playerTransform.position;
        _player.HealthComponent.TakeDamage(20, Vector2.zero, 0);
        yield return new WaitForSeconds(3f);
        StartCoroutine(BubbleBubble());
    }
}
