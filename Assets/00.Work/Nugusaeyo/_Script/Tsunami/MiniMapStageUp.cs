using System;
using _00.Work.CheolYee._01.Codes.Managers;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.InputSystem;

public class MiniMapStageUp : MonoBehaviour
{
    public Action FloorChanged;
    
    [SerializeField] private Image castleImage;
    public int maxTowerFloor;
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
        CurrentFloor = GameManager.Instance.currentLevel;
    }

    public void CastleViewUp()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(castleImage.rectTransform.DOScale(new Vector3(0.7f, 0.7f, 0), 0.3f).SetEase(Ease.InOutCubic));
        sequence.Append(castleImage.rectTransform.DOScale(new Vector3(1f, 1f, 0), 1f).SetEase(Ease.InSine));
        sequence.Join(castleImage.rectTransform.DOAnchorPos(new Vector2(0, _currentFloorSliced), 1f).SetEase(Ease.OutSine));
        _currentFloorSliced -= _castleFloor;
        CurrentFloor++;
        FloorChanged?.Invoke();
    }

    public void SetCurrentLevel(int level)
    {
        CurrentFloor = level;
    }
}
