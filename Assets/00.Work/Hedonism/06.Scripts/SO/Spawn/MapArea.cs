using System.Collections.Generic;
using UnityEngine;

public class MapArea : MonoBehaviour
{
    [Header("이 맵의 이름")]
    public string mapName;

    [Header("포탈이 소환될 수 있는 위치들")]
    public List<Transform> portalSpawnPoints = new();

    // 플레이어가 맵에 들어올 때 감지
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"{mapName} 입장!");
            // 플레이어가 들어오면 해당 맵에 포탈 생성
            SpawnManager.Instance.SpawnPortalInMap(this);
        }
    }

    private void OTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"{mapName} 퇴장!");

            SpawnManager.Instance.DespawnCurrentPortals();
        }
    }
}
