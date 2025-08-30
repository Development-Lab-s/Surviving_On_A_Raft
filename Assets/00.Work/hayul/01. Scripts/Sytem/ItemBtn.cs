using UnityEngine;
using UnityEngine.UI;

public class ItemBtn : MonoBehaviour
{
    [Header("아이템 데이터")]
    public ItemSO itemData;

    [Header("참조")]
    public Button button;
    public InfoPanel infoPanel;

    private void Awake()
    {
        if (button == null)
            button = GetComponent<Button>();
    }

    private void Start()
    {
        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        if (infoPanel != null && itemData != null)
        {
            infoPanel.SetItemInfo(itemData);
        }
        else
        {
            Debug.LogWarning($"{name} 버튼에 InfoPanel 또는 ItemSO가 연결되지 않았습니다.");
        }
    }
}