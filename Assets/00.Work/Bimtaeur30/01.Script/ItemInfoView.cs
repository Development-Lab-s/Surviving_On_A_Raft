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
    [SerializeField] private Image ButtonImage;

    [SerializeField] private Vector2 IIUPPos;
    [SerializeField] private Vector2 IIDownPos;

    [SerializeField] private Volume globalVolume;

    [SerializeField] private PlayerInput PInput;

    private DepthOfField dof;  // FieldOfView override

    private void Start()
    {
        if (globalVolume.profile.TryGet(out DepthOfField depthOfField))
        {
            dof = depthOfField;
        }
    }

    public void ItemInfoViewMethod(ExItemSO ItemSO)
    {
        if (PInput.isActiveAndEnabled == true)
            return;
        PInput.ChangeUIEnabled(true);

        Sequence seq = DOTween.Sequence();
        AnimateFocus(0.1f, 0.3f);
        seq.Join(ItemInfo.DOAnchorPos(IIUPPos, 0.8f));
        seq.Join(BackgroundGroup.DOFade(1f, 1f));
        ItemImage.sprite = ItemSO.ItemImage;
        ItemNameTxt.text = ItemSO.ItemName;
        ItemDescriptionTxt.text = ItemSO.ItemDescription;

        seq.AppendInterval(1f);

        seq.Join(ButtonImage.DOFade(1f, 1f));
    }

    public void ItemInfoUnViewMethod()
    {
        PInput.ChangeUIEnabled(false);

        Sequence seq = DOTween.Sequence();
        AnimateFocus(10f, 0.3f);
        seq.Join(ItemInfo.DOAnchorPos(IIDownPos, 0.8f));
        seq.Join(BackgroundGroup.DOFade(0f, 1f));
        seq.AppendInterval(0f);

        seq.Join(ButtonImage.DOFade(0f, 1f));
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
