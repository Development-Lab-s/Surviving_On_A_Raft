using _00.Work.Resource.Manager;
using Unity.Cinemachine;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Managers
{
    public class CameraManager : MonoSingleton<CameraManager>
    {
        [SerializeField] private CinemachineCamera cinemachineCamera;
        [SerializeField] private CinemachineConfiner2D confiner;

        public void SetFollowTarget(Transform target)
        {
            cinemachineCamera.Follow = target;
        }

        public void SetCameraBounds(Collider2D bounds)
        {
            confiner.BoundingShape2D = bounds;
            confiner.InvalidateLensCache();
        }
    }
}