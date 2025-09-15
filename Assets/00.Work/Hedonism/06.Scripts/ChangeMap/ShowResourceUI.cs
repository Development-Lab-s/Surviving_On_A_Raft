using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class ShowResourceUI : MonoBehaviour 
{
    [Header("References")]
    [SerializeField] private Tilemap tilemap; // 타일맵 참조
    [SerializeField] private Transform Panel;
    [SerializeField] private List<Sprite> resourceList = new List<Sprite>();
    [SerializeField] private Image imageUI1;
    [SerializeField] private Image imageUI2;
    [SerializeField] private Image imageUI3;

    // [SerializeField] private Text textUI1;
    // [SerializeField] private Text textUI2;
    // [SerializeField] private Text textUI3;
    
    // Properties
    public bool isRand { get; set; } = false;
    
    // Private variables
    private List<(int type, int value)> needResources = new List<(int type, int value)>();

    void Start() 
    {
        InitializeTilemap();
        InitializeUI();
        InitializeResources();
    }

    void Update() 
    {
        IsCanNext();
    }

    void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            if (!isRand) 
            {
                PickThreeResources();
                isRand = true;
            }
        }
    }

    void OnTriggerStay2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            IsCanNext();
            ShowUI();
        }
    }

    void OnTriggerExit2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            Panel.gameObject.SetActive(false);
        }
    }

    private void InitializeTilemap()
    {
        if (tilemap == null) return;
        
        // 기존 색상 가져오기
        Color color = tilemap.color;
        // 알파만 변경
        color.a = 0.5f;
        // 적용
        tilemap.color = color;
    }

    private void InitializeUI()
    {
        Panel.gameObject.SetActive(false);
    }

    private void InitializeResources()
    {
        //0번 자원 3개, 2번 자원 5개 필요하게 세팅
        // 실제 게임에서는 랜덤 뽑기 로직을 따로 호출해주면 됨
        needResources.Add((0, 1));
        needResources.Add((1, 0));
        needResources.Add((2, 0));
    }

    public void PickThreeResources() 
    {
        const int REQUIRED_RESOURCE_COUNT = 5;
        
        if (resourceList.Count < REQUIRED_RESOURCE_COUNT) 
        {
            Debug.LogWarning($"자원 리스트에 최소 {REQUIRED_RESOURCE_COUNT}개 이상의 자원이 필요합니다!");
            return;
        }

        List<Sprite> pickedResources = new List<Sprite>();
        
        // 0 또는 1 중에서 고정 선택
        int fixedChoice = Random.Range(0, 2);
        pickedResources.Add(resourceList[fixedChoice]);

        // 2, 3, 4 중에서 2개 랜덤 선택
        List<int> candidates = new List<int> { 2, 3, 4 };
        
        // 첫 번째 랜덤 선택
        int randomIndex1 = Random.Range(0, candidates.Count);
        pickedResources.Add(resourceList[candidates[randomIndex1]]);
        candidates.RemoveAt(randomIndex1); // 중복 방지
        
        // 두 번째 랜덤 선택
        int randomIndex2 = Random.Range(0, candidates.Count);
        pickedResources.Add(resourceList[candidates[randomIndex2]]);

        // UI에 적용
        ApplySpritesToUI(pickedResources);
    }

    private void ApplySpritesToUI(List<Sprite> sprites)
    {
        if (sprites.Count >= 3)
        {
            imageUI1.sprite = sprites[0];
            imageUI2.sprite = sprites[1];
            imageUI3.sprite = sprites[2];
        }
    }

    private void IsCanNext()
    {
        // 이 메서드의 구현이 누락되어 있습니다.
        // 필요한 로직을 여기에 추가하세요.
    }

    private void ShowUI()
    {
        // 이 메서드의 구현이 누락되어 있습니다.
        // UI 표시 로직을 여기에 추가하세요.
    }
}