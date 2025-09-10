using System.Collections.Generic;
using _00.Work.CheolYee._01.Codes.SO;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class ShowResourceUI : MonoBehaviour
{
    [field: SerializeField] public GameObject mapUI { get; private set; }

    [SerializeField] private PlayerInputSo playerInputSo;
    [SerializeField] private Image[] mapImages;
    [SerializeField] private List<Sprite> mapSprites;
    [SerializeField] private List<Image> outlines;
}
