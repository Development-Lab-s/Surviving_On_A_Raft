using _00.Work.CheolYee._01.Codes.SO;
using _00.Work.Resource.Manager;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Managers
{
    public class GameSelectManager : MonoSingleton<GameSelectManager>
    {
        public CharacterDataSo currentCharacter;
        protected override void Awake()
        {
            base.Awake();
            if (Instance == this)
                DontDestroyOnLoad(this);
        }

    }
}