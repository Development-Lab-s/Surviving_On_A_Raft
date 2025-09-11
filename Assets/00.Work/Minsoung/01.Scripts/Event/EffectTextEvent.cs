using UnityEngine;

public class EffectTextEvent : MonoBehaviour
{
    [SerializeField] private string effectText;

    public string EffectText()
    {
        return effectText;
    }
}
