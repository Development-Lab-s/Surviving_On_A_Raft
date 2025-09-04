using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SceneFader : MonoBehaviour
{
    public static SceneFader Instance;
    public Image fadeImage;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        FadeIn();
    }

    public void FadeIn(float duration = 1f)
    {
        fadeImage.gameObject.SetActive(true);
        
        fadeImage.color = new Color(0, 0, 0, 1);
        
        fadeImage.DOFade(0f, duration).OnComplete(() =>
        {
            fadeImage.gameObject.SetActive(false);
        });
    }

    public void FadeOut(float duration = 1f)
    {
        fadeImage.gameObject.SetActive(true);

        fadeImage.color = new Color(0, 0, 0, 0);
        fadeImage.DOFade(1f, duration);
    }
}