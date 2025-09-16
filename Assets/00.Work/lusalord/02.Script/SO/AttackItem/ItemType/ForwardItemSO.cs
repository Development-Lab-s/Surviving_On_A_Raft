using _00.Work.lusalord._02.Script;
using _00.Work.lusalord._02.Script.SO.AttackItem;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "ForwardItemSO", menuName = "SO/Item/ForwardItemSO")]
public class ForwardItemSO : AttackItemSo
{
    public float coolTime;
    protected override void OnValidate()
    {
        
    }
}
