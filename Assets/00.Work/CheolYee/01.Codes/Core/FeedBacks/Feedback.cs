using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Core.FeedBacks
{
    public abstract class Feedback : MonoBehaviour
    {
        public abstract void CreateFeedback();
    
        public abstract void FinishFeedback();

        public void OnDisable()
        {
            FinishFeedback();
        }
    }
}