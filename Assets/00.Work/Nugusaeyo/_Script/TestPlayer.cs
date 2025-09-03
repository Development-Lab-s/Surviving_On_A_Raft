using System;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    public Vector2 MoveDir { get; private set; }

    private void Start()
    {
        MoveDir = Vector2.right;
    }
}
