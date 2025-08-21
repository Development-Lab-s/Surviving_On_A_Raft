using System;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Agent
{
    public enum DamageType
    {
        None,
        SingleTarget,
        RangedTarget
    }
    public class AgentDamageCast : MonoBehaviour
    {
        public ContactFilter2D targetFilter;
        public float damageRadius;
        public DamageType damageType;

        private Collider2D[] _resultArray;

        private void Awake()
        {
            switch (damageType)
            {
                case DamageType.None:
                    break;
                case DamageType.SingleTarget:
                    _resultArray = new Collider2D[1];
                    break;
                case DamageType.RangedTarget:
                    _resultArray = new Collider2D[10];
                    break;
            }
        }
    }
}