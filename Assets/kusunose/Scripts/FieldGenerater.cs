using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Kusunose
{
    public class FieldGenerater : MonoBehaviour
    {
        /// <summary>
        /// �n�ʂ̃I�u�W�F�N�g
        /// </summary>
        [SerializeField]
        private List<GameObject> _fieldUnitObjects;

        /// <summary>
        /// �I�t�Z�b�g
        /// </summary>
        [SerializeField]
        private Vector3 _offset;
        /// <summary>
        /// �O���̐�
        /// </summary>
        [SerializeField, Min(0)]
        private float _forwardCount = 10f;
        /// <summary>
        /// ����̐�
        /// </summary>
        [SerializeField, Min(0)]
        private float _backwardCount = 10f;

        [SerializeField]
        private Color _debugColor = Color.red;

        /// <summary>
        /// �v���C���[�̃C���X�^���X
        /// </summary>
        private GameObject _player;
        /// <summary>
        /// �n�ʃ��j�b�g�̃v�[��
        /// </summary>
        private List<FieldUnit> _fieldUnitPool = new List<FieldUnit>();
        /// <summary>
        /// �n�ʂ̃R���W����
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
                // �n�ʂ𐶐����ăv�[���ɒǉ�
                GameObject gameObject = Instantiate(_fieldUnitObjects[i % _fieldUnitObjects.Count]);
                _fieldUnitPool.Add(gameObject.GetComponent<FieldUnit>());
            }

            int begin = (int)(_backwardCount) * -1;
            for (int i = 0; i < loopCount; i++)
            {
                // �ʒu���Z�b�g
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
        /// �n�ʂ̍X�V
        /// </summary>
        /// <param name="basePosZ"></param>
        private void UpdateField(float basePosZ)
        {
            for (int i = 0; i < _fieldUnitPool.Count; i++)
            {
                FieldUnit fieldUnit = _fieldUnitPool[i];
                float unitPosZ = fieldUnit.transform.position.z + (fieldUnit.Size.z / 2);

                // ����͈̔͂𒴂�����O���Ɉړ�
                if (unitPosZ < basePosZ - _backwardCount * fieldUnit.Size.z)
                {
                    // �ʒu���X�V
                    fieldUnit.transform.position = _fieldUnitPool.Last().transform.position + new Vector3(0, 0, fieldUnit.Size.z);

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
        //private void UpdateCollision(float basePosZ)
        //{
        //    //�@�R���W��������Ƀv���C���[�̑����ɂ���悤�ɂ���
        //    Vector3 pos = _fieldCollision.transform.position;
        //    _fieldCollision.transform.position = new Vector3(pos.x, pos.y, basePosZ);
        //}

        private void OnDrawGizmos()
        {
            ShapeGizmo.DrawWireCube(transform.position + _offset, new Vector3(5f, 1f, 5f), _debugColor);
        }
    }
}
