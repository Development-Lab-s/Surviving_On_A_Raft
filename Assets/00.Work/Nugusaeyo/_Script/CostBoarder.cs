using System;
using _00.Work.Nugusaeyo._Script.Enemy;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CostBoarder : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] costText;
    [SerializeField] private CostInformationSO[] costInformation;
    [SerializeField] private Image[] costImg;

    private void Start()
    {
        CostManager.instance.CostUpEvent += ResetCostBoard;
        ResetCostBoard();
        ResetCostImg();
    }

    public void ResetCostBoard()
    {
        for (int i = 0; i < costText.Length; i++)
        {
            costText[i].text = costInformation[i].name + " : " + CostManager.instance.Costs[i];
        }
    }

    private void ResetCostImg()
    {
        for (int i = 0; i < costImg.Length; i++)
        {
            costImg[i].sprite = costInformation[i].sprite;
        }
    }
}
