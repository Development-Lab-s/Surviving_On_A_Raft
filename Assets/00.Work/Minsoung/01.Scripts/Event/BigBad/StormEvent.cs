using System.Collections;
using _00.Work.CheolYee._01.Codes.Managers;
using _00.Work.Resource.Manager;
using DG.Tweening;
using UnityEngine;

public class StormEvent : MonoBehaviour, IEvent
{
    [field: SerializeField] public GameEventType eventType { get; private set; }
    [field: SerializeField] public string eventEffectText { get; private set; }

    [SerializeField] private GameObject stormPre;
    [SerializeField] private CameraEventSO eventSO;
    [SerializeField] private int num = 5;
    private float _rand;
    private Vector2 _posision;

    private void Start()
    {
        
    }

    [ContextMenu("허리지팡이")]
    public void StartEvent()
    {
        StartCoroutine(CreateThunder());
        EventTimer.Instance.StartTimer(10f);
    }

    private IEnumerator CreateThunder()
    {
        for (float i = 0; i < num; i++)
        {
            Debug.Log("허리게인");
            yield return new WaitForSeconds(2f);
            _rand = Random.Range(-10f + i *20f, i * 10f);
            GameObject stormPrefab = Instantiate(stormPre);
            stormPrefab.transform.position =
            GameManager.Instance.playerTransform.position + new Vector3(_rand, 9);
            
        }
    }
    public void StartEventEffectText()
    {
        EventUIManager.Instance.SetEventTextEffect(eventEffectText);
    }

}
