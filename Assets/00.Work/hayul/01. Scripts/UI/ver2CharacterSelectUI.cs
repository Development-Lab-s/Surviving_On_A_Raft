using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class CharacterData2
{
    public Sprite image;       // 캐릭터 이미지
    public string name;        // 이름
    public string ability;     // 능력
    public string skill;       // 스킬
    public string info;        // 정보
}

public class ver2CharacterSelectUI : MonoBehaviour
{
    public Image characImage;
    public TMP_Text nameText;
    public TMP_Text abilityText;
    public TMP_Text skillText;
    public TMP_Text infoText;

    public CharacterData[] characters;  
    private int _currentIndex = 0;       

    private void Start()
    {
        ShowCharacter(_currentIndex);
    }
    
    void ShowCharacter(int index)
    {
        CharacterData data = characters[index];
        characImage.sprite = data.image;
        nameText.text = data.name;
        abilityText.text = data.ability;
        skillText.text = data.skill;
        infoText.text = data.info;
    }
    
    public void NextCharacter() 
    {
        _currentIndex++;
        if (_currentIndex >= characters.Length)
            _currentIndex = 0;
        ShowCharacter(_currentIndex);
    }
    
    public void PreviousCharacter() 
    {
        _currentIndex--;
        if (_currentIndex < 0)
            _currentIndex = characters.Length - 1;
        ShowCharacter(_currentIndex);
    }
    
    public void SelectCharacter()
    {
        Debug.Log("선택된 캐릭터: " + characters[_currentIndex].name);
        
        PlayerPrefs.SetInt("SelectedCharacterIndex", _currentIndex);
        PlayerPrefs.Save();
    }
}