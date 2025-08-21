using _00.Work.lusalord._02.Script.ItemType;
using Unity.VisualScripting;
using UnityEngine;

public class TestItem : ItemTypeSpin
{
    protected override void Awake()
    {
        base.Awake();
        Debug.Log(spinSpeed);
    }
}
