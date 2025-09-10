using System;
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
        _player = GameManager.Instance.playerTransform.gameObject.GetComponent<Player>();
    }

    public void ResetItem()
    {
        _particleSys.Pause();
    }

    public void Play()
    {
        _particleSys.Play();
    }

    private void Update()
    {
        transform.position = GameManager.Instance.playerTransform.position;
        _player.HealthComponent.CurrentHealth--;
    }
}
