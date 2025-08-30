using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

public class CharacterSelectUI : MonoBehaviour
{
    [System.Serializable]
    public class CharacterSlot
    {
        public RectTransform rect;
        public CharacterStatsSO data;
    }

    public List<CharacterSlot> slots;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI attackCoolDownText;
    public TextMeshProUGUI moveSpeedText;
    public TextMeshProUGUI attackPowerText;
    public TextMeshProUGUI attackSpeedText;

    private int _currentIndex = 0;
    private CharacterStatsSO _selectedCharacter;
    
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
        SceneManager.LoadScene(0);
    }
    
    public void OnClickSelect()
    {
        _selectedCharacter = slots[_currentIndex].data;
        Debug.Log($"선택된 캐릭터: {_selectedCharacter.characterName}");
        RectTransform rect = slots[_currentIndex].rect;
        rect.DOScale(_bigScale * 1.2f, 0.2f).OnComplete(() =>
        {
            rect.DOScale(_bigScale, 0.2f);
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

        CharacterStatsSO data = slots[_currentIndex].data;
        nameText.text = data.characterName;
        attackSpeedText.text = data.attackSpeed;
        hpText.text = data.maxHp;
        moveSpeedText.text = data.moveSpeed;
        attackCoolDownText.text = data.abilityCooldown;
        attackPowerText.text = data.attackPower;
    }
}