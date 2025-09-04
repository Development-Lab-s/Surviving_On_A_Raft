using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapAlpha : MonoBehaviour
{


    [SerializeField] private Tilemap tilemap; // 타일맵 참조
    [SerializeField] private Transform Panel;
    [Range(0f, 1f)] [SerializeField] private float alpha = 1f; // 투명도 조절 (0 = 완전투명, 1 = 불투명)

    void Start()
    {
        if (tilemap == null) return;

        // 기존 색상 가져오기
        Color color = tilemap.color;

        // 알파만 변경
        color.a = 0.5f;

        // 적용
        tilemap.color = color;

        Panel.gameObject.SetActive(false);
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


    private void IsCanNext()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            // 아아템이 충족됏을 때 실행
            // if()
            // {
            //     Color color = tilemap.color;


            //     color.a = 1f;


            //     tilemap.color = color;
            //     SpawnManager.Instance.StartCycle();        
            // }
        }
    }

    private void ShowUI()
    {
        Panel.gameObject.SetActive(true);
    }
}
