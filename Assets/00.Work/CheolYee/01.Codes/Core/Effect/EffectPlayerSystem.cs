using System.Collections;
using _00.Work.Resource.Manager;
using _00.Work.Resource.SO;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Core.Effect
{
    public class EffectPlayerSystem : MonoBehaviour, IPoolable
    {
        [SerializeField] private string itemName;
        
        private ParticleSystem _particleSystem;
        private float _duration;
        private WaitForSeconds _waitForSeconds;
        public string ItemName => itemName;
        public GameObject GameObject => gameObject;

        private void Awake()
        {
            _particleSystem = GetComponent<ParticleSystem>();
            _duration = _particleSystem.main.duration;
            _waitForSeconds = new WaitForSeconds(_duration);
        }

        public void SetPosAndPlay(Vector3 pos)
        {
            transform.position = pos;
            _particleSystem.Play();
            StartCoroutine(DelayAndGoToPool());
        }

        private IEnumerator DelayAndGoToPool()
        {
            yield return _waitForSeconds;
            PoolManager.Instance.Push(this);
        }

        public void ResetItem()
        {
            _particleSystem.Stop(); //멈추기
            _particleSystem.Simulate(0); //0초로 되감기 (처음으로 가기)
        }
    }
}