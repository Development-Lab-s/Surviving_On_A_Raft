using System.Collections;
using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;

public class MeteorEvent : MonoBehaviour, IEvent
{
    [field: SerializeField] public GameEventType eventType { get; private set; }
    [SerializeField] private GameObject meteor;
    [SerializeField] private Camera mainCam;
    [SerializeField] private CameraEventSO eventSO;
    [SerializeField] private int num = 5;
    private float _rand;
    private Vector2 _posision;


    private void Start()
    {
        mainCam = Camera.main;

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
        for (int i = 0; i < num; i++)
        {
            _rand = Random.Range(1f, 3f);
            _posision = mainCam.ViewportToWorldPoint(new Vector2(_rand, 3));
            yield return new WaitForSeconds(1f);
            Instantiate(meteor);
            meteor.transform.position = _posision;


        }
    }
}
