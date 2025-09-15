using _00.Work.CheolYee._01.Codes.Players;
using _00.Work.CheolYee._01.Codes.SO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _00.Work.CheolYee._01.Codes.Skills
{
    public class SkillUI : MonoBehaviour
    {
        [SerializeField] private Image skillImage;
        [SerializeField] private Image coolTimeImage;
        [SerializeField] private TextMeshProUGUI coolTimeText;

        private float _timer;
        private float _coolTime;
        private bool _isCooldown;
        
        private PlayerInputSo _playerInput;
        private Player _player;
        
        public void Initialize(Sprite skillIcon, float coolTime, Player player)
        {
            skillImage.sprite = skillIcon;
            coolTimeImage.fillAmount = 0f;
            coolTimeText.text = "";
            _timer = coolTime;
            _coolTime = coolTime;
            _playerInput = player.PlayerInput;
            _player = player;
            _playerInput.OnQKeyPress += UseSkill;
        }

        private void OnDisable()
        {
            _playerInput.OnQKeyPress -= UseSkill;
        }

        private void Update()
        {
            if (!_isCooldown) return;
            
            _timer -= Time.deltaTime;
            coolTimeImage.fillAmount = _timer / _coolTime;
            coolTimeText.text = _timer.ToString("0.0");

            if (_timer <= 0)
            {
                _isCooldown = false;
                _timer = 0f;
                coolTimeImage.fillAmount = 0f;
                coolTimeText.text = "";
            }
        }

        public void UseSkill()
        {
            if (_isCooldown) return;

            _player.GetComponentInChildren<SkillBase>().TryUseSkill();
            _timer = _coolTime;
            _isCooldown = true;
            coolTimeImage.fillAmount = 1;
        }
    }
}