using UnityEngine;

public class EventTwoMinsung : MinsungChuSang, IEventMinsung
{
    protected override void Start()
    {
        base.Start();
        Debug.Log("민성이 멍청이 리얼끄끄");
        id = 0;
        ID = 7;
        Debug.Log(ID);
    }

    public void StartEvent()
    {
        Debug.Log("난 이미 쌀 다 팜");
    }
}
