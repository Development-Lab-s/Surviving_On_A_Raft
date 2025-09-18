using _00.Work.CheolYee._01.Codes.Core.Effect;
using _00.Work.Resource.Manager;
using _00.Work.Resource.SO;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Core.FeedBacks
{
    public class EffectPlayerFeedback : Feedback
    {
        [SerializeField] private PoolItem effectItem;
        public override void CreateFeedback()
        {
            EffectPlayerSystem effect = PoolManager.Instance.Pop(effectItem.poolName) as EffectPlayerSystem;
            effect?.SetPosAndPlay(transform.position);
            SoundManager.Instance.PlaySfx("BOOM");
        }

        public override void FinishFeedback()
        {
            
        }
    }
}