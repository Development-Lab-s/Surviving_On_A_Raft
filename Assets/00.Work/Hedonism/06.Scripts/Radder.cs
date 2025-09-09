using System;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Radder : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap; // 타일맵 참조
    [SerializeField] private Transform Panel;
    [Range(0f, 1f)] [SerializeField] private float alpha = 1f; // 투명도 조절 (0 = 완전투명, 1 = 불투명)
    public bool isRand { get; set; } = false;
    private bool _isRadder = false;
    public bool nextKey { get; set; } = false;
    public bool isNext { get; set; } = false;

    [SerializeField] List<Sprite> resourceList = new List<Sprite>();
    // 예시: 랜덤으로 뽑힌 자원 (타입, 필요 개수)
    private List<(int type, int value)> needResources = new();

    [SerializeField] private UnityEngine.UI.Image imageUI1;
    [SerializeField] private UnityEngine.UI.Image imageUI2;
    [SerializeField] private UnityEngine.UI.Image imageUI3;

    void Start()
    {
        if (tilemap == null) return;

        nextKey = false;
        isNext = false;

        // 기존 색상 가져오기
        Color color = tilemap.color;

        // 알파만 변경
        color.a = 0.5f;

        // 적용
        tilemap.color = color;

        Panel.gameObject.SetActive(false);

        // 여기서는 예시로 0번 자원 1개 필요하게 세팅
        needResources.Add((0, 1));
        needResources.Add((1, 0));
        needResources.Add((2, 0));
    }

    void Update()
    {
        IsCanNext();
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isRadder = true;
            ShowUI();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isRadder = false;
            Panel.gameObject.SetActive(false);
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

        int fixedChoice = UnityEngine.Random.Range(0, 2); // 0 또는 1
        picked.Add(resourceList[fixedChoice]);

        List<int> candidates = new List<int>() { 2, 3, 4 };

        // 첫 번째 랜덤
        int randIndex1 = UnityEngine.Random.Range(0, candidates.Count);
        picked.Add(resourceList[candidates[randIndex1]]);
        candidates.RemoveAt(randIndex1); // 중복 방지

        // 두 번째 랜덤
        int randIndex2 = UnityEngine.Random.Range(0, candidates.Count);
        picked.Add(resourceList[candidates[randIndex2]]);

        //UI에 적용
        imageUI1.sprite = picked[0];
        imageUI2.sprite = picked[1];
        imageUI3.sprite = picked[2];
    }

    private void IsCanNext()
    {
        // F키 입력 처리
        if (isNext && Input.GetKeyDown(KeyCode.F))
        {
            isNext = false;
            if (CheckResources())
            {
                Debug.Log("조건 충족! 자원 차감 후 다음 단계로 진행 가능!");
                UseResources();
                SpawnManager.Instance.StartCycle();

                // 자원 차감 후 리소스 UI 표시
                if (!isRand)
                {
                    PickThreeResources();
                    isRand = true;
                }
            }
            else
            {
                Debug.Log("자원이 부족합니다!");
            }
        }
    }

    // 자원 충족 여부 확인
    private bool CheckResources()
    {
        foreach (var need in needResources)
        {
            int type = need.type;
            int value = need.value;

            if (CostManager.instance.Costs[type] < value)
            {
                return false; // 하나라도 부족하면 실패
            }
        }
        return true;
    }

    // 자원 소비
    private void UseResources()
    {
        foreach (var need in needResources)
        {
            int type = need.type;
            int value = need.value;
            CostManager.instance.MinusCost(type, value);
        }
    }

    private void ShowUI()
    {
        // nextKey가 true일 때만 Radder 패널 표시
        if (nextKey)
        {
            Panel.gameObject.SetActive(true);
        }
    }
}