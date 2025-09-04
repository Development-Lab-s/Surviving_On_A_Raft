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
    
        void Update()
        {
            if (Camera.main != null)
                transform.forward = Camera.main.transform.forward;
        }

        public void SetText(float damage, Transform spawntrm)
        {

            textMesh.text = damage.ToString("0.0");

            transform.position = spawntrm.position;

            // 위로 1만큼 이동 (1초 동안)
            transform.DOMoveY(spawntrm.position.y + 1f, 1f);

            // 1초 동안 서서히 사라지기
            textMesh.DOFade(0f, 1f).OnComplete(() =>
            {
                PoolManager.Instance.Push(this);
            });
        }

        public void ResetItem()
        {
            textMesh.text = "";
            textMesh.color = textColor;
        }
    }
}