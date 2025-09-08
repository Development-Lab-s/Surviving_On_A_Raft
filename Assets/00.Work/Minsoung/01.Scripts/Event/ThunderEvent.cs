using System.Collections;
using _00.Work.CheolYee._01.Codes.Managers;
using _00.Work.Resource.Manager;
using _00.Work.Resource.SO;
using DG.Tweening;
using NUnit.Framework.Constraints;
using UnityEngine;


public class ThunderEvent : MonoBehaviour, IEvent
{
    [field: SerializeField] public GameEventType eventType { get; private set; }

    [field:SerializeField] private GameObject[] thunder;
    [SerializeField] private CameraEventSO eventSO;
    [SerializeField] private int num = 5;
    private int _rand;
    private Vector2 _posision;



    [ContextMenu("알로알로 ㄹㅊㄱ")]
    public void StartEvent()
    {
        StartCoroutine(CreateThunder());
    }

    private IEnumerator CreateThunder()
    {
        for (int i = 0; i < num; i++)
        {
            yield return new WaitForSeconds(3f);
            Debug.Log(_posision);
            _rand = Random.Range(0, thunder.Length);
            IPoolable thunderPrefab = PoolManager.Instance.Pop(thunder[_rand].GetComponent<Thunder>().ItemName);
            thunderPrefab.GameObject.transform.position = 
                GameManager.Instance.playerTransform.position + new Vector3(0,7);

            thunderPrefab.GameObject.transform.localScale = new Vector3(0, 0, 0);
            thunderPrefab.GameObject.transform.DOScale(new Vector3(1, 1, 1), 0.5f).SetEase(Ease.OutQuart);

        }
    }
}
