using UnityEngine;

public class TitleFloat : MonoBehaviour
{
    [SerializeField] float floatSpeed = 2f;
    [SerializeField] float floatHeight = 20f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        float newY = Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.localPosition = startPos + new Vector3(0, newY, 0);
    }
}