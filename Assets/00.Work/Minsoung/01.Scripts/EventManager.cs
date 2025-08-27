using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

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
    float _currentTime = 0;

    public static EventManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);

            
            IEventBunlyu(); // Awake에서 IEvent 분류 함해줘 ㅇㅇ
        }
        else
        {
            Destroy(this.gameObject);
        }
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

        System.Action eventAction = goodEvent ?
            () => {
                bool big = Random.Range(0, 2) == 1; // 여기 좋은 것 중에서도
                if (big) BigGoodEvent(); // 큰거
                else SmallGoodEvent(); // 작은거
            }
        :
            () => {
                bool big = Random.Range(0, 2) == 1; // 똑같이 나쁜 것 중에서도
                if (big) BigBadEvent(); // 큰거
                else SmallBadEvent(); // 작은거
            };
        eventAction();
    }

    private void SmallGoodEvent()
    {
        print("작고 좋은");
        if (smallGoodEventList.Count > 0)
        {
            int randomIndex = Random.Range(0, smallGoodEventList.Count); // List 크기까지 랜덤
            smallGoodEventList[randomIndex].StartEvent(); // 이벤트 ㄹㅊㄱ
            print("작고 좋은" + randomIndex); // 디버깅용민 엌
        }
    }

    private void BigGoodEvent()
    {
        print("크고 좋은");
        if (bigGoodEventList.Count > 0)
        {
            int randomIndex = Random.Range(0, bigGoodEventList.Count);
            bigGoodEventList[randomIndex].StartEvent();
            print("크고 좋은" + randomIndex); // 디버깅용민 엌
        }
    }

    private void SmallBadEvent()
    {
        print("작고 나쁜");
        if (smallBadEventList.Count > 0)
        {
            int randomIndex = Random.Range(0, smallBadEventList.Count);
            smallBadEventList[randomIndex].StartEvent();
            print("작고 나쁜" + randomIndex); // 디버깅용민 엌
        }
    }

    private void BigBadEvent()
    {
        print("크고 나쁜");
        if (bigBadEventList.Count > 0)
        {
            int randomIndex = Random.Range(0, bigBadEventList.Count);
            bigBadEventList[randomIndex].StartEvent();
            print("크고 나쁜" + randomIndex); // 디버깅용민 엌
        }
    }
}