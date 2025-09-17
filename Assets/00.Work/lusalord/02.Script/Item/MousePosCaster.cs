using System;
using _00.Work.CheolYee._01.Codes.SO;
using UnityEngine;

namespace _00.Work.lusalord._02.Script.Item
{
    public class MousePosCaster : MonoBehaviour
    {
        [SerializeField] private PlayerInputSo playerInputSo;

        private void Update()
        {
            Vector3 mousePos = playerInputSo.MousePosition;
            mousePos.z = 0;

            Vector3 scale = transform.localScale;

            // 마우스가 오브젝트 기준 오른쪽에 있으면 scale.x 양수, 왼쪽이면 음수
            if (mousePos.x > transform.position.x)
                scale.x = Mathf.Abs(scale.x);   // 오른쪽
            else
                scale.x = -Mathf.Abs(scale.x);  // 왼쪽

            transform.localScale = scale;
        }
    }
}