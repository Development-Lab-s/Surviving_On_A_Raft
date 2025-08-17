using UnityEngine;
using UnityEngine.Tilemaps;

public class Test : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private TileBase myTile;
    // [SerializeField] private Vector3 tileVec; -47, -24, 0

    void Start()
    {
        tilemap.ClearAllTiles();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            for (int i = 0; i < 15; i++)
            {
                Vector3Int tilePosition = new Vector3Int(-47, -24 + i, 0);
                tilemap.SetTile(tilePosition, myTile);
            }
        }
    }
}
