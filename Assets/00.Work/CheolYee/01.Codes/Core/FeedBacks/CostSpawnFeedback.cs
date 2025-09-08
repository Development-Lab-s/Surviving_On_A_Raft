using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Core.FeedBacks
{
    public class CostSpawnFeedback : Feedback
    {
        [SerializeField] CostSpawner costSpawner;
        public override void CreateFeedback()
        {
            costSpawner.SpawnCost(transform.position);
        }

        public override void FinishFeedback()
        {
        }
    }
}