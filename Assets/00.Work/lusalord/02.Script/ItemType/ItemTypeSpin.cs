using System.Collections.Generic;
using _00.Work.lusalord._02.Script.Item;
using _00.Work.lusalord._02.Script.SO.AttackItem.ItemType;
using UnityEngine;

namespace _00.Work.lusalord._02.Script.ItemType
{
    public abstract class ItemTypeSpin : AttackItem
    {
        protected SpinItemSo _spinItemSo;
        private float _angle;
        private Vector3 _startDir;
        private float _radius;
        private readonly List<float> _childOffsets = new();
        public List<GameObject> objects = new();

        protected override void Awake()
        {
            base.Awake();
            _spinItemSo = (SpinItemSo)attackItemSo;
            _radius = _spinItemSo.spinRadius;
            
            gameObject.name = _spinItemSo.itemName;
            for (int i = 0; i < _spinItemSo.spinAmount; i++)
            {
                float angle = i * (2f * Mathf.PI / _spinItemSo.spinAmount);
                _childOffsets.Add(angle);
                Spawn();
            }
        }

        public override void ApplySetting()
        {
            _spinItemSo = (SpinItemSo)attackItemSo;
            _radius = _spinItemSo.spinRadius;

            gameObject.name = _spinItemSo.itemName;

            // 기존 childOffsets 초기화 후 새롭게 계산
            _childOffsets.Clear();

            // 현재 아이템 개수와 spinAmount 맞추기
            while (objects.Count < _spinItemSo.spinAmount)
            {
                Spawn();
            }
            while (objects.Count > _spinItemSo.spinAmount)
            {
                Destroy(objects[objects.Count - 1]);
                objects.RemoveAt(objects.Count - 1);
            }

            // 각 아이템을 균등하게 다시 배치
            for (int i = 0; i < _spinItemSo.spinAmount; i++)
            {
                float angle = i * (2f * Mathf.PI / _spinItemSo.spinAmount);
                _childOffsets.Add(angle);
            }

        }

        private void Spawn()
        {
            GameObject spawnItem = Instantiate(_spinItemSo.spinPrefab, transform);
            objects.Add(spawnItem);
            spawnItem.transform.position = transform.position = new Vector3(
                _radius * Mathf.Cos(_angle),
                _radius* Mathf.Sin(_angle),
                0);
        }

        
        private void Update()
        {
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
                    Vector3 dir = Player.gameObject.transform.position - child.position;
                    float angleToPlayer = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    child.rotation = Quaternion.AngleAxis(angleToPlayer + 90, Vector3.forward);
                }

                if (_spinItemSo.rickRolling)
                {
                    objects[i].transform.Rotate(0, 0, _spinItemSo.rotateSpeed);
                }
            }
            transform.position = Player.gameObject.transform.position;
        }
    }
}
