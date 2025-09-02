using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoPanel : MonoBehaviour
{
    [Header("UI References")]
    public Image itemImage;         // 이미지
    public TMP_Text itemNameText;   // 이름
    public TMP_Text abilityText;    // 능력
    public TMP_Text[] upgradeTexts; // 1강~5강 정보 텍스트 배열

    [Header("ScrollView Content")]
    public Transform scrollContent;

    private void OnEnable()
    {
        if (scrollContent != null && scrollContent.childCount > 0)
        {
            ItemBtn firstButton = scrollContent.GetChild(0).GetComponent<ItemBtn>();

            if (firstButton != null && firstButton.itemData != null)
            {
                SetItemInfo(firstButton.itemData);
            }
        }
    }

    public void SetItemInfo(ItemSO item)
    {
        if (item == null) return;
        
        itemImage.sprite = item.itemIcon;
        itemNameText.text = item.itemName;
        abilityText.text = item.ability;
        
        for (int i = 0; i < upgradeTexts.Length; i++)
        {
            if (i < item.upgradeInfos.Length && !string.IsNullOrEmpty(item.upgradeInfos[i]))
                upgradeTexts[i].text = $"{i + 1}강: {item.upgradeInfos[i]}";
            else
                upgradeTexts[i].text = $"{i + 1}강: -";
        }
    }
}