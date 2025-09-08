using UnityEngine;

public class PlayerMapTracker : MonoBehaviour
{
    public MapArea CurrentMap { get; private set; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        MapArea map = other.GetComponent<MapArea>();
        if (map != null)
        {
            CurrentMap = map;
            Debug.Log($"플레이어가 {map.mapName} 에 입장");
            SpawnManager.Instance.SpawnPortalInMap(map);
        }
    }
}
