using _00.Work.Resource.Manager;

namespace _00.Work.lusalord._02.Script.Item
{
    public class TestForwardItem : ItemType.ItemTypeForward
    {
        public override void AnimateForwardAttack()
        {
            base.AnimateForwardAttack();
            SoundManager.Instance.PlaySfx("HAMMER");
        }
    }
}
