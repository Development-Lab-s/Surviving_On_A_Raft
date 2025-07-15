using UnityEngine;

namespace _00.Work.Resource.SO
{
    public interface IPoolable
    {
        public string ItemName { get; }
        public GameObject GameObject { get; }
        public void ResetItem();
    }
}