using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Globalization;
using _00.Work.CheolYee._01.Codes.Managers;
using _00.Work.CheolYee._01.Codes.SO;
using _00.Work.Resource.Manager;
using TMPro;
using UnityEngine.SceneManagement;

public class CharacterSelectUI : MonoBehaviour
{
    [System.Serializable]
    public class CharacterSlot
    {
        public RectTransform rect;
        public CharacterDataSo data;
    }

    public List<CharacterSlot> slots;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI moveSpeedText;
    public TextMeshProUGUI attackPowerText;
    public TextMeshProUGUI attackSpeedText;

    private int _currentIndex = 0;
    private CharacterDataSo _selectedCharacter;
    
    public Vector3 _centerPos = new Vector3(0, 0, 0);
    public Vector3 _leftPos = new Vector3(-250, 0, 0);
    public Vector3 _rightPos = new Vector3(250, 0, 0);
    public Vector3 _hiddenPos = new Vector3(500, 0, 0);

    public Vector3 _bigScale = Vector3.one * 0.005f;
    public Vector3 _smallScale = Vector3.one * 0.002f;
    
    private void Start()
    {
        InitUI();
    }

    public void OnClickNext()
    {
        _currentIndex = (_currentIndex + 1) % slots.Count;
        InitUI();
    }

    public void OnClickPrev()
    {
        _currentIndex = (_currentIndex - 1 + slots.Count) % slots.Count;
        InitUI();
    }

    public void BackToTitle()
    {
        FadeManager.Instance.FadeToScene(0);
    }
    
    public void OnClickSelect()
    {
        _selectedCharacter = slots[_currentIndex].data;
        GameSelectManager.Instance.currentCharacter = _selectedCharacter;
        RectTransform rect = slots[_currentIndex].rect;
        rect.DOScale(_bigScale * 1.2f, 0.2f).OnComplete(() =>
        {
            rect.DOScale(_bigScale, 0.2f).OnComplete(() =>
            {
                FadeManager.Instance.FadeToScene(2);
            });
        });
    }
    
    private void InitUI()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            RectTransform rect = slots[i].rect;
            int leftIndex = (_currentIndex - 1 + slots.Count) % slots.Count;
            int rightIndex = (_currentIndex + 1) % slots.Count;

            if (i == _currentIndex)
            {
                rect.DOScale(_bigScale, 0.5f);
                rect.DOAnchorPos(_centerPos, 0.5f);
            }
            else if (i == leftIndex)
            {
                rect.DOScale(_smallScale, 0.5f);
                rect.DOAnchorPos(_leftPos, 0.5f);
            }
            else if (i == rightIndex)
            {
                rect.DOScale(_smallScale, 0.5f);
                rect.DOAnchorPos(_rightPos, 0.5f);
            }
            else
            {
                rect.localPosition = _hiddenPos;
            }
        }

        CharacterDataSo data = slots[_currentIndex].data;
        nameText.text = data.characterName;
        attackSpeedText.text = $"공격 속도 :{data.attackSpeed.ToString(CultureInfo.InvariantCulture)}";
        hpText.text = $"체력 : {data.health.ToString(CultureInfo.InvariantCulture)}";
        moveSpeedText.text = $"이동 속도 : {data.moveSpeed.ToString(CultureInfo.InvariantCulture)}";
        attackPowerText.text = $"공격력 : {data.attack.ToString(CultureInfo.InvariantCulture)}";
    }
}