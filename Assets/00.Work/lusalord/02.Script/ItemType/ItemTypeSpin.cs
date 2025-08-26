using _00.Work.lusalord._02.Script.SO.AttackItem.ItemType;
using UnityEngine;

namespace _00.Work.lusalord._02.Script.ItemType
{
    public abstract class ItemTypeSpin : AttackItem
    {
        private SpinItemSo _spinItemSo;
        protected float SpinSpeed => _spinItemSo.spinSpeed;
        [SerializeField] private Transform _playerTrs;
        public float _angle = 0;
        private Vector3 _startDir;
        private float _radius = 0;
        
        protected virtual void Awake()
        {
            _spinItemSo = (SpinItemSo)attackItemSo;
            _radius = _spinItemSo.spinRadius;
            Spawn();
        }
        public GameObject Spawn()
        {
            GameObject spawnItem = Instantiate(_spinItemSo.spinPrefab, transform);
            spawnItem.transform.position = transform.position = new Vector3(
                _radius * Mathf.Cos(_angle) , 
                _radius* Mathf.Sin(_angle) , 
                0);
            return spawnItem;
        }

        private void Update()
        {
            _angle += SpinSpeed * Time.deltaTime;

            foreach (Transform child in transform)
            {
                child.localPosition = new Vector3(
                    _radius * Mathf.Cos(_angle),
                    _radius * Mathf.Sin(_angle),
                    0);
            }
            transform.position = _playerTrs.position;
        }
    }
}
