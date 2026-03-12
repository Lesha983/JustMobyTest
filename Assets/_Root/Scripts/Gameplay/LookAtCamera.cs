namespace JustMobyTest.Gameplay
{
    using UnityEngine;

    public class LookAtCamera : MonoBehaviour
    {
        private Camera _camera;
        
        private void Start()
        {
            _camera = Camera.main;
        }
        
        private void Update()
        {
            var cameraRotation = _camera.transform.rotation;
            transform.LookAt(transform.position + cameraRotation * Vector3.forward, cameraRotation * Vector3.up);
        }
    }
}