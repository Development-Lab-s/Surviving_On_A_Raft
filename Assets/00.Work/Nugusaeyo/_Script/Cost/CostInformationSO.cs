using UnityEngine;

namespace _00.Work.Nugusaeyo._Script.Enemy
{
    [CreateAssetMenu(fileName = "New CostSO", menuName = "SO/Cost", order = 0)]
    public class CostInformationSO : ScriptableObject
    {
        public string name;
        public Sprite sprite;
    }
}