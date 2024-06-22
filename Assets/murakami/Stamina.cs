using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    public static Stamina instance;

    // �X�^�~�i�l
    [HideInInspector]
    public float value;

    [Header("�񕜂���l")]
    [SerializeField]
    [Range(0f, 50f)]
    private float _recoverValue;

    // ������
    private float _decValue;

    [Header("���̌����l(1�b�Ō���l)")]
    [SerializeField]
    [Range(0f, 50f)]
    private float _originalDecValue;

    // �����l�̔{��
    private float _mulDecValue = 1f;

    [Header("�����ʕύX�̑�����l")]
    [SerializeField]
    [Range(0f, 0.2f)]
    private float _addMulDecValue;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        value = 100f;
        _decValue = _originalDecValue;
    }

    void Update()
    {
        // �����l�ύX
        _decValue = _originalDecValue * _mulDecValue;

        // ����
        value -= _decValue * Time.deltaTime;
        value = Mathf.Max(0f, value);


        // �ȉ��e�X�g�p==================================
        Debug.Log(value);
        if (Input.GetKeyUp(KeyCode.Space)) AddStamina();
        if (Input.GetKey(KeyCode.Return)) ChangeDecValue();
    }

    //==============================================-s

    public void AddStamina()
    {
        value += _recoverValue;
        value = Mathf.Min(100f, value);
    }

    public void StaminaReset()
    {
        value = 100f;
        _decValue = _originalDecValue;
    }

    public void ChangeDecValue()
    {
        _mulDecValue += _addMulDecValue;
    }
}
