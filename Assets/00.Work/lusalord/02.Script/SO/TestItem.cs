using _00.Work.lusalord._02.Script.ItemType;
using Unity.VisualScripting;
using UnityEngine;

public class TestItem : ItemTypeSpin
{
    protected override void Start()
    {
        base.Start();
        Debug.Log(spinSpeed);
    }
}
