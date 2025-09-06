using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BossHealthTesr : MonoBehaviour
{
    [SerializeField] private Image fill; // 위의 Fill(Image)
    [SerializeField] private int maxHp = 1000;
    [SerializeField] private int curHp = 1000;
    [SerializeField] private int playerAtk = 25;

    void Start()
    {
        curHp = maxHp;
        UpdateBar();
    }

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.nKey.wasPressedThisFrame)
        {
            curHp = Mathf.Max(0, curHp - playerAtk);
            UpdateBar();
        }
    }

    void UpdateBar()
    {
        fill.fillAmount = (float)curHp / maxHp; // 오른쪽→왼쪽으로 매끄럽게 깎임
    }
}

