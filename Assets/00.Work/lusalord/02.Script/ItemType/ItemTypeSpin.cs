using System.Collections.Generic;
using _00.Work.lusalord._02.Script.SO.AttackItem.ItemType;
using UnityEngine;

namespace _00.Work.lusalord._02.Script.ItemType
{
    public abstract class ItemTypeSpin : AttackItem
    {
        private SpinItemSo _spinItemSo;
        protected float SpinSpeed => _spinItemSo.spinSpeed;
        [SerializeField] private Transform playerTrs;
        private float _angle = 0;
        private Vector3 _startDir;
        private float _radius = 0;
        private List<float> _childOffsets = new List<float>();
        protected List<GameObject> objects = new List<GameObject>();
        
        protected virtual void Awake()
        {
            _spinItemSo = (SpinItemSo)attackItemSo;
            _radius = _spinItemSo.spinRadius;
            
            gameObject.name = _spinItemSo.itemName;
            for (int i = 1; i < _spinItemSo.spinAmount + 1; i++)
            {
                float angle = i * (2f * Mathf.PI / _spinItemSo.spinAmount);
                _childOffsets.Add(angle);
                Spawn(angle);
            }
        }
        public GameObject Spawn(float angle)
        {
            GameObject spawnItem = Instantiate(_spinItemSo.spinPrefab, transform);
            objects.Add(spawnItem);
            spawnItem.SetActive(false);
            spawnItem.transform.position = transform.position = new Vector3(
                _radius * Mathf.Cos(_angle),
                _radius* Mathf.Sin(_angle),
                0);
            return spawnItem;
        }

        private void Update()
        {
            _angle += SpinSpeed * Time.deltaTime;

            for (int i = 0; i < _childOffsets.Count; i++)
            {
                float childAngle = _childOffsets[i] + _angle;
                transform.GetChild(i).localPosition = new Vector3(
                    _radius * Mathf.Cos(childAngle), 
                    _radius * Mathf.Sin(childAngle), 
                    0);
            }
            transform.position = playerTrs.position;
        }
    }
}
