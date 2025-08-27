using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using DG.Tweening;

public class Bim_UIManager : MonoBehaviour
{
    public static Bim_UIManager Instance { get; private set; }
    public bool isFullscreenUIEnabled = false;

    [Header("ItemCreateSelectUI")]
    [SerializeField] private RectTransform MoveObejct;
    [SerializeField] private new Vector2 UpPos;
    [SerializeField] private new Vector2 DownPos;
    [SerializeField] private Volume globalVolume;

    private DepthOfField dof;  // FieldOfView override
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    private void Start()
    {
        if (globalVolume.profile.TryGet(out DepthOfField depthOfField))
        {
            dof = depthOfField;
        }
    }

    public void ItemCreateUIView()
    {
        MoveObejct.DOAnchorPos(UpPos, 1f);

        AnimateFocus(0.1f, 0.3f);
    }

    public void ItemCreateUIUnView()
    {
        MoveObejct.DOAnchorPos(DownPos, 0.5f);
        AnimateFocus(10f, 0.3f);
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
