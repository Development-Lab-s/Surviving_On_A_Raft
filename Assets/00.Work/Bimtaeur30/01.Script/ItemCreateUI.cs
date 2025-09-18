using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using DG.Tweening;

public class ItemCreateUI : MonoBehaviour
{
    [Header("UI 애니메이션 설정")]
    [SerializeField] private RectTransform MoveObject;
    [SerializeField] private Vector2 upPos;
    [SerializeField] private Vector2 downPos;
    [SerializeField] private Volume globalVolume;
    [SerializeField] private PlayerInventoryInput PInput;
    [SerializeField] private CanvasGroup UICanvasGroup;

    private DepthOfField dof; // DOF 효과 컨트롤
    private bool isUIEnabled = false;
    private Sequence currentSeq; // 현재 실행 중인 시퀀스 추적

    private void Start()
    {
        if (globalVolume.profile.TryGet(out DepthOfField depthOfField))
        {
            dof = depthOfField;
        }
    }

    public void ItemCreateUIView()
    {
        // 애니메이션 도중엔 무시
        if (currentSeq != null && currentSeq.IsActive() && currentSeq.IsPlaying())
            return;

        if (isUIEnabled == true)
        {
            Time.timeScale = 1;
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
                //AnimateFocus(0.1f, 0.3f);
                dof.focusDistance.value = 0.1f;
                PInput.ChangeUIEnabled(true);
                currentSeq = null; // 끝나면 초기화
                Time.timeScale = 0;
            });
        }
    }

    public void ItemCreateUIUnView()
    {
        // 애니메이션 도중엔 무시
        if (currentSeq != null && currentSeq.IsActive() && currentSeq.IsPlaying())
            return;

        currentSeq = DOTween.Sequence();
        currentSeq.Join(MoveObject.DOAnchorPos(downPos, 0.5f));
        currentSeq.Join(UICanvasGroup.DOFade(0f, 0.2f));

        currentSeq.OnComplete(() =>
        {
            MoveObject.gameObject.SetActive(false);
            isUIEnabled = false;
            //AnimateFocus(10f, 0.3f);
            dof.focusDistance.value = 10f;
            PInput.ChangeUIEnabled(false);
            currentSeq = null; // 끝나면 초기화
        });
    }

    public void AnimateFocus(float to, float duration)
    {
        if (dof != null)
        {
            DOTween.Kill(dof); // 기존 트윈 제거
            DOTween.To(() => dof.focusDistance.value,
                       x => dof.focusDistance.value = x,
                       to, duration).SetTarget(dof);
        }
    }
}
