using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.InputSystem;

public class MiniMapStageUp : MonoBehaviour
{
    public Action FloorChanged;
    
    [SerializeField] private Image castleImage;
    [SerializeField] private int maxTowerFloor;
    private int _castleFloor;
    private int _currentFloorSliced;
    public int CurrentFloor { get; private set; }
    
    public static MiniMapStageUp Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _castleFloor = 450 / maxTowerFloor;
        _currentFloorSliced = (int)castleImage.rectTransform.anchoredPosition.y - _castleFloor;
        CurrentFloor = 1;
    }

    private void Update()
    {
        if (Keyboard.current.mKey.wasPressedThisFrame)
        {
            CastleViewUp();
        }
    }

    public void CastleViewUp()
    {
        //castleImage.rectTransform.position += new Vector3(0, _castleFloor, 0);
        Sequence sequence = DOTween.Sequence();
        sequence.Append(castleImage.rectTransform.DOScale(new Vector3(0.7f, 0.7f, 0), 0.3f).SetEase(Ease.InOutCubic));
        sequence.Append(castleImage.rectTransform.DOScale(new Vector3(1f, 1f, 0), 1f).SetEase(Ease.InSine));
        sequence.Join(castleImage.rectTransform.DOAnchorPos(new Vector2(0, _currentFloorSliced), 1f).SetEase(Ease.OutSine));
        _currentFloorSliced -= _castleFloor;
        CurrentFloor++;
        FloorChanged?.Invoke();
    }
}
