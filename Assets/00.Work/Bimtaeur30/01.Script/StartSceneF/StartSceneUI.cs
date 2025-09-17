using DG.Tweening;
using System.Text.RegularExpressions;
using _00.Work.Resource.Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartSceneUI : MonoBehaviour
{
    [SerializeField] RectTransform TitleRec;
    [SerializeField] RectTransform TxtRec;
    [SerializeField] RectTransform ButtonsRec;
    
    [SerializeField] private GameObject StartBtn;

    [SerializeField] Image Line;
    private void Start()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(Line.DOFade(1f, 1f));
        seq.AppendInterval(1f);
        seq.Join(TitleRec.DOAnchorPos(new Vector2(0, 203), 1f));
        seq.Join(TxtRec.DOAnchorPos(new Vector2(0, 75), 1f));
        seq.Join(ButtonsRec.DOAnchorPos(new Vector2(0, -480), 1f));
        seq.Join(TitleRec.gameObject.GetComponent<CanvasGroup>().DOFade(1f, 1f));
        seq.Join(TxtRec.gameObject.GetComponent<TextMeshProUGUI>().DOFade(1f, 1f));
        
        seq.OnComplete(() => StartBtn.SetActive(true));
    }
}
