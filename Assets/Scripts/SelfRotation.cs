using UnityEngine;

namespace Assets.Scripts
{
    public class SelfRotation : MonoBehaviour
    {
        public float RotationSpeed = 10.0f;
        private Transform _transform;

        public void Start()
        {
            _transform = transform;
        }

        private void Update()
        {
            _transform.Rotate(Vector3.forward, Time.deltaTime*RotationSpeed);
        }
    }
}