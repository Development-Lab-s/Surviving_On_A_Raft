using System.Collections;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Core.FeedBacks
{
    public class BlinkFeedback : Feedback
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private float blinkDuration = 0.1f;
        
        private Material _material;
        
        private readonly int _isBlinkHash = Shader.PropertyToID("_IsBlink");

        private void Awake()
        {
            _material = spriteRenderer.material;
        }

        public override void CreateFeedback()
        {
            _material.SetInt(_isBlinkHash, 1); //깜박이게 만들기
            StartCoroutine(DelayBlink());
        }

        private IEnumerator DelayBlink()
        {
            yield return new WaitForSeconds(blinkDuration); //깜박임 시간 지난 후
            _material.SetInt(_isBlinkHash, 0); //원상복구
        }

        public override void FinishFeedback()
        {
            StopAllCoroutines(); //깜빡 끝나면 모든 코루틴 정지
            _material.SetInt(_isBlinkHash, 0); //머테리얼의 정상화
        }
    }
}