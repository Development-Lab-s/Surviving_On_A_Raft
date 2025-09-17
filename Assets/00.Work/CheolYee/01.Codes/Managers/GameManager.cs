using _00.Work.CheolYee._01.Codes.Players;
using _00.Work.Resource.Manager;
using DG.Tweening;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Managers
{
    public class GameManager : MonoSingleton<GameManager>
    {
        public Transform playerTransform;
        public Player player;
        public bool isThunami;
        
        public int currentLevel = 1;
        public int maxLevel = 1;
        protected override void Awake()
        {
            base.Awake();
            DOTween.SetTweensCapacity(1000, 200);
        }
    }
}