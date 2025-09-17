using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VictoryScene : MonoBehaviour
{
    [SerializeField] private CanvasGroup VictorySceneUICG;

    [SerializeField] private CanvasGroup Star_01;
    [SerializeField] private CanvasGroup Star_02;
    [SerializeField] private CanvasGroup Star_03;

    [SerializeField] private RectTransform Line;
    [SerializeField] private RectTransform Spin;
    [SerializeField] private TextMeshProUGUI LobbyBtn;


    public void ActivateVictoryUI()
    {
        Spin.DORotate(
            new Vector3(0f, 0f, 180f), // �� ����
            2f,                        // 2�� ���� ȸ��
            RotateMode.FastBeyond360   // �ڿ������� ���� ȸ��
        ).SetLoops(-1).SetEase(Ease.Linear);

        // UI ������ �������� ����
        Sequence seq = DOTween.Sequence();
        seq.Join(VictorySceneUICG.DOFade(1f, 1f));
        seq.AppendInterval(0.5f);
        seq.Join(Line.DOScaleX(16.2f, 1f));
        seq.AppendInterval(1f);
        seq.Join(Star_03.DOFade(1f, 0.5f));
        seq.AppendInterval(0.5f);
        seq.Join(Star_02.DOFade(1f, 0.5f));
        seq.AppendInterval(0.8f);
        seq.Append(Star_01.DOFade(1f, 2f));
        seq.AppendInterval(0.8f);
        seq.Append(LobbyBtn.DOFade(1f, 1f));


    }

    public void BackToLobby()
    {
        // �κ�� ���ư���
    }

    private void Start()
    {
        ActivateVictoryUI();
    }
}
