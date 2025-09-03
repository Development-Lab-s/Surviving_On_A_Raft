using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Ingredient
{
    public string Name;   // ��� �̸�
    public int Amount;    // �ʿ� ����
}
[CreateAssetMenu(fileName = "ExItemSO", menuName = "SO/ExItemSO")]
public class ExItemSO : ScriptableObject
{
    public Sprite ItemImage;
    public string ItemName;
    public string ItemDescription;
    public string[] itemAttributes;
    public List<Ingredient> ItemIgdt = new List<Ingredient>(); // ũ�� 3¥�� �迭
}
