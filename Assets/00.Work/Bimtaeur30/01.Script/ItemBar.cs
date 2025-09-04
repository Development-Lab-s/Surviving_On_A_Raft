using UnityEngine;

public class ItemBar : MonoBehaviour
{
    public ExItemSO MyItem;

    public void CreateBtnClick()
    {
        ItemCreateManager.Instance.CreateItem(MyItem);
    }
}
