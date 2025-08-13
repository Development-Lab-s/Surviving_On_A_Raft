using UnityEngine;

public class PanelOpenEvent : MonoBehaviour
{
    public GameObject characterInfoPanel;

    public void ShowCharacterInfo()
    {
        if (characterInfoPanel != null)
        {
            characterInfoPanel.SetActive((true));
        }
    }
}
