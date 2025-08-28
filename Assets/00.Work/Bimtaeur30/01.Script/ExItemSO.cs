using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ExItemSO", menuName = "SO/ExItemSO")]
public class ExItemSO : ScriptableObject
{
    public Sprite ItemImage;
    public string ItemName;
    public string ItemDescription;
}
