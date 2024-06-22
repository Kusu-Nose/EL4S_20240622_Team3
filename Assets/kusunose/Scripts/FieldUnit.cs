using UnityEngine;

namespace Kusunose
{
    public class FieldUnit : MonoBehaviour
    {
        /// <summary>
        /// ÉTÉCÉY
        /// </summary>
        [SerializeField]
        private Vector3 _size = Vector3.one;
        public Vector3 Size => _size;

        private void OnDrawGizmos()
        {
            ShapeGizmo.DrawWireCube(transform.position, _size, Color.red);
        }
    }
}