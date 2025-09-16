using _00.Work.Resource.Manager;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Skills
{
    public class StartSelect : MonoBehaviour
    {
        public void FadeInToSelect() => FadeManager.Instance.FadeToScene(1);
        public void QuitBtn() => Application.Quit();
    }
}