using UnityEngine;

namespace _00.Work.lusalord._02.Script.SO
{
    [CreateAssetMenu(fileName = "PassiveItem", menuName = "SO/ItemType/PassiveItem")]
    public class PassiveItem : ItemSO
    {
        [SerializeField] private StatModifier[] _modifiers; // 여러 능력치 적용 가능
        public StatModifier[] Modifiers => _modifiers;

        public override ItemType Type => ItemType.Passive;

        // 플레이어에게 능력치 적용
        // public void ApplyEffect(Player player)
        // {
        //     foreach (var modifier in _modifiers)
        //     {
        //         float finalValue = modifier.value * Level; // 레벨 배율 적용
        //         switch (modifier.stateType)
        //         {
        //             case StateType.Atk: player.Attack += finalValue; break;
        //             case StateType.AtkSpeed: player.AttackSpeed += finalValue; break;
        //             case StateType.Hp: player.Health += finalValue; break;
        //             case StateType.Speed: player.Speed += finalValue; break;
        //         }
        //     }
        // }

        // //아이템 제거 시 효과 반영
        // public void RemoveEffect(Player player)
        // {
        //     foreach (var modifier in _modifiers)
        //     {
        //         float finalValue = modifier.value * Level;
        //         switch (modifier.stateType)
        //         {
        //             case StateType.Atk: player.Attack -= finalValue; break;
        //             case StateType.AtkSpeed: player.AttackSpeed -= finalValue; break;
        //             case StateType.Hp: player.Health -= finalValue; break;
        //             case StateType.Speed: player.Speed -= finalValue; break;
        //         }
        //     }
        // }
    }
}
