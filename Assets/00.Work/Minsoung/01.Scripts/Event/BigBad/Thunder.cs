using System.Collections;
using _00.Work.CheolYee._01.Codes.Enemys.Attacks;
using _00.Work.Resource.Manager;
using _00.Work.Resource.SO;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Thunder : MonoBehaviour, IPoolable
{
    [SerializeField] private DamageCaster damageCaster;
    [SerializeField] private float damage;
    [SerializeField] private CameraEventSO eventSO;
    [SerializeField] private float dotDamageTime = 0.2f;
    [SerializeField] private int dotDamageNum = 5;

    private readonly int _thunderHash = Animator.StringToHash("ThunderPop");

    CameraEventData eventData = new();
    private Animator _animator;

    [Header("Pooling")]
    [SerializeField] private string itemName;
    public string ItemName => itemName;

    public GameObject GameObject => gameObject;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void ThunderPop()
    {
        SoundManager.Instance.PlaySfx("Lightning");
        _animator.SetBool(_thunderHash, true);
        StartCoroutine(DotDamage());
        eventData.gain = 10;
        eventSO.RaiseEvent(eventData);

    }

    public void ThunderEnd()
    {
        eventData.gain = 0;
        eventSO.RaiseEvent(eventData);
        PoolManager.Instance.Push(this);
        StopCoroutine(DotDamage());
    }

    private IEnumerator DotDamage()
    {
        for (int i = 0; i < dotDamageNum; i++)
        {
            yield return new WaitForSeconds(dotDamageTime);
            damageCaster.CastDamage(damage, 0);
            Debug.Log("Thunder");
        }
    }

    public void ResetItem()
    {
        _animator.SetBool(_thunderHash, false);
    }
}
