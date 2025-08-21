using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class CharacterData
{
    public Sprite image;       // 캐릭터 이미지
    public string name;        // 이름
    public string ability;     // 능력
    public string skill;       // 스킬
    public string info;        // 정보
}

public class CharacterSelectUI : MonoBehaviour
{
    [Header("UI References")]
    public Image characImage;
    public TMP_Text nameText;
    public TMP_Text abilityText;
    public TMP_Text skillText;
    public TMP_Text infoText;

    [Header("Data")]
    public CharacterData[] characters;  // 캐릭터 데이터 배열
    private int currentIndex = 0;        // 현재 캐릭터 인덱스

    private void Start()
    {
        ShowCharacter(currentIndex);
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

    public void NextCharacter() // 오른쪽 버튼
    {
        currentIndex++;
        if (currentIndex >= characters.Length)
            currentIndex = 0;
        ShowCharacter(currentIndex);
    }

    public void PreviousCharacter() // 왼쪽 버튼(이건 없애도 ㄱㅊ을듯)
    {
        currentIndex--;
        if (currentIndex < 0)
            currentIndex = characters.Length - 1;
        ShowCharacter(currentIndex);
    }

    public void SelectCharacter() // 선택 버튼
    {
        // 현재 선택된 캐릭터 인덱스를 저장하는 코드임
        PlayerPrefs.SetInt("SelectedCharacter", currentIndex);
        PlayerPrefs.Save();

        Debug.Log($"캐릭터 선택됨: {characters[currentIndex].name}");
        // 여기서 시작 씬으로 넘어가는거 넣으면 댐
    }
}