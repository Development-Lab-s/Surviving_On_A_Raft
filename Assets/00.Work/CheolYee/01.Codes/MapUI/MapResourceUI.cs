using _00.Work.Nugusaeyo._Script.SO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _00.Work.CheolYee._01.Codes.MapUI
{
    public class MapResourceUI : MonoBehaviour
    {
        [Header("Map Resource UI")]
        [SerializeField] private Transform requirementParent;  //부모
        [SerializeField] private GameObject requirementPrefab; //프리팹

        public void ShowRequirements(MapDataSo mapData)
        {
            //기존 UI 제거
            foreach (Transform child in requirementParent)
                Destroy(child.gameObject);

            //필요한 재료 UI 생성  
            foreach (var req in mapData.resourceDatas)
            {
                var go = Instantiate(requirementPrefab, requirementParent);

                var icon = go.transform.Find("Icon").GetComponent<Image>();
                var text = go.transform.Find("Name").GetComponent<TextMeshProUGUI>();
                var amount = go.transform.Find("Amount").GetComponent<TextMeshProUGUI>();

                icon.sprite = req.resourceData.sprite;
                text.text = $"{req.resourceData.costName}";
                amount.text = $"{req.resourceAmount}";
            }
        }
    }
}