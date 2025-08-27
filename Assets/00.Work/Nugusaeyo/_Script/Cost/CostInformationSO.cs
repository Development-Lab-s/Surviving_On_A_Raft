using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New CostInformationSO", menuName = "SO/Cost/CostInformationSO", order = 0)]
public class CostInformationSO : ScriptableObject
{
    public Sprite costImg;
    public string costName;
}