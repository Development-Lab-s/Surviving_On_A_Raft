using TMPro;
using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;

[System.Serializable]
public class DevInfo
{
    public string name;
    public string works;
    public Sprite image;
}

public class Creadit : MonoBehaviour
{
    [SerializeField] private CanvasGroup TextGroup;
    [SerializeField] private CanvasGroup CreditGroup;

    [SerializeField] private TextMeshProUGUI NameTxt;
    [SerializeField] private TextMeshProUGUI NameWork;
    [SerializeField] private Image DevImage;

    [SerializeField] private List<DevInfo> Developers = new List<DevInfo>();

    private Sequence creditSeq; // ���� ���� Sequence�� ����

    [ContextMenu("�׽�Ʈ ũ���� ����")]
    public void ActivateCredit()
    {
        // �ߺ� ���� ����
        if (creditSeq != null && creditSeq.IsActive())
        {
            creditSeq.Kill();
        }

        CreditGroup.gameObject.SetActive(true);
        CreditGroup.alpha = 0;
        CreditGroup.DOFade(1f, 1f);

        creditSeq = DOTween.Sequence();

        foreach (var dev in Developers)
        {
            creditSeq.AppendCallback(() =>
            {
                NameTxt.text = dev.name;
                NameWork.text = dev.works;
                DevImage.sprite = dev.image;
            });
            creditSeq.Append(TextGroup.DOFade(1f, 1f));
            creditSeq.AppendInterval(1.5f);
            creditSeq.Append(TextGroup.DOFade(0f, 1f));
            creditSeq.AppendInterval(0.5f);
        }

        creditSeq.OnComplete(() =>
        {
            CreditQuit();
        });
    }

    public void CreditQuit()
    {
        // ���� ���� ũ���� ��� ����
        if (creditSeq != null && creditSeq.IsActive())
        {
            creditSeq.Kill();
        }

        Sequence quitSeq = DOTween.Sequence();
        quitSeq.Append(CreditGroup.DOFade(0f, 1f));
        quitSeq.OnComplete(() =>
        {
            CreditGroup.gameObject.SetActive(false);
        });
    }
}
