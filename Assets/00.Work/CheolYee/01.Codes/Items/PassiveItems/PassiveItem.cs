using _00.Work.CheolYee._01.Codes.Players;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Items.PassiveItems
{
    public class PassiveItem : MonoBehaviour
    {
        [field: SerializeField] public PassiveItemSo PassiveItemSo { get; private set; }

        protected Player Player;
        private void Awake()
        {
            Player = GetComponentInParent<Player>();
            gameObject.SetActive(false);
        }

        public void PassiveItemLevelUp(int id)
        {
            if (id == PassiveItemSo.id)
            {
               PassiveItemSo = PassiveItemSo.TryPassiveItemLevelUp();
            }
        }

        //오버라이딩하세요
        public virtual void ApplyBuff() {}
        public virtual void CancelBuff() {}
    }
}