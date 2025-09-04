using System.Collections;
using Febucci.UI;
using UnityEngine;

public class Test : MonoBehaviour
{
    public TypewriterByCharacter test;

    private void Start()
    {
        test.ShowText("adsfasdfasdfasdfasdfaadsf");
    }

    public void OnTypingComplete() 
    {
        StartCoroutine(WaitAndDisappear());
    }

    IEnumerator WaitAndDisappear()
    {
        yield return new WaitForSeconds(0f); 
        test.StartDisappearingText();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            test.ShowText("adsfasdfasdfasdfasdfaadsf");
        }
    }

}
