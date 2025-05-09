using Cinemachine;
using UnityEngine;

namespace CameraModule
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _introCamera;
        [SerializeField] private CinemachineVirtualCamera _gameplayCamera;

        public void SetCameraTarget(CameraType cameraType, Transform target)
        {
            CinemachineVirtualCamera camera = _introCamera;

            switch (cameraType)
            {
                case CameraType.Intro:
                    camera = _introCamera;
                    break;
                case CameraType.Gameplay:
                    camera = _gameplayCamera;
                    break;
            }
            
            camera.Follow = camera.LookAt = target;
        }
        
        public void SwitchCamera(CameraType cameraType)
        {
            switch (cameraType)
            {
                case CameraType.Intro:
                    _introCamera.enabled = true;
                    _gameplayCamera.enabled = false;
                    break;
                case CameraType.Gameplay:
                    _introCamera.enabled = false;
                    _gameplayCamera.enabled = true;
                    break;
            }
        }

        public enum CameraType
        {
            Intro,
            Gameplay
        }
    }
}