using _00.Work.Resource.Manager;
using UnityEngine;

namespace _00.Work.Nugusaeyo._Script.Tsunami
{
    public class TsunamiEventManager : MonoSingleton<TsunamiEventManager>
    {
        [SerializeField] private TsunamiEvent tsunamiEvent;
        public void LadderInteracted(int currentLevel)
        {
            tsunamiEvent.MiniMapStageUp.SetCurrentLevel(currentLevel);
            tsunamiEvent.MiniMapStageUp.CastleViewUp();
        }
    }
}
