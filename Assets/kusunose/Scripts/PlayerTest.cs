using UnityEngine;

namespace Kusunose
{
    public class PlayerTest : MonoBehaviour
    {
        void Update()
        {
            if(Input.GetKey(KeyCode.W))
            {
                transform.position += Vector3.forward * 0.1f;
            }
        }
    }
}
