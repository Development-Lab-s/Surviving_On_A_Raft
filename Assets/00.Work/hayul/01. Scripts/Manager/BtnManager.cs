using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnManager : MonoBehaviour
{
    [SerializeField] private GameObject _activePanel;
    [SerializeField] private GameObject _passivePanel;
    [SerializeField] private GameObject _settingPanel;
    private CanvasGroup _activeGroup;
    private CanvasGroup _settingGroup;

    private void Awake()
    {
        _activeGroup = _activePanel.GetComponent<CanvasGroup>();
        _settingGroup = _settingPanel.GetComponent<CanvasGroup>();
    }

    public void StartBtn()
    {
        SceneManager.LoadScene(1);
    }

    public void GuideBtn()
    {
        _activeGroup.DOFade(1, 0.5f).OnComplete(() =>
        {
            _activeGroup.blocksRaycasts = true;
        });
    }

    public void SettingBtn()
    {
        _settingGroup.DOFade(1, 0.5f).OnComplete(() =>
        {
            _settingGroup.blocksRaycasts = true;
        });
    }

    public void ExitBtn()
    {
        Application.Quit();
    }
    
    public void SettingXBtn()
    {
        _settingGroup.DOFade(0, 0.1f).OnComplete(() =>
        {
            _settingGroup.blocksRaycasts = false;
        });
    }
    
    public void ActiveXBtn()
    {
        _activeGroup.DOFade(0, 0.1f).OnComplete(() =>
        {
            _activeGroup.blocksRaycasts = false;
        });
    }
    
    public void PassiveXBtn()
    {
        _passivePanel.SetActive(false);
    }
    
    public void RightBtn() // 패시프 판넬 키기
    {
        _passivePanel.SetActive(true);
        _activePanel.SetActive(false);
    }

    public void PsRightBtn() // 액티브 판넬 키기
    {
        _activePanel.SetActive(true);
        _passivePanel.SetActive(false);
    }
}