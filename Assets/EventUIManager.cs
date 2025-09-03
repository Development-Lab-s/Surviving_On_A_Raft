using Febucci.UI;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EventUIManager : MonoBehaviour
{
    [Header("이벤트별 예고 메시지들")]
    [Tooltip("효과 넣을려면 Text Animator 써야함")]
    [field: SerializeField] private List<string> smallGoodMessage = new List<string>();
    [field: SerializeField] private List<string> BigGoodMessage = new List<string>();
    [field: SerializeField] private List<string> smallBadMessage = new List<string>();
    [field: SerializeField] private List<string> BigBadMessage = new List<string>();

    private TypewriterByCharacter _eventText;
    public static EventUIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        _eventText = GetComponentInChildren<TypewriterByCharacter>();
    }

    public void WhatEvent(GameEventType gameEventType)
    {
        Debug.Log($"WhatEvent 호출됨: {gameEventType}");

        // EventManager에서 게임 이벤트 타입 받아오고 그거에 따라 랜덤으로 메시지 실행
        switch (gameEventType)
        {
            case GameEventType.SmallGood:
                if (smallGoodMessage.Count > 0)
                {
                    int rand1 = Random.Range(0, smallGoodMessage.Count);
                    Debug.Log($"Small Good 메시지 선택: {rand1}, 내용: {smallGoodMessage[rand1]}");
                    SetEventText(smallGoodMessage[rand1]);
                }
                else
                {
                    Debug.LogWarning("smallGoodMessage 리스트가 비어있습니다!");
                }
                break;

            case GameEventType.BigGood:
                if (BigGoodMessage.Count > 0)
                {
                    int rand2 = Random.Range(0, BigGoodMessage.Count);
                    Debug.Log($"Big Good 메시지 선택: {rand2}, 내용: {BigGoodMessage[rand2]}");
                    SetEventText(BigGoodMessage[rand2]);
                }
                else
                {
                    Debug.LogWarning("BigGoodMessage 리스트가 비어있습니다!");
                }
                break;

            case GameEventType.SmallBad:
                if (smallBadMessage.Count > 0)
                {
                    int rand3 = Random.Range(0, smallBadMessage.Count);
                    Debug.Log($"Small Bad 메시지 선택: {rand3}, 내용: {smallBadMessage[rand3]}");
                    SetEventText(smallBadMessage[rand3]);
                }
                else
                {
                    Debug.LogWarning("smallBadMessage 리스트가 비어있습니다!");
                }
                break;

            case GameEventType.BigBad:
                if (BigBadMessage.Count > 0)
                {
                    int rand4 = Random.Range(0, BigBadMessage.Count);
                    Debug.Log($"Big Bad 메시지 선택: {rand4}, 내용: {BigBadMessage[rand4]}");
                    SetEventText(BigBadMessage[rand4]);
                }
                else
                {
                    Debug.LogWarning("BigBadMessage 리스트가 비어있습니다!");
                }
                break;
        }
    }

    private void SetEventText(string text)
    {
        _eventText.ShowText(text);
        print("되고 있다고 !!");
        
    }

    public void OnTypingComplete()
    {
        StartCoroutine(WaitAndDisappear());
    }

    private IEnumerator WaitAndDisappear()
    {
        yield return new WaitForSeconds(3f);
        if (_eventText != null)
        {
            _eventText.StartDisappearingText();
        }
    }
}