using _00.Work.Bimtaeur30._01.Script;
using UnityEngine;

public class ItemBar : MonoBehaviour
{
    public ExItemSO MyItem;
    [SerializeField] private GameObject CreateBtn;
    [SerializeField] private GameObject UpgradeBtn;
    [SerializeField] private GameObject MaxUpgradeBtn;
    [SerializeField] private GameObject CreateBtn_Disabled;


    public void CreateBtnClick()
    {
        ItemCreateManager.Instance.CreateItem(MyItem);
    }

    public void SetStateCreate()
    {
        CreateBtn.SetActive(true);
        CreateBtn_Disabled.SetActive(false);
        UpgradeBtn.SetActive(false);
        MaxUpgradeBtn.SetActive(false);
    }
    public void SetStateCreateDisabled()
    {
        CreateBtn.SetActive(false);
        CreateBtn_Disabled.SetActive(true);
        UpgradeBtn.SetActive(false);
        MaxUpgradeBtn.SetActive(false);
    }

    public void SetStateUpgrade()
    {
        CreateBtn.SetActive(false);
        CreateBtn_Disabled.SetActive(false);
        UpgradeBtn.SetActive(true);
        MaxUpgradeBtn.SetActive(false);
    }

    public void SetStateMaxUpgrade()
    {
        CreateBtn.SetActive(false);
        CreateBtn_Disabled.SetActive(false);
        UpgradeBtn.SetActive(false);
        MaxUpgradeBtn.SetActive(true);
    }
}
