using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using DG.Tweening;

public class ItemCreateUI : MonoBehaviour
{
    [Header("UI �ִϸ��̼� ����")]
    [SerializeField] private RectTransform MoveObject;
    [SerializeField] private Vector2 upPos;
    [SerializeField] private Vector2 downPos;
    [SerializeField] private Volume globalVolume;
    [SerializeField] private PlayerInput PInput;
    [SerializeField] private CanvasGroup UICanvasGroup;

    private DepthOfField dof; // DOF ȿ�� ��Ʈ��
    private bool isUIEnabled = false;
    private Sequence currentSeq; // ���� ���� ���� ������ ����

    private void Start()
    {
        if (globalVolume.profile.TryGet(out DepthOfField depthOfField))
        {
            dof = depthOfField;
        }
    }

    public void ItemCreateUIView()
    {
        // �ִϸ��̼� ���߿� ����
        if (currentSeq != null && currentSeq.IsActive() && currentSeq.IsPlaying())
            return;

        if (isUIEnabled == true)
        {
            ItemCreateUIUnView();
            return;
        }

        if (PInput.isFullscreenUIEnabled != true)
        {
            MoveObject.gameObject.SetActive(true);

            currentSeq = DOTween.Sequence();
            currentSeq.Join(MoveObject.DOAnchorPos(upPos, 0.3f));
            currentSeq.Join(UICanvasGroup.DOFade(1f, 0.5f));

            currentSeq.OnComplete(() =>
            {
                isUIEnabled = true;
                AnimateFocus(0.1f, 0.3f);
                PInput.ChangeUIEnabled(true);
                currentSeq = null; // ������ �ʱ�ȭ
            });
        }
    }

    public void ItemCreateUIUnView()
    {
        // �ִϸ��̼� ���߿� ����
        if (currentSeq != null && currentSeq.IsActive() && currentSeq.IsPlaying())
            return;

        currentSeq = DOTween.Sequence();
        currentSeq.Join(MoveObject.DOAnchorPos(downPos, 0.5f));
        currentSeq.Join(UICanvasGroup.DOFade(0f, 0.2f));

        currentSeq.OnComplete(() =>
        {
            MoveObject.gameObject.SetActive(false);
            isUIEnabled = false;
            AnimateFocus(10f, 0.3f);
            PInput.ChangeUIEnabled(false);
            currentSeq = null; // ������ �ʱ�ȭ
        });
    }

    public void AnimateFocus(float to, float duration)
    {
        if (dof != null)
        {
            DOTween.Kill(dof); // ���� Ʈ�� ����
            DOTween.To(() => dof.focusDistance.value,
                       x => dof.focusDistance.value = x,
                       to, duration).SetTarget(dof);
        }
    }
}
