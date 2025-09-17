using _00.Work.Resource.Manager;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum DeathReasonEnum
{
    watarDie,
    enemyDie,
    DisasterDie
}

public class DeadScene : MonoSingleton<DeadScene>
{
    [SerializeField] private TextMeshProUGUI Title;
    [SerializeField] private TextMeshProUGUI Description;
    [SerializeField] private Image Line;
    [SerializeField] private CanvasGroup DeadSceneGroup;


    public void ActiveDeadScene(DeathReasonEnum reason)
    {
        if (reason == DeathReasonEnum.watarDie)
        {
            Title.text = "익사";
            Description.text = "숨을 쉴수가 없네요";
        }
        else if (reason == DeathReasonEnum.enemyDie)
        {
            Title.text = "과다출혈";
            Description.text = "적을 감당하지 못했어요";
        }
        else if (reason == DeathReasonEnum.DisasterDie)
        {
            Title.text = "자연재해";
            Description.text = "정신을 차릴수가 없네요";
        }
        DeadSceneGroup.alpha = 0f;

        Vector3 scale = Line.rectTransform.localScale;
        scale.x = 0.1f;          
        Line.rectTransform.localScale = scale;

        Sequence seq = DOTween.Sequence();
        seq.Join(DeadSceneGroup.DOFade(1f, 1f));
        seq.AppendInterval(0.5f);
        seq.Join(Line.rectTransform.DOScaleX(2f, 1f));
        seq.Join(Line.DOFade(1f, 1f));
        
    }

    public void ReturnToLobby()
    {
        FadeManager.Instance.FadeToScene(0);
    }
}
