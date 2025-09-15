using System.Collections.Generic;
using _00.Work.Nugusaeyo._Script.Enemy;
using UnityEngine;

namespace _00.Work.Nugusaeyo._Script.SO
{
    [CreateAssetMenu(fileName = "NewMapDataSo", menuName = "SO/MapData")]
    public class MapDataSo : ScriptableObject
    {
        public int mapIndex;
        public Sprite mapIcon;
        public List<ResourceData> resourceDatas;
    }

    [System.Serializable]
    public class ResourceData
    {
        public int resourceIndex;
        public CostInformationSO resourceData;
        public int resourceAmount;
    }
}