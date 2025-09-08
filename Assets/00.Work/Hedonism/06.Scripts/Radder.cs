using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Radder : MonoBehaviour
{
    [Header("Tilemap, Panels")]
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private GameObject Panel;              // 최종 패널
    [SerializeField] private GameObject ChoiseMapPanel;     // 선택 패널

    [Header("Choice Panel Images")]
    [SerializeField] private Image choiceImage1;
    [SerializeField] private Image choiceImage2;
    [SerializeField] private Image choiceImage3;

    [Range(0f, 1f)] [SerializeField] private float alpha = 1f;
    public bool isRand { get; set; } = false;
    private bool _isRadder = false;

    [SerializeField] private List<Sprite> resourceList = new();
    private List<(int type, int value)> needResources = new();

    private int currentSelectedIndex = 0;   // 방향키로 선택된 인덱스 (0,1,2)
    private bool isChoiceMode = false;      // 현재 ChoiseMapPanel이 열려 있는 상태인지 여부

    void Start()
    {
        if (tilemap != null)
        {
            Color color = tilemap.color;
            color.a = 0.5f;
            tilemap.color = color;
        }

        Panel.SetActive(false);
        ChoiseMapPanel.SetActive(false);

        needResources.Add((0, 1));
        needResources.Add((1, 0));
        needResources.Add((2, 0));
    }

    void Update()
    {
        if (isChoiceMode)
        {
            HandleChoiceInput();
        }
        else
        {
            IsCanNext();
        }
    }

    private void HandleChoiceInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            currentSelectedIndex = Mathf.Max(0, currentSelectedIndex - 1);

        if (Input.GetKeyDown(KeyCode.RightArrow))
            currentSelectedIndex = Mathf.Min(2, currentSelectedIndex + 1);

        HighlightChoice(currentSelectedIndex);

        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log($"선택된 자원 인덱스: {currentSelectedIndex}");
            ChoiseMapPanel.SetActive(false);
            isChoiceMode = false;

            // 최종 패널 열기
            Panel.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isRadder = true;
            if (!isRand)
            {
                PickThreeResources();
                ChoiseMapPanel.SetActive(true);
                isChoiceMode = true;
                currentSelectedIndex = 0;
                HighlightChoice(currentSelectedIndex);
                isRand = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isRadder = false;
            Panel.SetActive(false);
            ChoiseMapPanel.SetActive(false);
            isChoiceMode = false;
        }
    }

    public void PickThreeResources()
    {
        if (resourceList.Count < 5)
        {
            Debug.LogWarning("자원 리스트에 최소 5개 이상의 자원이 필요합니다!");
            return;
        }

        List<Sprite> picked = new List<Sprite>();

        int fixedChoice = UnityEngine.Random.Range(0, 2);
        picked.Add(resourceList[fixedChoice]);

        List<int> candidates = new List<int>() { 2, 3, 4 };

        int randIndex1 = UnityEngine.Random.Range(0, candidates.Count);
        picked.Add(resourceList[candidates[randIndex1]]);
        candidates.RemoveAt(randIndex1);

        int randIndex2 = UnityEngine.Random.Range(0, candidates.Count);
        picked.Add(resourceList[candidates[randIndex2]]);

        choiceImage1.sprite = picked[0];
        choiceImage2.sprite = picked[1];
        choiceImage3.sprite = picked[2];
    }

    private void HighlightChoice(int index)
    {
        choiceImage1.color = (index == 0) ? Color.yellow : Color.white;
        choiceImage2.color = (index == 1) ? Color.yellow : Color.white;
        choiceImage3.color = (index == 2) ? Color.yellow : Color.white;
    }

    private void IsCanNext()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (CheckResources() && _isRadder)
            {
                Debug.Log("조건 충족! 자원 차감 후 다음 단계로 진 가능!");
                UseResources();
                SpawnManager.Instance.StartCycle();
                _isRadder = false;
            }
            else if (!_isRadder)
            {
                Debug.Log("이 사다리가 아니여");
            }
            else
            {
                Debug.Log("자원이 부족합니다!");
            }
        }
    }

    private bool CheckResources()
    {
        foreach (var need in needResources)
        {
            int type = need.type;
            int value = need.value;

            if (CostManager.instance.Costs[type] < value)
                return false;
        }
        return true;
    }

    private void UseResources()
    {
        foreach (var need in needResources)
        {
            int type = need.type;
            int value = need.value;
            CostManager.instance.MinusCost(type, value);
        }
    }
}
