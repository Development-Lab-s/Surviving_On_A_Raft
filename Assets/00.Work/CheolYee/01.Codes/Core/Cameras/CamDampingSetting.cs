using System.Collections;
using _00.Work.CheolYee._01.Codes.Managers;
using _00.Work.Resource.Manager;
using Unity.Cinemachine;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Core.Cameras
{
    public class CamDampingSetting : MonoSingleton<CamDampingSetting>
    {
        [SerializeField] private CinemachineCamera vcam;
        [SerializeField] private Transform player;

        private Vector3 _lastPlayerPos;

        void Start()
        {
            _lastPlayerPos = GameManager.Instance.playerTransform.position;
        }

        public void WarpPlayer(Vector3 newPos)
        {
            Vector3 offset = newPos - player.position;

            // 플레이어 이동
            player.position = newPos;

            // 카메라 순간 이동
            vcam.OnTargetObjectWarped(player, offset);

            _lastPlayerPos = newPos;
        }
    }
}