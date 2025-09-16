using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using DG.Tweening;

public class ItemInfoView : MonoBehaviour
{
    [SerializeField] private CanvasGroup BackgroundGroup;
    [SerializeField] private RectTransform ItemInfo;
    [SerializeField] private Image ItemImage;
    [SerializeField] private TextMeshProUGUI ItemNameTxt;
    [SerializeField] private TextMeshProUGUI ItemDescriptionTxt;
    [SerializeField] private TextMeshProUGUI ItemLevelTxt; // Level 표시 추가
    [SerializeField] private Image ButtonImage;

    [SerializeField] private Vector2 IIUPPos;
    [SerializeField] private Vector2 IIDownPos;

    [SerializeField] private Volume globalVolume;

    [SerializeField] private PlayerInventoryInput pInventoryInput;

    private DepthOfField dof;

    private void Start()
    {
        if (globalVolume.profile.TryGet(out DepthOfField depthOfField))
        {
            dof = depthOfField;
        }
    }

    // PlayerItem 기준으로 표시
    public void ItemInfoViewMethod(PlayerItem playerItem)
    {

        if (!pInventoryInput.isFullscreenUIEnabled)
        {
            Sequence seq = DOTween.Sequence();
            AnimateFocus(0.1f, 0.3f);
            seq.Join(ItemInfo.DOAnchorPos(IIUPPos, 0.8f));
            seq.Join(BackgroundGroup.DOFade(1f, 1f));

            ItemImage.sprite = playerItem.Template.ItemImage;
            ItemNameTxt.text = playerItem.Template.ItemName;
            ItemDescriptionTxt.text = playerItem.Template.ItemDescription;
            ItemLevelTxt.text = "Level: " + playerItem.Level; // 레벨 표시

            seq.Join(ButtonImage.DOFade(1f, 1f));
            seq.OnComplete(() => Time.timeScale = 0);

            pInventoryInput.ChangeUIEnabled(true);
            
        }
    }

    public void ItemInfoUnViewMethod()
    {
        Time.timeScale = 1;
        Sequence seq = DOTween.Sequence();
        AnimateFocus(10f, 0.3f);
        seq.Join(ItemInfo.DOAnchorPos(IIDownPos, 0.8f));
        seq.Join(BackgroundGroup.DOFade(0f, 1f));
        seq.AppendInterval(0f);
        seq.Join(ButtonImage.DOFade(0f, 1f));

        pInventoryInput.ChangeUIEnabled(false);
    }

    public void AnimateFocus(float to, float duration)
    {
        if (dof != null)
        {
            DOTween.To(() => dof.focusDistance.value,
                       x => dof.focusDistance.value = x,
                       to, duration);
        }
    }
}
