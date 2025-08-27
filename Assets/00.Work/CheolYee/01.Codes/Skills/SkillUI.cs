using UnityEngine;
using UnityEngine.UI;

namespace _00.Work.CheolYee._01.Codes.Skills
{
    public class SkillUI : MonoBehaviour
    {
        [SerializeField] private Image skillImage;
        [SerializeField] private Image collTimeImage;
        [SerializeField] private Text coolTimeText;

        private float _timer;
        
        public void Initialize(Sprite skillIcon, int coolTime)
        {
            skillImage.sprite = skillIcon;
        }
    }
}