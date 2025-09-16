using System.Collections;
using _00.Work.Hedonism._06.Scripts.SO.Manager;
using _00.Work.Resource.Manager;
using _00.Work.Resource.SO;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace _00.Work.CheolYee._01.Codes.Enemys.Portals
{
    public class Portal : MonoBehaviour, IPoolable
    {
        [field:SerializeField] public PortalDataSo PortalData {get; private set;}

        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Light2D portalLight;
        [SerializeField] private string poolName;
        [SerializeField] private Transform spawnTrm;
        [SerializeField] private float startDelay = 5;
        
        private bool _closePortal;
        private bool _isLeft;
        
        public string ItemName => poolName;
        public GameObject GameObject => gameObject;

        public void Initialize(PortalDataSo portalData, bool left)
        {
            PortalData = portalData;
            spriteRenderer.color = PortalData.portalColor;
            portalLight.color = PortalData.portalColor;
            
            _isLeft = left;
            if (_isLeft)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            
            OpenPortal(spawnTrm);
        }

        private void OpenPortal(Transform portalTrm)
        {
            transform.position = portalTrm.position + Vector3.up;
            transform.localScale = Vector3.zero;
            
            Color transparentColor = PortalData.portalColor;
            transparentColor.a = 0;
            spriteRenderer.color = transparentColor;
                
            Sequence seq = DOTween.Sequence();
            seq.Append(transform.DOScale(1, 1f));
            seq.Join(spriteRenderer.DOFade(1, 1f));
            seq.AppendInterval(0.5f);
            seq.AppendCallback(() =>
            {
                
                StartCoroutine(SummonCoroutine());
            });
        }

        private IEnumerator SummonCoroutine()
        {
            yield return new WaitForSeconds(startDelay);
            while (_closePortal == false)
            {
                float spawnTime = PortalData.GetRandomSpawnTime();
                
                if (SpawnManager.Instance.CanSpawn())
                {
                    int randomListIndex = PortalData.GetRandomListIndex();
                    
                    Enemy enemy = PoolManager.Instance.Pop(PortalData.enemies[randomListIndex].poolName) as Enemy;
                    if (enemy != null)
                    {
                        if (PortalData.enemyData != null)
                        {
                            enemy.enemyData = PortalData.enemyData[randomListIndex];
                            enemy.Initialize();
                        }
                        enemy.transform.position = spawnTrm.position;
                        if (_isLeft) enemy.MovementComponent.SetMovement(PortalData.launchForce);
                        else enemy.MovementComponent.SetMovement(-PortalData.launchForce);
                        yield return new WaitForSeconds(spawnTime);
                    }
                }
                else
                {
                    yield return new WaitForSeconds(spawnTime);
                }
            }

            ClosePortal();
        }

        private void ClosePortal()
        {
            Sequence seq = DOTween.Sequence();
            seq.Append(transform.DOScale(0, 1.5f));
            seq.Join(spriteRenderer.DOFade(0, 1f));
            seq.OnComplete(() => PoolManager.Instance.Pop(poolName));
        }

        public void ResetItem()
        {
            StopAllCoroutines();
            _closePortal = false;
        }
    }
}