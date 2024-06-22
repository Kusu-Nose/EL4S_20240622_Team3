using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Kusunose
{
    public class FieldGenerater : MonoBehaviour
    {
        /// <summary>
        /// �v���C���[�̃C���X�^���X
        /// </summary>
        [SerializeField]
        private GameObject _player;
        /// <summary>
        /// �n�ʃ��j�b�g�̃v���n�u
        /// </summary>
        [SerializeField]
        private List<GameObject> _fieldUnits;

        [SerializeField]
        private Vector3 _fieldOffset;
        /// <summary>
        /// �O���͈̔�
        /// </summary>
        [SerializeField, Min(0)]
        private float _forwardRange = 10f;
        /// <summary>
        /// ����͈̔�
        /// </summary>
        [SerializeField, Min(0)]
        private float _backwardRange = 10f;

        /// <summary>
        /// �n�ʃ��j�b�g�̃v�[��
        /// </summary>
        private List<GameObject> _fieldUnitPool = new List<GameObject>();
        /// <summary>
        /// �n�ʂ̃R���W����
        /// </summary>
        private GameObject _fieldCollision;
        /// <summary>
        /// �n�ʃ��j�b�g�̃X�P�[��
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

                // �n�ʂ𐶐����ăv�[���ɒǉ�
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
        /// �n�ʂ̍X�V
        /// </summary>
        /// <param name="basePosZ"></param>
        private void UpdateField(float basePosZ)
        {
            for (int i = 0; i < _fieldUnitPool.Count; i++)
            {
                GameObject fieldUnit = _fieldUnitPool[i];
                float unitPosZ = _fieldUnitPool[i].transform.position.z;

                // ����͈̔͂𒴂�����O���Ɉړ�
                if (unitPosZ < basePosZ - _backwardRange)
                {
                    // �ʒu���X�V
                    fieldUnit.transform.position = _fieldUnitPool.Last().transform.position + new Vector3(0, 0, _fieldUnitScale.z);

                    // �Y���v�f���Ō���Ɉړ�
                    _fieldUnitPool.RemoveAt(i);
                    _fieldUnitPool.Add(fieldUnit);
                }
            }
        }

        /// <summary>
        /// �����蔻��̍X�V
        /// </summary>
        /// <param name="basePosZ"></param>
        private void UpdateCollision(float basePosZ)
        {
            //�@�R���W��������Ƀv���C���[�̑����ɂ���悤�ɂ���
            Vector3 pos = _fieldCollision.transform.position;
            _fieldCollision.transform.position = new Vector3(pos.x, pos.y, basePosZ);
        }
    }
}
