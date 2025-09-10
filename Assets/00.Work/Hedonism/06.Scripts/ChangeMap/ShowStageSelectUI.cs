using System;
using System.Collections.Generic;
using _00.Work.CheolYee._01.Codes.SO;
using _00.Work.Resource.Manager;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Random = UnityEngine.Random;
public class ShowStageSelectUI : MonoSingleton<ShowStageSelectUI>
{
    [field: SerializeField] public GameObject mapUI { get; private set; }

    [SerializeField] private PlayerInputSo playerInputSo;
    [SerializeField] private Image[] mapImages;
    [SerializeField] private List<Sprite> mapSprites;
    [SerializeField] private List<Image> outlines;

    int random;
    List<int> selectIndex = new List<int>();
    private bool IsFirst = true;
    private bool IsResourceUI = false;

    [SerializeField] private int currentSelectIndex = 0;

    protected override void Awake()
    {
        base.Awake();

        IsFirst = true;
        IsResourceUI = false;
        mapUI.SetActive(false);
    }

    void Start()
    {
        playerInputSo.OnFkeyPress += IsPay;
    }

    void Update()
    {
        if (mapUI.activeSelf)
        {
            if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
            {
                foreach (var outline in outlines)
                {
                    outline.enabled = false;
                }                   
                    outlines[++currentSelectIndex].enabled = true;
            }

            if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
            {
                foreach (var outline in outlines)
                {
                    outline.enabled = false;
                }
                    outlines[--currentSelectIndex].enabled = true;
            }

            Mathf.Clamp(currentSelectIndex, 0, 2);
        }
    }

    private void IsPay()
    {
        int selectedIndex = selectIndex[currentSelectIndex];
        Debug.Log(selectedIndex);
        IsResourceUI = true;
        mapUI.SetActive(false);
        //자원 ui 활성화
    }

    public void ShowMaps(Vector3 pos)
    {
        if (!IsResourceUI)
        {
            mapUI.SetActive(true);
            mapUI.transform.position = pos;
            if (IsFirst) RandomMapImages();
        }
    }

    public void CloseMapUI()
    {
        mapUI.SetActive(false);
    }

    public void ResetMapUI()
    {
        selectIndex.Clear();
        IsFirst = true;
    }

    // private void RandomMapImages()
    // {
    //     IsFirst = false;
    //     RandomIndex(random);
    //     for (int i = 0; i < mapImages.Length; i++)
    //     {
    //         mapImages[i].sprite = mapSprites[selectIndex[i]];
    //     }
    //     foreach (var outline in outlines)
    //     {
    //         outline.enabled = false;
    //     }
    //     outlines[currentSelectIndex].enabled = true;
    // }

    private void RandomMapImages()
    {
        IsFirst = false;
        RandomIndex(mapImages.Length);
        for (int i = 0; i < mapImages.Length; i++)
        {
            mapImages[i].sprite = mapSprites[selectIndex[i]];
        }
        foreach (var outline in outlines)
        {
            outline.enabled = false;
        }
        outlines[currentSelectIndex].enabled = true;
    }


    // private void RandomIndex(int randomIndex)
    // {
    //     if (selectIndex.Count == 3) return;

        //     random = Random.Range(0, mapSprites.Count);
        //     if (selectIndex.Contains(randomIndex))
        //     {
        //         RandomIndex(random);
        //     }
        //     else
        //     {
        //         selectIndex.Add(random);
        //         RandomIndex(random);
        //     }
        // }


    private void RandomIndex(int randomIndex)
    {
        selectIndex.Clear();

        List<int> tempList = new List<int>();
        while (tempList.Count < randomIndex)
        {
            int rand = Random.Range(0, mapSprites.Count);
            if (!tempList.Contains(rand))
            {
                tempList.Add(rand);
            }
        }

        selectIndex = tempList;
    }
}
