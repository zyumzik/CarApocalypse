using BufferSystem;
using UnityEngine;
using Zenject;

namespace UI
{
    public class LookAtCameraCanvas : MonoBehaviour
    {
        public enum LookMode
        {
            LookAt,
            LookAtInverted,
            CameraForward,
            CameraForwardInverted
        }

        [SerializeField] private LookMode _lookMode;
        [SerializeField] private string _cameraId = "MainCamera";
        
        [Inject] private BufferManager _bufferManager;
        
        public Camera Camera;

        private void Awake()
        {
            Camera = _bufferManager.GetObject<Camera>(_cameraId);
        }

        private void LateUpdate()
        {
            if (Camera is null) return;
            
            switch (_lookMode)
            {
                case LookMode.LookAt:
                    transform.LookAt(Camera.transform);
                    break;
                case LookMode.LookAtInverted:
                    Vector3 dirFromCamera = transform.position - Camera.transform.position;
                    transform.LookAt(transform.position + dirFromCamera);
                    break;
                case LookMode.CameraForward:
                    transform.forward = Camera.transform.forward;
                    break;
                case LookMode.CameraForwardInverted:
                    transform.forward -= Camera.transform.forward;
                    break;
            }
        }
    }
}
