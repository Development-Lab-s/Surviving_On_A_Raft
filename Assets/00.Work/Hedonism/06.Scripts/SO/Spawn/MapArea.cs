using System.Collections.Generic;
using _00.Work.Hedonism._06.Scripts.SO.Manager;
using UnityEngine;

public class MapArea : MonoBehaviour
{
    [Header("이 맵의 이름")]
    public int mapIndex;

    [Header("포탈이 소환될 수 있는 위치들")]
    public List<Transform> portalSpawnPoints = new();

    // 플레이어가 맵에 들어올 때 감지
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 플레이어가 들어오면 해당 맵에 포탈 생성
            SpawnManager.Instance?.SpawnPortalInMap(this);
        }
    }

    private bool _isQuitting;

    private void OnApplicationQuit()
    {
        _isQuitting = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (_isQuitting) return; // 종료 중 이벤트 무시

        if (other.CompareTag("Player"))
        {
            SpawnManager.Instance?.DespawnCurrentPortals();
        }
    }
}
