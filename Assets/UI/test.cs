using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public float moveSpeed = 5f; // �J�����̈ړ����x

    // Update is called once per frame
    void Update()
    {
        // �J������O���Ɉړ�������
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }
}
