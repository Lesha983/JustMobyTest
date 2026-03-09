namespace JustMobyTest.Gameplay
{
    using System;
    using System.Collections.Generic;
    using Input;
    using UnityEngine;
    using Zenject;

    public class CameraService : MonoBehaviour
    {
        [Inject]
        private IInputHandler InputHandler { get; set; }
        
        [SerializeField]
        private List<VirtualCamera> cameras;

        public VirtualCamera CurrentCamera { get; private set; }

        private CameraType _currentCameraType;

        public void SwitchCamera(CameraType cameraType)
        {
            foreach (var virtualCamera in cameras)
            {
                var active = virtualCamera.Type == cameraType;
                virtualCamera.SetActive(active);
                if (active) CurrentCamera = virtualCamera;
            }

            _currentCameraType = cameraType;
        }

        private void OnEnable()
        {
            InputHandler.OnStartAim += SetAimCamera;
            InputHandler.OnEndAim += SetPlayerCamera;
        }

        private void OnDisable()
        {
            InputHandler.OnStartAim -= SetAimCamera;
            InputHandler.OnEndAim -= SetPlayerCamera;
        }

        private void Start()
        {
            SetPlayerCamera();
        }

        private void SetAimCamera()
        {
            SwitchCamera(CameraType.Aim);
        }
        
        private void SetPlayerCamera()
        {
            SwitchCamera(CameraType.Player);
        }
    }
}