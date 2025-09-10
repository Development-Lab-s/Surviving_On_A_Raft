using _00.Work.Resource.Manager;
using _00.Work.Resource.SO;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _00.Work.hayul._01._Scripts.Sytem
{
    public class DamageText : MonoBehaviour, IPoolable
    {
        [SerializeField] private TextMeshPro textMesh;
        [SerializeField] private Color textColor;
        public string ItemName => "DamageText";
        public GameObject GameObject => gameObject;
    
        private Sequence _seq;
        
        void Update()
        {
            if (Camera.main != null)
                transform.forward = Camera.main.transform.forward;
        }

        public void SetText(float damage, Transform spawntrm)
        {
            // 혹시 이전 시퀀스가 남아있으면 정리
            _seq?.Kill();

            textMesh.text = damage.ToString("0.0");
            transform.position = spawntrm.position;

            // 새로운 시퀀스 생성
            _seq = DOTween.Sequence();

            _seq.Join(transform.DOMoveY(spawntrm.position.y + 1f, 1f)) // 위로 이동
                .Join(textMesh.DOFade(0f, 1f)) // 동시에 페이드 아웃
                .OnComplete(() =>
                {
                    PoolManager.Instance.Push(this);
                });
        }

        public void ResetItem()
        {
            // 시퀀스도 정리
            _seq?.Kill();
            _seq = null;

            textMesh.text = "";
            textMesh.color = textColor;
        }
    }
}