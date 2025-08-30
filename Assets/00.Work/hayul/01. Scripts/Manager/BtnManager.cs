using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnManager : MonoBehaviour
{
    [SerializeField] private GameObject _activePanel;
    [SerializeField] private GameObject _passivePanel;
    [SerializeField] private GameObject _settingPanel;
    public void StartBtn()
    {
        SceneManager.LoadScene(1);
    }

    public void GuideBtn()
    {
        _activePanel.SetActive(true);
    }

    public void SettingBtn()
    {
        _settingPanel.SetActive(true);
    }

    public void ExitBtn()
    {
        Application.Quit();
    }
    
    public void SettingXBtn()
    {
        _settingPanel.SetActive(false);
    }
    
    public void ActiveXBtn()
    {
        _activePanel.SetActive(false);
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