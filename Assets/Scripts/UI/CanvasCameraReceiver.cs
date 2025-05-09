using System;
using BufferSystem;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace UI
{
    public class CanvasCameraReceiver : MonoBehaviour
    {
        [SerializeField] private string _cameraId = "MainCamera";
        [SerializeField] private Canvas[] _canvases;
        [SerializeField] private InputController _inputController;

        [Inject]
        private void Construct(BufferManager bufferManager)
        {
            var canvasCamera = bufferManager.GetObject<Camera>(_cameraId);
            AssignCanvasesCamera(canvasCamera);
        }

        private void AssignCanvasesCamera(Camera worldCamera)
        {
            foreach (var canvas in _canvases)
            {
                canvas.worldCamera = worldCamera;
            }
            _inputController.Initialize(worldCamera);
        }
    }
}