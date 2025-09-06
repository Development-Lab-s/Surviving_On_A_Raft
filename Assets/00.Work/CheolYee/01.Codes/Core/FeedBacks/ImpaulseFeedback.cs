using Unity.Cinemachine;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Core.FeedBacks
{
    public class ImpulseFeedback : Feedback
    {
        [SerializeField] private CinemachineImpulseSource cinemachineImpulseSource;
        [SerializeField] private float impulseForce = 0.1f;
        public override void CreateFeedback()
        {
            cinemachineImpulseSource.GenerateImpulse(impulseForce);
        }

        public override void FinishFeedback()
        {
        }
    }
}