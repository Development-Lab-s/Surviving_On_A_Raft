using _00.Work.CheolYee._01.Codes.Managers;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Core.Cameras
{
    [RequireComponent(typeof(Collider2D))]
    public class CameraBounds : MonoBehaviour
    {
        private Collider2D _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
            _collider.isTrigger = true; // 플레이어 감지용
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                CameraManager.Instance.SetCameraBounds(_collider);
            }
        }
    }
}