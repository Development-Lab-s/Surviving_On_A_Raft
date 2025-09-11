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
            SpawnManager.Instance.SpawnPortalInMap(map);
        }
    }
}
