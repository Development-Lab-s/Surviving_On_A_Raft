using UnityEngine;

public class ItemBar : MonoBehaviour
{
    public ExItemSO MyItem;
    [SerializeField] private GameObject CreateBtn;
    [SerializeField] private GameObject CreateBtn_Disabled;


    public void CreateBtnClick()
    {
        ItemCreateManager.Instance.CreateItem(MyItem);
    }

    public void SetStateCreate()
    {
        CreateBtn.SetActive(true);
        CreateBtn_Disabled.SetActive(false);
    }
    public void SetStateCreateDisabled()
    {
        CreateBtn.SetActive(false);
        CreateBtn_Disabled.SetActive(true);
    }
}
