using UnityEngine;

namespace _00.Work.Resource.Manager
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindFirstObjectByType<T>();
                    if (_instance == null)
                    {
                        GameObject singleton = new GameObject(typeof(T).Name);
                        _instance = singleton.AddComponent<T>();
                    }
                }
                
                return _instance;
            }
            
        }

        protected virtual void Awake()
        {
            T[] managers = FindObjectsByType<T>(FindObjectsSortMode.None);

            if (managers.Length > 1)
                Destroy(gameObject);
        }

        protected void OnDestroy()
        {
            if (_instance == this)
                _instance = null;
        }
    }
}
