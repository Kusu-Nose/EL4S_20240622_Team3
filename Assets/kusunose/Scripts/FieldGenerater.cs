using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Kusunose
{
    public class FieldGenerater : MonoBehaviour
    {
        /// <summary>
        /// プレイヤーのインスタンス
        /// </summary>
        [SerializeField]
        private GameObject _player;
        /// <summary>
        /// 地面ユニットのプレハブ
        /// </summary>
        [SerializeField]
        private List<GameObject> _fieldUnits;

        [SerializeField]
        private Vector3 _fieldOffset;
        /// <summary>
        /// 前方の範囲
        /// </summary>
        [SerializeField, Min(0)]
        private float _forwardRange = 10f;
        /// <summary>
        /// 後方の範囲
        /// </summary>
        [SerializeField, Min(0)]
        private float _backwardRange = 10f;

        /// <summary>
        /// 地面ユニットのプール
        /// </summary>
        private List<GameObject> _fieldUnitPool = new List<GameObject>();
        /// <summary>
        /// 地面のコリジョン
        /// </summary>
        private GameObject _fieldCollision;
        /// <summary>
        /// 地面ユニットのスケール
        /// </summary>
        private Vector3 _fieldUnitScale;

        private void Awake()
        {
            _fieldCollision = transform.GetChild(0).gameObject;
        }

        private void Start()
        {
            _fieldUnitScale = _fieldUnits.First().transform.localScale;

            int begin = (int)(_backwardRange) * -1;
            int end = (int)(_forwardRange);

            int count = 0;
            for (int i = begin; i <= end; i++)
            {
                Vector3 pos = _player.transform.position + _fieldOffset + new Vector3(0, 0, i * _fieldUnitScale.z);

                // 地面を生成してプールに追加
                _fieldUnitPool.Add(Instantiate(_fieldUnits[count % _fieldUnits.Count], pos, Quaternion.identity));
                count++;
            }
        }

        private void Update()
        {
            UpdateField(_player.transform.position.z);
            UpdateCollision(_player.transform.position.z);
        }

        /// <summary>
        /// 地面の更新
        /// </summary>
        /// <param name="basePosZ"></param>
        private void UpdateField(float basePosZ)
        {
            for (int i = 0; i < _fieldUnitPool.Count; i++)
            {
                GameObject fieldUnit = _fieldUnitPool[i];
                float unitPosZ = _fieldUnitPool[i].transform.position.z;

                // 後方の範囲を超えたら前方に移動
                if (unitPosZ < basePosZ - _backwardRange)
                {
                    // 位置を更新
                    fieldUnit.transform.position = _fieldUnitPool.Last().transform.position + new Vector3(0, 0, _fieldUnitScale.z);

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
        private void UpdateCollision(float basePosZ)
        {
            //　コリジョンが常にプレイヤーの足元にあるようにする
            Vector3 pos = _fieldCollision.transform.position;
            _fieldCollision.transform.position = new Vector3(pos.x, pos.y, basePosZ);
        }
    }
}
