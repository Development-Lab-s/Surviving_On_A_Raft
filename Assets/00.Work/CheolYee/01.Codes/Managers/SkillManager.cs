using System;
using _00.Work.CheolYee._01.Codes.Players;
using _00.Work.CheolYee._01.Codes.SO;
using _00.Work.Resource.Manager;
using UnityEngine;
using UnityEngine.Serialization;

namespace _00.Work.CheolYee._01.Codes.Managers
{
    public class SkillManager : MonoSingleton<SkillManager>
    {
        [SerializeField] private Player player;
        [SerializeField] private GameObject skillUIPrefab;
        [SerializeField] private Transform uiRoot;
        
        private PlayerSkillDataSo _currentSkillData;

        protected override void Awake()
        {
            base.Awake();
            _currentSkillData = player.CharacterData.playerSkillData;
        }

        private void Start()
        {
            Instantiate(skillUIPrefab, uiRoot);
        }
    }
}