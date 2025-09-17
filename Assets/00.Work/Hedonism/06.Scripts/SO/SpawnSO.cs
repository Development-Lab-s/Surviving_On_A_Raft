using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnSO", menuName = "SO/SpawnPoints")]
public class SpawnSO : ScriptableObject
{
    [System.Serializable]
    public class SpawnPointData
    {
        public int id;            // 고유 번호
        public Vector2 position;  // 좌표
    }

    public List<SpawnPointData> spawnPoints = new List<SpawnPointData>();
}
