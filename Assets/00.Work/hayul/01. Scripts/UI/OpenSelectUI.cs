using System;
using Unity.VisualScripting;
using UnityEngine;

public class OpenSelectUI : MonoBehaviour
{
    public Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _animator.SetTrigger("Open");
        }
    }
}
