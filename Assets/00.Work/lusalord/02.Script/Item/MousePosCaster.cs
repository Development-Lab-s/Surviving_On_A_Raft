using UnityEngine;
using _00.Work.CheolYee._01.Codes.SO;

namespace _00.Work.lusalord._02.Script.Item
{
    public class MousePosCaster : MonoBehaviour
    {
        private bool _lockFlip = false;   // 방향 고정 여부
        private float _lockedScaleX;      // 고정된 방향 값
        private Vector3 _mousePosition;

        private void Update()
        {
            if (_lockFlip)
            {
                // 공격 중에는 저장된 방향 유지
                Vector3 scale = transform.localScale;
                scale.x = _lockedScaleX;
                transform.localScale = scale;
                return;
            }

            
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;

            Vector3 newScale = transform.localScale;

            if (mousePos.x > transform.position.x)
                newScale.x = Mathf.Abs(newScale.x);   // 오른쪽
            else
                newScale.x = -Mathf.Abs(newScale.x);  // 왼쪽

            transform.localScale = newScale;
        }

        // 공격 시작 시 호출
        public void LockDirection()
        {
            _lockFlip = true;
            _lockedScaleX = transform.localScale.x;
        }

        // 공격 종료 시 호출
        public void UnlockDirection()
        {
            _lockFlip = false;
        }
    }
}