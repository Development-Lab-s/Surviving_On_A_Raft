using System;
using System.Collections;
using _00.Work.Nugusaeyo._Script.Cost;
using _00.Work.Nugusaeyo._Script.Enemy;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CostBoarder : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] costText;
    [SerializeField] private TextMeshProUGUI[] costName;
    [SerializeField] private CostInformationSO[] costInformation;
    [SerializeField] private Image[] costImg;
    
    private Coroutine[] _coroutines = new Coroutine[5];
    private int[] costUi = new int[5];

    public static CostBoarder Instance;

    public Transform costGoalPos;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        for (int i = 0; i < costName.Length; i++)
        {
            costName[i].text = costInformation[i].costName + " : ";
            costText[i].text = "0";
            costUi[i] = 0;
        }
    }

    private IEnumerator CostUp(int i)
    {
        WaitForSeconds delayTime = new  WaitForSeconds(0.05f);
        while (CostManager.Instance.Costs[i] > int.Parse(costText[i].text))
        {
            yield return delayTime;
            costUi[i]++;
            costText[i].text = costUi[i].ToString();
        }
    }

    private void Start()
    {
        CostManager.Instance.CostUpEvent += CostUpBoard;
        CostManager.Instance.CostDownEvent += CostDownBoard;
        CostUpBoard();
        ResetCostImg();
    }

    private void OnDestroy()
    {
        CostManager.Instance.CostUpEvent -= CostUpBoard;
        CostManager.Instance.CostDownEvent -= CostDownBoard;
    }

    private void CostUpBoard()
    {
        for (int i = 0; i < costText.Length; i++)
        {
            if (CostManager.Instance.Costs[i] != int.Parse(costText[i].text))
            {
                if (_coroutines[i] != null)
                {
                    StopCoroutine(_coroutines[i]);
                }
                _coroutines[i] = StartCoroutine(CostUp(i));
            }
            
        }
    }

    private void CostDownBoard()
    {
        StopAllCoroutines();
        for (int i = 0; i < costText.Length; i++)
        {
            int currentCost = CostManager.Instance.Costs[i];
            costText[i].text = currentCost.ToString();
            costUi[i] = currentCost;
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
