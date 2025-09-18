using _00.Work.Resource.Manager;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _00.Work.Resource.Codes
{
    public class MenuUIBtn : MonoBehaviour
    {
        [SerializeField] private GameObject menu;
        [SerializeField] private Button mainButton;
        public Slider bgmSlider;
        public Slider sfxSlider;
        
        private bool _isPressedEsc;

        private void Start()
        {
            bgmSlider.value = SoundManager.Instance.GetBGMVolume();
            sfxSlider.value = SoundManager.Instance.GetSfxVolume();

            bgmSlider.onValueChanged.AddListener((v) => SoundManager.Instance.SetBgmVolume(v));
            sfxSlider.onValueChanged.AddListener((v) => SoundManager.Instance.SetSfxVolume(v));
            menu.SetActive(false);
        }

        public void MainMenu()
        {
            menu.SetActive(true);
            mainButton.gameObject.SetActive(false);
            Time.timeScale = 0;
        }
        
        public void ContinueButton()
        {
            _isPressedEsc = !_isPressedEsc;
            
            Time.timeScale = 1;
            mainButton.gameObject.SetActive(true);
            menu.SetActive(false);
        }

        public void ExitButton()
        {
            if (SceneManager.GetActiveScene().buildIndex != 0)
            {
                Time.timeScale = 1;
                FadeManager.Instance.FadeToScene(0);
            }
            else
            {
                Application.Quit();
            }
        }
    }
}