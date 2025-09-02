using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _00.Work.Resource.Manager
{
    public class FadeManager : MonoSingleton<FadeManager>
    {
        [Header("Fade UI")]
        public Image fadeImage;
        public float fadeDuration = 1f;

        protected override void Awake()
        {
            base.Awake();
            if (Instance == this)
            {
                DontDestroyOnLoad(this.gameObject);
            }
        }

        public void FadeOut(System.Action onComplete = null)
        {
            if (fadeImage == null)
            {
                return;
            }
            
            fadeImage.gameObject.SetActive(true);
            fadeImage.color = new Color(0, 0, 0, 1);
            fadeImage.DOFade(0f, fadeDuration).OnComplete(() =>
            {
                fadeImage.gameObject.SetActive(false);
                onComplete?.Invoke();
            });
        }

        public void FadeIn(System.Action onFadeComplete = null)
        {
            if (fadeImage == null)
            {
                return;
            }
            fadeImage.gameObject.SetActive(true);
            fadeImage.color = new Color(0,0,0,0);
            fadeImage.DOFade(5f, fadeDuration).OnComplete(() =>
            {
                onFadeComplete?.Invoke();
                FadeManager.Instance.FadeOut();
            });
        }
        
        public void FadeToScene(int sceneIndex)
        {
            FadeIn(() =>
            {
                SceneManager.LoadScene(sceneIndex);
            });
        }


        public void FadeToSceneDelay(int sceneIndex)
        {
            StartCoroutine(DelayAndFadeToScene(sceneIndex));
        }
        
        public IEnumerator DelayAndFadeToScene(int sceneIndex)
        {
            yield return null; // 한 프레임 대기: 모든 Awake() 보장
            FadeManager.Instance.FadeToScene(sceneIndex);
        }
    }
}