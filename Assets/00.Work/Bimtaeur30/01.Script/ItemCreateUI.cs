using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using DG.Tweening;

public class ItemCreateUI : MonoBehaviour
{
    [Header("")]
    [SerializeField] private RectTransform MoveObejct;
    [SerializeField] private new Vector2 UpPos;
    [SerializeField] private new Vector2 DownPos;
    [SerializeField] private Volume globalVolume;
    [SerializeField] private PlayerInput PInput;
    private DepthOfField dof;  // FieldOfView override

    private bool isUIEnabled = false;

    private void Start()
    {
        if (globalVolume.profile.TryGet(out DepthOfField depthOfField))
        {
            dof = depthOfField;
        }
    }
    public void ItemCreateUIView()
    {
        if (isUIEnabled == true)
        {
            ItemCreateUIUnView();
            return;
        }
        isUIEnabled = true;

        Debug.Log("È£Ãâ");
        if (PInput.isFullscreenUIEnabled != true)
        {
            Debug.Log(1);
            MoveObejct.DOAnchorPos(UpPos, 1f);
            AnimateFocus(0.1f, 0.3f);
            PInput.ChangeUIEnabled(true);
            Debug.Log(2);

        }
    }

    public void ItemCreateUIUnView()
    {
        isUIEnabled = false;
        MoveObejct.DOAnchorPos(DownPos, 0.5f);
        AnimateFocus(10f, 0.3f);

        PInput.ChangeUIEnabled(false);
    }

    public void ItemCreatePanelUIView()
    {

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
