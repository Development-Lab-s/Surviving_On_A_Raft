using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnListSO", menuName = "Scriptable Objects/SpawnListSO")]
public class SpawnListSO : ScriptableObject
{
    public List<Transform> SpawnTrm;
}
