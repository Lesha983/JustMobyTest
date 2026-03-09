namespace JustMobyTest.Gameplay
{
    using Cinemachine;
    using UnityEngine;

    public class VirtualCamera : MonoBehaviour
    {
        [SerializeField]
        private CameraType type;
        [SerializeField]
        private CinemachineVirtualCamera cm;
        
        public CameraType Type => type;
        public bool IsActive => cm.gameObject.activeSelf;

        public void SetActive(bool active)
        {
            cm.gameObject.SetActive(active);
        }
    }
}