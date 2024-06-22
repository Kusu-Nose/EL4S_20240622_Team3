using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootEffect : MonoBehaviour
{
    [SerializeField]
    private GameObject _effect;

    [SerializeField]
    private float _interval;

    private float _time;

    void Start()
    {
        _time = 0f;
    }

    private void Update()
    {
        _time += Time.deltaTime;

        if(_time > _interval) 
        {
            CreateEffect();

            _time = 0f;
        }

    }

    private void CreateEffect()
    {
        Instantiate(_effect, this.transform);
    }
}
