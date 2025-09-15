using UnityEngine;

namespace _00.Work.Hedonism._06.Scripts.ChangeMap
{
    public class ShowRadderUI : MonoBehaviour
    {
        public StageSelectState State { get; private set; } = StageSelectState.None;
        private void Awake()
        {
            // 매니저에 자기 자신 등록
            ShowStageSelectUI.Instance.RegisterLadder(this);
        }

        public void SetState(StageSelectState newState)
        {
            State = newState;
        }

        public void ResetLadder()
        {
            State = StageSelectState.None;
        }

        public void MarkUsed()
        {
            State = StageSelectState.Finished;
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                ShowStageSelectUI.Instance.ShowMaps(this);
            }
        }

        void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                ShowStageSelectUI.Instance?.CloseMapUI();
            }
        }
    }
}