using _00.Work.Resource.Manager;
using _00.Work.Resource.SO;
using UnityEngine;

namespace _00.Work.Nugusaeyo._Script.Enemy
{
    public class Costs : MonoBehaviour, IPoolable
    {
        private Rigidbody2D _rigidbody2D;
        [SerializeField] private int costType;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            _rigidbody2D.AddForce(new Vector2(Random.Range(-100, 100), Random.Range(125, 200)));
        }
        
        public string ItemName => "ItemCost";
        public GameObject GameObject => gameObject;
        public void ResetItem()
        {
            _rigidbody2D.linearVelocity = Vector2.zero;
            gameObject.transform.rotation = Quaternion.Euler(Vector3.zero);
            
        }

    }
}
