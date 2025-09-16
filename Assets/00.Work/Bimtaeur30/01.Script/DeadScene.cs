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

public class DeadScene : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Title;
    [SerializeField] private TextMeshProUGUI Description;
    [SerializeField] private Image Line;
    [SerializeField] private CanvasGroup DeadSceneGroup;


    public void ActiveDeadScene(DeathReasonEnum reason)
    {
        if (reason == DeathReasonEnum.watarDie)
        {
            Title.text = "�ͻ�";
            Description.text = "���� ������ ���׿�";
        }
        else if (reason == DeathReasonEnum.enemyDie)
        {
            Title.text = "��������";
            Description.text = "���� �������� ���߾��";
        }
        else if (reason == DeathReasonEnum.DisasterDie)
        {
            Title.text = "�ڿ�����";
            Description.text = "������ �������� ���׿�";
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
        // �κ� �� ��ȯ
    }

    private void Start()
    {
        ActiveDeadScene(DeathReasonEnum.enemyDie);
    }
    //,
    //dddddhgfftf
}
