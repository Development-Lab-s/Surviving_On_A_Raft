using UnityEngine;

public class EventOneMinsung : MinsungChuSang, IEventMinsung
{
    protected override void Start()
    {
        base.Start();
        Debug.Log("마 릴ㄹ 소다빱");
    }

    public void StartEvent()
    {
        Debug.Log("마이 리를 소다팝");
        Debug.Log(ID);
    }
}
