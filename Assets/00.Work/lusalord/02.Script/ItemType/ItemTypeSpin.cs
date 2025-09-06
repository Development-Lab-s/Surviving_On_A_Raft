using System.Collections.Generic;
using _00.Work.lusalord._02.Script.SO.AttackItem.ItemType;
using UnityEngine;

namespace _00.Work.lusalord._02.Script.ItemType
{
    public abstract class ItemTypeSpin : AttackItem
    {
        private SpinItemSo _spinItemSo;
        [SerializeField] private Transform playerTrs;
        private float _angle;
        private Vector3 _startDir;
        private float _radius;
        private readonly List<float> _childOffsets = new();
        public List<GameObject> objects = new();
        private float _time;
        private int _flip;
        
        
        protected virtual void Awake()
        {
            _spinItemSo = (SpinItemSo)attackItemSo;
            _radius = _spinItemSo.spinRadius;
            
            gameObject.name = _spinItemSo.itemName;
            for (int i = 1; i < _spinItemSo.spinAmount + 1; i++)
            {
                float angle = i * (2f * Mathf.PI / _spinItemSo.spinAmount);
                _childOffsets.Add(angle);
                Spawn();
            }

            if (_spinItemSo.flip)
            {
                _flip = 180;
            }
        }

        private GameObject Spawn()
        {
            GameObject spawnItem = Instantiate(_spinItemSo.spinPrefab, transform);
            objects.Add(spawnItem);
            spawnItem.transform.position = transform.position = new Vector3(
                _radius * Mathf.Cos(_angle),
                _radius* Mathf.Sin(_angle),
                0);
            return spawnItem;
        }

        
        private void Update()
        {
            _time += Time.deltaTime;
            _angle += _spinItemSo.spinSpeed * Time.deltaTime;
            

            for (int i = 0; i < _childOffsets.Count; i++)
            {
                float childAngle = _childOffsets[i] + _angle;
                Transform child = transform.GetChild(i);

                transform.GetChild(i).localPosition = new Vector3(
                    _radius * Mathf.Cos(childAngle), 
                    _radius * Mathf.Sin(childAngle), 
                    0);

                if (_spinItemSo.isRotate)
                {
                    Vector3 dir = playerTrs.position - child.position;
                    float angleToPlayer = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    child.rotation = Quaternion.AngleAxis(angleToPlayer + (90 + _flip), Vector3.forward);
                }

                if (_spinItemSo.rickRolling)
                {
                    objects[i].transform.Rotate(0, 0, _spinItemSo.rotateSpeed);
                }
            }
            transform.position = playerTrs.position;
        }
    }
}
