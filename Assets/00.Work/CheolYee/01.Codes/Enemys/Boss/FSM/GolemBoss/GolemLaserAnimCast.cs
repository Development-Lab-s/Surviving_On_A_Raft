using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Enemys.Boss.FSM.GolemBoss
{
    public class GolemLaserAnimCast : MonoBehaviour
    {
        [SerializeField] private GolemLaser laser;
        
        public void LaserAnimAndCast()
        {
            laser.LaserDamageCast();
        }
    }
}