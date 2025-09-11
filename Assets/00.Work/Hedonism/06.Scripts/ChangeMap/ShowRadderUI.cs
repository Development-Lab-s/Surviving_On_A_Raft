using UnityEngine;

public class ShowRadderUI : MonoBehaviour
{
    [SerializeField] private Transform UItrm;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vector3 CanvasPos = Camera.main.WorldToScreenPoint(UItrm.position);
            ShowStageSelectUI.Instance.ShowMaps(CanvasPos);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ShowStageSelectUI.Instance.CloseMapUI();
        }
    }
}
