using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum GameEventType
{
    SmallGood,
    BigGood,
    SmallBad,
    BigBad
}

public class EventManager : MonoBehaviour
{
    [Header("GoodEvent")]
    public List<IEvent> smallGoodEventList = new List<IEvent>();
    public List<IEvent> bigGoodEventList = new List<IEvent>();

    [Header("BadEvent")]
    public List<IEvent> smallBadEventList = new List<IEvent>();
    public List<IEvent> bigBadEventList = new List<IEvent>();

    [SerializeField] private float _cycle = 30f;
    [SerializeField] private float _eventStartOffset = 10f;
    float _currentTime = 0;

    public float CurrentEventDuration { get; private set; }


    public static EventManager Instance { get; private set; }

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
        IEventBunlyu(); // Awake에서 IEvent 분류 함해줘 ㅇㅇ

    }

    private void IEventBunlyu() // 이벤트들 타입별로 분류
    {
        IEvent[] Events = GetComponentsInChildren<IEvent>(); // 자식에서 IEvent 상속 가지고 있으면 다 가져오고 배열에 넣어 ㅇㅇ

        foreach (IEvent iEventCompo in Events) //  자기 스크립트에 게임 타입 찾기 위해서 포이치 돌려서 뽑아
        {
            switch (iEventCompo.eventType) // 타입별로 스위치로 분류 ㄷㄱㅈ
            {
                case GameEventType.SmallGood:
                    smallGoodEventList.Add(iEventCompo); // 작고 좋은
                    break;
                case GameEventType.BigGood:
                    bigGoodEventList.Add(iEventCompo); // 크고 좋은
                    break;
                case GameEventType.SmallBad:
                    smallBadEventList.Add(iEventCompo); // 작고 나쁜
                    break;
                case GameEventType.BigBad:
                    bigBadEventList.Add(iEventCompo); // 크고 나쁜
                    break;
            }
        }
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;

        if (_currentTime >= _cycle)
        {
            _currentTime -= _cycle;
            StartEvent();
        }
        
    }

    public void StartEvent()
    {
        bool goodEvent = Random.Range(0, 2) == 1; // 굿 이벤트 true? 그럼 삼항 연산자로 

        GameEventType selectedEventType;

        if (goodEvent)
        {
            bool big = Random.Range(0, 2) == 1; // 여기 좋은 것 중에서도
            selectedEventType = big ? GameEventType.BigGood : GameEventType.SmallGood;
        }
        else
        {
            bool big = Random.Range(0, 2) == 1; // 똑같이 나쁜 것 중에서도
            selectedEventType = big ? GameEventType.BigBad : GameEventType.SmallBad;
        }

        // UI를 먼저 보여주고, 오프셋 뒤에 실제 이벤트 실행
        EventStartOffset(selectedEventType);
    }

    private void EventStartOffset(GameEventType gameEventType)
    {
        EventUIManager.Instance.WhatEvent(gameEventType);
        
        switch (gameEventType)
        {
            case GameEventType.SmallGood:
                StartCoroutine(SmallGoodEvent());
                break;
            case GameEventType.BigGood:
                StartCoroutine(BigGoodEvent());
                break;
            case GameEventType.SmallBad:
                StartCoroutine(SmallBadEvent());
                break;
            case GameEventType.BigBad:
                StartCoroutine(BigBadEvent());
                break;
        }
    }

    private IEnumerator SmallGoodEvent()
    {
        print("작고 좋은 이벤트 실행");
        if (smallGoodEventList.Count > 0)
        {
            int randomIndex = Random.Range(0, smallGoodEventList.Count); // List 크기까지 랜덤
            smallGoodEventList[randomIndex].StartEventEffectText(); // 부제목? 효과 띄어주기
            yield return new WaitForSeconds(_eventStartOffset);
            smallGoodEventList[randomIndex].StartEvent(); // 이벤트 ㄹㅊㄱ
            
            
        }
    }

    private IEnumerator BigGoodEvent()
    {
        print("크고 좋은 이벤트 실행");
        if (bigGoodEventList.Count > 0)
        {
            int randomIndex = Random.Range(0, bigGoodEventList.Count);
            bigGoodEventList[randomIndex].StartEventEffectText();
            yield return new WaitForSeconds(_eventStartOffset);
            bigGoodEventList[randomIndex].StartEvent();
        }
    }

    private IEnumerator SmallBadEvent()
    {
        print("작고 나쁜 이벤트 실행");
        if (smallBadEventList.Count > 0)
        {
            int randomIndex = Random.Range(0, smallBadEventList.Count);
            smallBadEventList[randomIndex].StartEventEffectText();
            yield return new WaitForSeconds(_eventStartOffset);
            smallBadEventList[randomIndex].StartEvent();
        }
    }

    private IEnumerator BigBadEvent()
    {
        print("크고 나쁜 이벤트 실행");
        if (bigBadEventList.Count > 0)
        {
            int randomIndex = Random.Range(0, bigBadEventList.Count);
            bigBadEventList[randomIndex].StartEventEffectText();
            yield return new WaitForSeconds(_eventStartOffset);
            bigBadEventList[randomIndex].StartEvent();
        }
    }
}