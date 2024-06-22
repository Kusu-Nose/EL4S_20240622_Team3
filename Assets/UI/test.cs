using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public float moveSpeed = 5f; // カメラの移動速度

    // Update is called once per frame
    void Update()
    {
        // カメラを前方に移動させる
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }
}
