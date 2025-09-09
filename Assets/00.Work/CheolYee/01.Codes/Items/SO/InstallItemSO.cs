using _00.Work.CheolYee._01.Codes.Projectiles;
using _00.Work.lusalord._02.Script.SO.AttackItem;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Items.SO
{
    [CreateAssetMenu(fileName = "InstallItemSO", menuName = "SO/Item/InstallItemSO")]
    public class InstallItemSo : AttackItemSo
    {
        [Header("Install Item")]
        public float speed;
        public float cooldown;
        public int grenadeCount;
        public GameObject installItem;

        protected override void OnValidate()
        {
            if (installItem != null)
            {
                if (installItem.TryGetComponent(out Projectile projectile))
                {
                    itemName = projectile.ItemName;
                }
                else
                {
                    installItem = null;
                    Debug.Log("얘는 발사체가 아닙니다.");
                }
            }
        }
    }
}
