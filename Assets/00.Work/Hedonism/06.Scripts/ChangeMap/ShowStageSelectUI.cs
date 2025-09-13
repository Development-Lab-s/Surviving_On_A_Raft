using System.Collections.Generic;
using _00.Work.CheolYee._01.Codes.Managers;
using _00.Work.CheolYee._01.Codes.MapUI;
using _00.Work.CheolYee._01.Codes.SO;
using _00.Work.Hedonism._06.Scripts.SO.Manager;
using _00.Work.Nugusaeyo._Script.Cost;
using _00.Work.Nugusaeyo._Script.SO;
using _00.Work.Resource.Manager;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace _00.Work.Hedonism._06.Scripts.ChangeMap
{
    public enum StageSelectState
    {
        None,
        MapSelect,     // 맵 3개 선택 화면
        ResourceCheck, // 재료 요구사항 화면
        Finished       // 최종 확정
    }

    public class ShowStageSelectUI : MonoSingleton<ShowStageSelectUI>
    {
        [field: SerializeField] public GameObject MapUI { get; private set; }
        [field: SerializeField] public MapResourceUI ResourceUI { get; private set; }

        [SerializeField] private PlayerInputSo playerInputSo;
        [SerializeField] private Image[] mapImages;
        [SerializeField] private List<MapDataSo> mapData;
        [SerializeField] private List<Image> outlines;

        private List<ShowRadderUI> _allLadders = new(); // 모든 사다리 등록 리스트

        private ShowRadderUI _currentEntrance; // 현재 조작 중인 사다리
        private int _currentSelectIndex;
       [HideInInspector] public int lastChosenIndex; // 이전에 선택된 맵
        private List<int> _selectIndex = new();

        protected override void Awake()
        {
            base.Awake();
            Initialize();
        }

        public void Initialize()
        {
            MapUI.SetActive(false);
            ResourceUI.gameObject.SetActive(false);
        }

        void Start()
        {
            playerInputSo.OnFkeyPress += MapSelect;
        }

        void Update()
        {
            if (MapUI.activeSelf)
            {
                if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
                {
                    foreach (var outline in outlines) outline.enabled = false;
                    _currentSelectIndex = Mathf.Clamp(++_currentSelectIndex, 0, 2);
                    outlines[_currentSelectIndex].enabled = true;
                }

                if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
                {
                    foreach (var outline in outlines) outline.enabled = false;
                    _currentSelectIndex = Mathf.Clamp(--_currentSelectIndex, 0, 2);
                    outlines[_currentSelectIndex].enabled = true;
                }
            }
        }

        private void MapSelect()
        {
            if (_currentEntrance == null) return;

            // 1단계: 맵 선택 상태일 때 - ResourceCheck로 전환
            if (_currentEntrance.State == StageSelectState.MapSelect)
            {
                _currentEntrance.SetState(StageSelectState.ResourceCheck);
                MapUI.SetActive(false);
                ResourceUI.gameObject.SetActive(true);

                int selectedIndex = _selectIndex[_currentSelectIndex];
                ResourceUI.ShowRequirements(mapData[selectedIndex]);
                return;
            }

            // 2단계: 재료 확인 상태일 때 - 지불 및 맵 확정
            if (_currentEntrance.State == StageSelectState.ResourceCheck)
            {
                int selectedIndex = _selectIndex[_currentSelectIndex];

                // 지불 가능 여부 체크
                foreach (ResourceData resource in mapData[selectedIndex].resourceDatas)
                {
                    if (!CostManager.Instance.IsPaid(resource.resourceIndex, resource.resourceAmount))
                    {
                        Debug.Log("지불할 수 없습니다.");
                        return;
                    }
                }

                // 지불 처리
                foreach (ResourceData resource in mapData[selectedIndex].resourceDatas)
                {
                    CostManager.Instance.MinusCost(resource.resourceIndex, resource.resourceAmount);
                }

                Debug.Log("맵 생성 완료!");

                StatManager.Instance.ResetGrowthMultipliers(); //적 성장 초기화;
                SpawnManager.Instance.StartCycle(selectedIndex);

                GameManager.Instance.currentLevel++;
                

                lastChosenIndex = selectedIndex; // 이번에 선택된 맵 저장
                _currentEntrance.MarkUsed(); // 사다리 상태 확정
                ResetAllLadders();
            }
        }


        public void ShowMaps(ShowRadderUI entrance)
        {
            if (entrance.State == StageSelectState.Finished) return; 

            _currentEntrance = entrance;
            
            switch (entrance.State)
            {
                case StageSelectState.None:
                    // 처음 접근: 맵 선택 UI
                    _currentEntrance.SetState(StageSelectState.MapSelect);
                    MapUI.SetActive(true);
                    ResourceUI.gameObject.SetActive(false);
                    RandomMapImages();
                    break;

                case StageSelectState.MapSelect:
                    // 이미 맵 선택 중이던 사다리: 맵 UI 복구
                    MapUI.SetActive(true);
                    ResourceUI.gameObject.SetActive(false);
                    break;

                case StageSelectState.ResourceCheck:
                    // 이미 자원 요구사항 단계였던 사다리: Resource UI 복구
                    MapUI.SetActive(false);
                    ResourceUI.gameObject.SetActive(true);

                    int selectedIndex = _selectIndex[_currentSelectIndex];
                    ResourceUI.ShowRequirements(mapData[selectedIndex]);
                    break;
            }
        }

        public void CloseMapUI()
        {
            MapUI.SetActive(false);
            ResourceUI.gameObject.SetActive(false);
            _currentEntrance = null;
        }

        public void ResetMapUI()
        {
            _selectIndex.Clear();
            _currentEntrance = null;
        }

        private void RandomMapImages()
        {
            RandomIndex(mapImages.Length);
            for (int i = 0; i < mapImages.Length; i++)
            {
                mapImages[i].sprite = mapData[_selectIndex[i]].mapIcon;
            }
            foreach (var outline in outlines) outline.enabled = false;
            outlines[_currentSelectIndex].enabled = true;
        }

        private void RandomIndex(int count)
        {
            if (count == 0) return;
            _selectIndex.Clear();

            List<int> tempList = new List<int>();
            int safety = 0;

            while (tempList.Count < count && safety < 1000)
            {
                int rand = Random.Range(0, mapData.Count);

                // 직전 선택 맵은 제외
                if (rand == lastChosenIndex) { safety++; continue; }

                if (!tempList.Contains(rand))
                    tempList.Add(rand);
            }

            _selectIndex = tempList;
        }

        // 사다리 등록
        public void RegisterLadder(ShowRadderUI ladder)
        {
            if (!_allLadders.Contains(ladder))
                _allLadders.Add(ladder);
        }

        // 씬 전환 시 전체 초기화
        public void ResetAllLadders()
        {
            foreach (var ladder in _allLadders)
            {
                ladder.ResetLadder();
            }

            ResetMapUI();
        }
    }
}
