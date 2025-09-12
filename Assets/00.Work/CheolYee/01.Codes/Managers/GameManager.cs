using _00.Work.Resource.Manager;
using DG.Tweening;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Managers
{
    public class GameManager : MonoSingleton<GameManager>
    {
        public Transform playerTransform;
        protected override void Awake()
        {
            base.Awake();
            DOTween.SetTweensCapacity(1000, 200);
        }
    }
}