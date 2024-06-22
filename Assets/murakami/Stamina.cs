using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    public static Stamina instance;

    // スタミナ値
    [HideInInspector]
    public float value;

    [Header("回復する値")]
    [SerializeField]
    [Range(0f, 50f)]
    private float _recoverValue;

    // 減少量
    private float _decValue;

    [Header("元の減少値(1秒で減る値)")]
    [SerializeField]
    [Range(0f, 50f)]
    private float _originalDecValue;

    // 減少値の倍率
    private float _mulDecValue = 1f;

    [Header("減少量変更の増える値")]
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
        // 減少値変更
        _decValue = _originalDecValue * _mulDecValue;

        // 減少
        value -= _decValue * Time.deltaTime;
        value = Mathf.Max(0f, value);


        // 以下テスト用==================================
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
