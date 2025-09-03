using System.Collections;
using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;

public class MeteorEvent : MonoBehaviour, IEvent
{
    [field: SerializeField] public GameEventType eventType { get; private set; }
    [SerializeField] private GameObject _meteor;
    [SerializeField] private Camera _mainCam;
    [SerializeField] private CameraEventSO _eventSO;
    [SerializeField] private int _num = 5;
    private float _rand;
    private Vector2 _posision;


    private void Start()
    {
        _mainCam = Camera.main;

        //CinemachineImpulseSource impulse;
        //impulse.ImpulseDefinition.CustomImpulseShape = 
    }


    [ContextMenu("메테오 ㄹㅊㄱ")]
    public void StartEvent()
    {
        StartCoroutine(CreateMeteor()); 
    }

    private IEnumerator CreateMeteor()
    {
        for (int i = 0; i < _num; i++)
        {
            _rand = Random.Range(1f, 3f);
            _posision = _mainCam.ViewportToWorldPoint(new Vector2(_rand, 3));
            yield return new WaitForSeconds(1f);
            Instantiate(_meteor);
            _meteor.transform.position = _posision;


        }
    }
}
