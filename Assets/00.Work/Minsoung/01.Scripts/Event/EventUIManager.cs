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

    private TypewriterByCharacter[] _eventText;
    public static EventUIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        _eventText = GetComponentsInChildren<TypewriterByCharacter>();
    }

    public void WhatEvent(GameEventType gameEventType)
    {
        // EventManager에서 게임 이벤트 타입 받아오고 그거에 따라 랜덤으로 메시지 실행
        switch (gameEventType)
        {
            case GameEventType.SmallGood:
                if (smallGoodMessage.Count > 0)
                {
                    int rand1 = Random.Range(0, smallGoodMessage.Count);
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
        _eventText[0].ShowText(text);
    }

    public void SetEventTextEffect(string text)
    {
        _eventText[1].ShowText(text);
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
            _eventText[0].StartDisappearingText();
            _eventText[1].StartDisappearingText();
        }
    }
}