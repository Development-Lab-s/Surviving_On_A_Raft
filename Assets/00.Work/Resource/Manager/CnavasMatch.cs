using UnityEngine;
using UnityEngine.UI;

namespace _00.Work.Resource.Manager
{
    public class CanvasMatch : MonoBehaviour
    {
        private CanvasScaler _canvasScaler;

        
        //Default 해상도 비율
        private readonly float _fixedAspectRatio = 16f / 9f; 

        //현재 해상도의 비율
        private readonly float _currentAspectRatio = Screen.width / (float)Screen.height;

        private void Awake()
        {
            _canvasScaler = GetComponent<CanvasScaler>();
        }

        void Start()
        {
            //현재 해상도 가로 비율이 더 길 경우
            if (_currentAspectRatio > _fixedAspectRatio) _canvasScaler.matchWidthOrHeight = 1;       
            //현재 해상도의 세로 비율이 더 길 경우
            else if (_currentAspectRatio < _fixedAspectRatio) _canvasScaler.matchWidthOrHeight = 0;
        }
    }
}
