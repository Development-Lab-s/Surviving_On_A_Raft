using System;
using System.Globalization;
using _00.Work.CheolYee._01.Codes.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _00.Work.CheolYee._01.Codes.Players
{
    public class PlayerHpBar : MonoBehaviour
    {
        [SerializeField] private Image hpBar;
        [SerializeField] private TextMeshProUGUI hpText;

        private Player _player;
        private void Start()
        {
            _player = GameManager.Instance.playerTransform.GetComponent<Player>();
            SetHp();
        }

        private void SetHp()
        {
            hpText.text = _player.HealthComponent.CurrentHealth.ToString("0.0");
            hpBar.fillAmount = _player.HealthComponent.CurrentHealth / _player.HealthComponent.MaxHealth;
        }

        private void Update()
        {
            SetHp();
        }
    }
}