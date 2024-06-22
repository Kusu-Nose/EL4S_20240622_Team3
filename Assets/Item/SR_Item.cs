using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_Item : MonoBehaviour
{
    [SerializeField][Tooltip("‰ñ“]‘¬“x")] float _rotVolume = 0.01f;

    // Update is called once per frame
    void Update()
    {
        var rotation = transform.rotation;
        rotation *= Quaternion.AngleAxis(_rotVolume, Vector3.up);
        transform.rotation = rotation;
    }
}
