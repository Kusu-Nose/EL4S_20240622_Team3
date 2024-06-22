using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Kusunose
{
    public class FieldGenerater : MonoBehaviour
    {
        /// <summary>
        /// 地面のオブジェクト
        /// </summary>
        [SerializeField]
        private List<GameObject> _fieldUnitObjects;

        /// <summary>
        /// オフセット
        /// </summary>
        [SerializeField]
        private Vector3 _offset;
        /// <summary>
        /// 前方の数
        /// </summary>
        [SerializeField, Min(0)]
        private float _forwardCount = 10f;
        /// <summary>
        /// 後方の数
        /// </summary>
        [SerializeField, Min(0)]
        private float _backwardCount = 10f;

        [SerializeField]
        private Color _debugColor = Color.red;

        /// <summary>
        /// プレイヤーのインスタンス
        /// </summary>
        private GameObject _player;
        /// <summary>
        /// 地面ユニットのプール
        /// </summary>
        private List<FieldUnit> _fieldUnitPool = new List<FieldUnit>();
        /// <summary>
        /// 地面のコリジョン
        /// </summary>
        private GameObject _fieldCollision;


        private void Awake()
        {
            _player = GameObject.FindWithTag("Player");
        }

        private void Start()
        {
            int loopCount = (int)(_backwardCount + _forwardCount) + 1;
            for(int i = 0; i < loopCount; i++)
            {
                // 地面を生成してプールに追加
                GameObject gameObject = Instantiate(_fieldUnitObjects[i % _fieldUnitObjects.Count]);
                _fieldUnitPool.Add(gameObject.GetComponent<FieldUnit>());
            }

            int begin = (int)(_backwardCount) * -1;
            for (int i = 0; i < loopCount; i++)
            {
                // 位置をセット
                Vector3 pos = transform.position + new Vector3(0f, 0f, _player.transform.position.z);
                _fieldUnitPool[i].transform.position = pos + _offset + new Vector3(0f, 0f, (begin + i) * _fieldUnitPool[i].Size.z);
            }
        }

        private void Update()
        {
            UpdateField(_player.transform.position.z);

            

            //UpdateCollision(_player.transform.position.z);
        }

        /// <summary>
        /// 地面の更新
        /// </summary>
        /// <param name="basePosZ"></param>
        private void UpdateField(float basePosZ)
        {
            for (int i = 0; i < _fieldUnitPool.Count; i++)
            {
                FieldUnit fieldUnit = _fieldUnitPool[i];
                float unitPosZ = fieldUnit.transform.position.z + (fieldUnit.Size.z / 2);

                // 後方の範囲を超えたら前方に移動
                if (unitPosZ < basePosZ - _backwardCount * fieldUnit.Size.z)
                {
                    // 位置を更新
                    fieldUnit.transform.position = _fieldUnitPool.Last().transform.position + new Vector3(0, 0, fieldUnit.Size.z);

                    // 該当要素を最後尾に移動
                    _fieldUnitPool.RemoveAt(i);
                    _fieldUnitPool.Add(fieldUnit);
                }
            }
        }

        /// <summary>
        /// 当たり判定の更新
        /// </summary>
        /// <param name="basePosZ"></param>
        //private void UpdateCollision(float basePosZ)
        //{
        //    //　コリジョンが常にプレイヤーの足元にあるようにする
        //    Vector3 pos = _fieldCollision.transform.position;
        //    _fieldCollision.transform.position = new Vector3(pos.x, pos.y, basePosZ);
        //}

        private void OnDrawGizmos()
        {
            ShapeGizmo.DrawWireCube(transform.position + _offset, new Vector3(5f, 1f, 5f), _debugColor);
        }
    }
}
