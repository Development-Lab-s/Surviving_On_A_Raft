using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _00.Work.CheolYee._01.Codes.MapUI
{
    public class Tutorial : MonoBehaviour
    {
        [SerializeField] private List<Sprite> tutoImgs;
        [SerializeField] private GameObject tutorialPanel;
        [SerializeField] private Image currentTutoImage;
        [SerializeField] private TextMeshProUGUI currentTutoText;

        private int _currentTutoIndex;
        private void Start()
        {
            _currentTutoIndex = 0;
            currentTutoText.text = $"{_currentTutoIndex}";
            currentTutoImage.sprite = tutoImgs[_currentTutoIndex];
        }

        public void OpenTutorial()
        {
            if (tutorialPanel.activeSelf)
            {
                tutorialPanel.SetActive(false);
            }
            else
            {
                tutorialPanel.SetActive(true);
            }
        }

        public void NextTutorial()
        {
            if (_currentTutoIndex < tutoImgs.Count -1)
            {
                currentTutoText.text = $"{_currentTutoIndex + 1}";
                _currentTutoIndex++;
                currentTutoImage.sprite = tutoImgs[_currentTutoIndex];
            }
        }

        public void BackTutorial()
        {
            if (_currentTutoIndex <= 0 == false)
            {
                currentTutoText.text = $"{_currentTutoIndex + 1}";
                _currentTutoIndex--;
                currentTutoImage.sprite = tutoImgs[_currentTutoIndex];
            }
        }
    }
}