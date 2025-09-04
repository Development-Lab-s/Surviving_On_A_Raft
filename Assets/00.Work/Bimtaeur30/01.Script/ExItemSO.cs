using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Ingredient
{
    public string Name;   // 재료 이름
    public int Amount;    // 필요 수량
}
[CreateAssetMenu(fileName = "ExItemSO", menuName = "SO/ExItemSO")]
public class ExItemSO : ScriptableObject
{
    public Sprite ItemImage;
    public string ItemName;
    public string ItemDescription;
    public string[] itemAttributes;
    public List<Ingredient> ItemIgdt = new List<Ingredient>(); // 크기 3짜리 배열
}
