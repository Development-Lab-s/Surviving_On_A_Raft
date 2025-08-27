using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CostBoarder : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] costTexts;
    [SerializeField] private Image[] costSprites;
    [SerializeField] private CostInformationSO[] costInformationSO;

    private void Start()
    {
        CostManager.instance.CostUpEvent += HandleCostUpEvent;
        for (int i = 0; i < costTexts.Length; i++)
        {
            costSprites[i].sprite = costInformationSO[i].costImg;
        }
        ResetBoard();
    }

    private void HandleCostUpEvent()
    {
        ResetBoard();
    }

    private void OnDestroy()
    {
        CostManager.instance.CostUpEvent -= HandleCostUpEvent;
    }

    public void ResetBoard()
    {
        for (int i = 0; i < costTexts.Length; i++)
        {
            costTexts[i].text = $"{costInformationSO[i].costName} : {CostManager.instance.Costs[i]}";
        }
    }
}
