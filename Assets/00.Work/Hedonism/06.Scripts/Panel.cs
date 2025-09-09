using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    private Radder radder;
    [SerializeField]private Transform mapPanel;
    [SerializeField] List<Sprite> mapList = new List<Sprite>();
    // 예시: 랜덤으로 뽑힌 자원 (타입, 필요 개수)
    [SerializeField] private UnityEngine.UI.Image imageUI1;
    [SerializeField] private UnityEngine.UI.Image imageUI2;
    [SerializeField] private UnityEngine.UI.Image imageUI3;

    void Awake()
    {
        radder = GetComponentInParent<Radder>();
        mapPanel.gameObject.SetActive(false);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!radder.nextKey)
            {
                radder.nextKey = true;
                mapPanel.gameObject.SetActive(false);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ViewMap();
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(!radder.nextKey)
                mapPanel.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            mapPanel.gameObject.SetActive(false);
        }
    }

    private void ViewMap()
    {
        mapPanel.gameObject.SetActive(true);
        List<Sprite> picked = new List<Sprite>();

        int fixedChoice = UnityEngine.Random.Range(0, 2); // 0 또는 1
        picked.Add(mapList[fixedChoice]);

        List<int> candidates = new List<int>() { 2, 3, 4 };

        // 첫 번째 랜덤
        int randIndex1 = UnityEngine.Random.Range(0, candidates.Count);
        picked.Add(mapList[candidates[randIndex1]]);
        candidates.RemoveAt(randIndex1); // 중복 방지

        // 두 번째 랜덤
        int randIndex2 = UnityEngine.Random.Range(0, candidates.Count);
        picked.Add(mapList[candidates[randIndex2]]);

        //UI에 적용
        imageUI1.sprite = picked[0];
        imageUI2.sprite = picked[1];
        imageUI3.sprite = picked[2];
    }
}
