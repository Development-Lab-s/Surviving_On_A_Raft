using System.Collections.Generic;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Core.FeedBacks
{
    public class FeedbackSystem : MonoBehaviour
    {
        [SerializeField] private List<Feedback> feedBacks;

        public void PlayFeedback()
        {
            feedBacks.ForEach(feedBack => feedBack.FinishFeedback());
            feedBacks.ForEach(feedBack => feedBack.CreateFeedback());
        }
    }
}